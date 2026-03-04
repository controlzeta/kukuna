using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DAIngredients
    {
        private readonly SqlServerDbContext _context;

        public DAIngredients(SqlServerDbContext context)
        {
            _context = context;
        }
        public List<Ingredient> GetIngredients() 
        {
			try
			{
			    return _context.Ingredients.OrderBy(i => i.IngredientId).ToList();
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
                return _context.Ingredients.Where(m => m.IngredientId == id).FirstOrDefault();
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
                return _context.Ingredients.Where(m => m.IngredientName == ingredientName).FirstOrDefault();
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
                if (i != null)
                {
                    i.Category = category;
                    i.IngredientName = ingredientName;
                    _context.Entry(i).State = EntityState.Modified;
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

        public bool DeleteIngredient(int id)
        {
            Ingredient i = GetIngredientsById(id);
            try
            {
                if (i != null)
                {
                    _context.Entry(i).State = EntityState.Deleted;
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


        public bool InsertIngredient(string ingredientName, string category)
        {
            try
            {
                if (GetIngredientsByName(ingredientName) == null)
                {
                    _context.Ingredients.Add(new Ingredient
                    {
                        IngredientName = ingredientName,
                        Category = category
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
