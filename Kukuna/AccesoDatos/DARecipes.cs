using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DARecipes
    {
        public List<Recipe> GetRecipes()
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Recipes.OrderBy(i => i.RecipeName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Recipe GetRecipesById(int id)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Recipes.Where(m => m.RecipeId == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Recipe GetRecipesByName(string name)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Recipes.Where(m => m.RecipeName == name).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateRecipe(int id, string recipeName, string instructions, int? servings)
        {
            Recipe r = GetRecipesById(id);
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    if (r != null)
                    {
                        r.RecipeName = recipeName;
                        r.Servings = servings;
                        r.Instructions = instructions;
                        context.Entry(r).State = EntityState.Modified;
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

        public bool DeleteRecipe(int id)
        {
            Recipe r = GetRecipesById(id);
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    if (r != null)
                    {
                        context.Entry(r).State = EntityState.Deleted;
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


        public bool InsertRecipe(string recipeName, string instructions, int? servings)
        {
            try
            {
                if (GetRecipesByName(recipeName) == null)
                {
                    using (var context = new SqlServerDbContext())
                    {
                        context.Recipes.Add(new Recipe
                        {
                            RecipeName = recipeName,
                            Instructions = instructions,
                            Servings = servings
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
