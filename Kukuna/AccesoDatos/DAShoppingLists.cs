﻿using AccesoDatos.Entities;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos
{
    public class DAShoppingLists
    {
        private readonly SqlServerDbContext _context;

        public DAShoppingLists(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShoppingListDTO>> GetShoppingListsAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            try
            {
                var res = await (from ri in _context.RecipeIngredients
                            join i in _context.Ingredients on ri.IngredientId equals i.IngredientId
                            join u in _context.Units on ri.UnitId equals u.UnitId
                            join r in _context.Recipes on ri.RecipeId equals r.RecipeId
                            join mp in _context.MealPlans on ri.RecipeId equals mp.RecipeId
                            where mp.DayOfWeek > today
                            select new // <-- Proyectamos un nuevo objeto anónimo
                            {
                                IngredientName = i.IngredientName,
                                IngredientId = i.IngredientId,
                                UnitId = ri.UnitId,
                                UnitName = u.UnitName,
                                QuantityParaSumar = ri.Quantity // <-- Incluimos QuantityParaSumar
                            } into elementosParaAgrupar // <-- Almacenamos los elementos para agrupar
                            group elementosParaAgrupar by new { elementosParaAgrupar.IngredientName, elementosParaAgrupar.IngredientId, elementosParaAgrupar.UnitId, elementosParaAgrupar.UnitName } into g // <-- Agrupamos por las propiedades del objeto anónimo
                            select new ShoppingListDTO
                            {
                                IngredientName = g.Key.IngredientName,
                                IngredientId = g.Key.IngredientId,
                                UnitId = g.Key.UnitId,
                                UnitName = g.Key.UnitName,
                                TotalQuantity = g.Sum(x => x.QuantityParaSumar) // <-- Ahora x tiene la propiedad QuantityParaSumar
                            })
                            .OrderBy(m => m.IngredientId) // Optimización: Ordenar en SQL antes de traer a memoria
                            .ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw; // Corrección: 'throw ex' borra el stack trace original. 'throw' lo mantiene.
            }
        }

        public async Task<ShoppingList?> GetShoppingListsAsync(int id)
        {
            try
            {
                // Optimización: AsNoTracking reduce overhead en lecturas y FirstOrDefault con predicado es más limpio
                return await _context.ShoppingLists.AsNoTracking()
                    .FirstOrDefaultAsync(i => i.ShoppingListId == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
