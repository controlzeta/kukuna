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
        private readonly SqlServerDbContext _context;

        public DARecipes(SqlServerDbContext context)
        {
            _context = context;
        }
        public List<Recipe> GetRecipes()
        {
            try
            {
                return _context.Recipes.OrderBy(i => i.RecipeName).ToList();
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
                return _context.Recipes.Where(m => m.RecipeId == id).FirstOrDefault();
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
                return _context.Recipes.Where(m => m.RecipeName == name).FirstOrDefault();
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
                if (r != null)
                {
                    r.RecipeName = recipeName;
                    r.Servings = servings;
                    r.Instructions = instructions;
                    _context.Entry(r).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
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
                if (r != null)
                {
                    _context.Entry(r).State = EntityState.Deleted;
                    _context.SaveChanges();
                    return true;
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
                    _context.Recipes.Add(new Recipe
                    {
                        RecipeName = recipeName,
                        Instructions = instructions,
                        Servings = servings
                    });
                    _context.SaveChanges();
                    return true;
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
