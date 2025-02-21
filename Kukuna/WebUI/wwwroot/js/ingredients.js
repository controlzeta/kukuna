const baseUrl = 'https://localhost:32777/Catalog';

async function get(controller, paramsget) {
}
async function getRecipeIngredients() {
    try {
        const response = await fetch(baseUrl + '/GetRecipeIngredients', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Error en la petición: ${response.status}');
        }

        const data = await response.json();
        const recipesJson = JSON.stringify(data);
        const recipesElement = document.getElementById("recipes");
        recipesElement.textContent = recipesJson;
        return data;
    } catch (error) {
        console.error('Error al obtener los ingredientes:', error);
        throw error; // Re-lanza el error para que quien llame a la función lo maneje
    }
}

getRecipeIngredients();