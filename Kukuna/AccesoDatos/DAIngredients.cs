using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DAIngredients
    {
        public List<Ingredient> GetIngredients() 
        {
			try
			{
				using (var context = new SqlServerDbContext())
				{
					return context.Ingredients.OrderBy(i => i.IngredientId).ToList();
				}
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ingredient GetIngredientsById(int id)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Ingredients.Where(m => m.IngredientId == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ingredient GetIngredientsByName(string ingredientName)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Ingredients.Where(m => m.IngredientName == ingredientName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateIngredient(int id, string ingredientName, string category)
        {
            Ingredient i = GetIngredientsById(id);
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    if (i != null)
                    {
                        i.Category = category;
                        i.IngredientName = ingredientName;
                        context.Entry(i).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool DeleteIngredient(int id)
        {
            Ingredient i = GetIngredientsById(id);
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    if (i != null)
                    {
                        context.Entry(i).State = EntityState.Deleted;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }


        public bool InsertIngredient(string ingredientName, string category)
        {
            try
            {
                if (GetIngredientsByName(ingredientName) == null)
                {
                    using (var context = new SqlServerDbContext())
                    {
                        context.Ingredients.Add(new Ingredient
                        {
                            IngredientName = ingredientName,
                            Category = category
                        });
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
