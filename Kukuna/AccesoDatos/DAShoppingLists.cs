using AccesoDatos.Entities;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DAShoppingLists
    {
        public List<ShoppingListDTO> GetShoppingLists()
        {
            DateOnly today = new DateOnly();
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return (from ri in context.RecipeIngredients
                            join i in context.Ingredients on ri.IngredientId equals i.IngredientId
                            join u in context.Units on ri.UnitId equals u.UnitId
                            join r in context.Recipes on ri.RecipeId equals r.RecipeId
                            join mp in context.MealPlans on ri.RecipeId equals mp.RecipeId
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
                            }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ShoppingList GetShoppingLists(int id)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.ShoppingLists.Where(i => i.ShoppingListId == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
