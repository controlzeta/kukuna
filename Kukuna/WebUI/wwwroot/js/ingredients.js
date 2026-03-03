﻿// TODO: Mover esta URL a un archivo de configuración o usar una ruta relativa en producción
// Si la API está en el mismo servidor, usa una ruta relativa para evitar problemas de puertos
const baseUrl = '/Catalog'; 
let ingredientsData = [];

// Cliente API Genérico para estandarizar CRUDs
async function apiCall(endpoint, method = 'GET', body = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    const response = await fetch(`${baseUrl}/${endpoint}`, options);

    if (!response.ok) {
        throw new Error(`Error API (${response.status}): ${response.statusText}`);
    }

    // Retornar null si no hay contenido, de lo contrario parsear JSON
    return response.status === 204 ? null : await response.json();
}

// Wrappers simplificados
const api = {
    get: (endpoint) => apiCall(endpoint, 'GET'),
    post: (endpoint, data) => apiCall(endpoint, 'POST', data),
    put: (endpoint, data) => apiCall(endpoint, 'PUT', data),
    del: (endpoint) => apiCall(endpoint, 'DELETE')
};

async function getRecipeIngredients() {
    const recipesElement = document.getElementById("recipes");
    if (!recipesElement) return;

    // Mostrar indicador de carga
    recipesElement.innerHTML = `
        <div class="d-flex justify-content-center my-5">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Cargando...</span>
            </div>
        </div>`;

    try {
        // Uso del cliente genérico
        ingredientsData = await api.get('GetRecipeIngredients');
        renderIngredientsUI(ingredientsData);
        
    } catch (error) {
        console.error('Error al obtener los ingredientes:', error);
        recipesElement.innerHTML = `
            <div class="alert alert-danger shadow-sm" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                No se pudieron cargar los ingredientes. Por favor, intenta más tarde.
            </div>`;
    }
}

function renderIngredientsUI(data) {
    const recipesElement = document.getElementById("recipes");
    recipesElement.innerHTML = '';

    // Crear barra de búsqueda y contenedor
    const container = document.createElement('div');
    container.className = 'vstack gap-3';

    container.innerHTML = `
        <div class="d-flex justify-content-between align-items-center mb-2">
            <div class="input-group w-50">
                <span class="input-group-text bg-white border-end-0"><i class="bi bi-search text-muted"></i></span>
                <input type="text" id="ingredientSearch" class="form-control border-start-0" placeholder="Buscar ingredientes...">
            </div>
            <span class="badge bg-light text-dark border">${data.length} elementos</span>
        </div>
        <div class="table-responsive shadow-sm rounded border">
            <table class="table table-hover mb-0 align-middle">
                <thead class="bg-light">
                    <tr>
                        <th class="ps-3">Ingrediente</th>
                        <th>Cantidad</th>
                        <th>Unidad</th>
                    </tr>
                </thead>
                <tbody id="ingredientsList">
                    ${data.map(item => `
                        <tr>
                            <td class="ps-3 fw-medium">${item.ingredientName || item.IngredientName || 'N/A'}</td>
                            <td>${item.quantity || item.Quantity || 0}</td>
                            <td><span class="badge bg-secondary bg-opacity-10 text-secondary">${item.unitName || item.UnitName || ''}</span></td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        </div>
    `;

    recipesElement.appendChild(container);

    // Lógica de filtrado
    document.getElementById('ingredientSearch').addEventListener('keyup', (e) => {
        const term = e.target.value.toLowerCase();
        const filtered = data.filter(i => 
            (i.ingredientName || i.IngredientName || '').toLowerCase().includes(term)
        );
        
        const tbody = document.getElementById('ingredientsList');
        if (filtered.length > 0) {
            tbody.innerHTML = filtered.map(item => `
                <tr>
                    <td class="ps-3 fw-medium">${item.ingredientName || item.IngredientName || 'N/A'}</td>
                    <td>${item.quantity || item.Quantity || 0}</td>
                    <td><span class="badge bg-secondary bg-opacity-10 text-secondary">${item.unitName || item.UnitName || ''}</span></td>
                </tr>
            `).join('');
        } else {
            tbody.innerHTML = `<tr><td colspan="3" class="text-center py-4 text-muted">No se encontraron resultados</td></tr>`;
        }
    });
}

// Asegurar que el DOM esté listo antes de ejecutar
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', getRecipeIngredients);
} else {
    getRecipeIngredients();
}