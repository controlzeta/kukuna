using AccesoDatos.Entities;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DARecipeIngredients
    {
        private readonly SqlServerDbContext _context;

        public DARecipeIngredients(SqlServerDbContext context)
        {
            _context = context;
        }

        public List<RecipeIngredientDTO> GetRecipeIngredientsDTO()
        {
            List<RecipeIngredientDTO> res = new List<RecipeIngredientDTO>();
            res = (from ri in _context.RecipeIngredients
                   join i in _context.Ingredients on ri.IngredientId equals i.IngredientId
                   join u in _context.Units on ri.UnitId equals u.UnitId
                   join r in _context.Recipes on ri.RecipeId equals r.RecipeId
                   select new RecipeIngredientDTO
                   {
                       RecipeIngredientId = ri.RecipeIngredientId,
                       RecipeId = (int)ri.RecipeId,
                       RecipeName = r.RecipeName,
                       IngredientId = i.IngredientId,
                       IngredientName = i.IngredientName,
                       UnitId = (int)ri.UnitId,
                       UnitName = u.UnitName,
                       Quantity = ri.Quantity
                   }
                   ).ToList();
            return res;
        }

        public RecipeIngredient GetRecipeIngredientsById(int id)
        {
            return _context.RecipeIngredients.Where(m => m.RecipeIngredientId == id).FirstOrDefault();
        }

        public RecipeIngredient GetRecipeIngredientsByIngredientId(int id)
        {
            return _context.RecipeIngredients.Where(m => m.IngredientId == id).FirstOrDefault();
        }

        public RecipeIngredient GetRecipeIngredientsByIngredientIdAndRecipeId(int ingredientId, int recipeId)
        {
            return _context.RecipeIngredients.Where(m => m.IngredientId == ingredientId && m.RecipeId == recipeId).FirstOrDefault();
        }

        public List<RecipeIngredientDTO> GetRecipeIngredientsDTOById(int id)
        {
            List<RecipeIngredientDTO> res = new List<RecipeIngredientDTO>();

            res = (from ri in _context.RecipeIngredients
                   join i in _context.Ingredients on ri.IngredientId equals i.IngredientId
                   join u in _context.Units on ri.UnitId equals u.UnitId
                   join r in _context.Recipes on ri.RecipeId equals r.RecipeId
                   where ri.RecipeId == id
                   select new RecipeIngredientDTO
                   {
                       RecipeIngredientId = ri.RecipeIngredientId,
                       RecipeId = (int)ri.RecipeId,
                       RecipeName = r.RecipeName,
                       IngredientId = i.IngredientId,
                       IngredientName = i.IngredientName,
                       UnitId = (int)ri.UnitId,
                       UnitName = u.UnitName,
                       Quantity = ri.Quantity
                   }
                   ).ToList();
            return res;

        }

        //public RecipeIngredientDTO GetRecipeIngredientsDTOById(int id)
        //{
        //    RecipeIngredientDTO res = new RecipeIngredientDTO();

        //    using (var context = new SqlServerDbContext())
        //    {
        //        res = (from ri in context.RecipeIngredients
        //               join i in context.Ingredients on ri.IngredientId equals i.IngredientId
        //               join u in context.Units on ri.UnitId equals u.UnitId
        //               join r in context.Recipes on ri.RecipeId equals r.RecipeId
        //               where ri.RecipeIngredientId == id
        //               select new RecipeIngredientDTO
        //               {
        //                   RecipeIngredientId = ri.RecipeIngredientId,
        //                   RecipeId = (int)ri.RecipeId,
        //                   RecipeName = r.RecipeName,
        //                   IngredientId = i.IngredientId,
        //                   IngredientName = i.IngredientName,
        //                   UnitId = (int)ri.UnitId,
        //                   UnitName = u.UnitName,
        //                   Quantity = ri.Quantity
        //               }
        //               ).FirstOrDefault();
        //        return res;
        //    }

        //}

        public bool UpdateRecipeIngredients(int id, int? unitId, decimal? quantity)
        {
            RecipeIngredient i = GetRecipeIngredientsById(id);
            try
            {
                if (i != null)
                {
                    if (unitId != null)
                        i.UnitId = unitId;
                    if (quantity != null)
                        i.Quantity = (decimal)quantity;
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

        public bool DeleteRecipeIngredient(int id)
        {
            RecipeIngredient i = GetRecipeIngredientsById(id);
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


        public bool InsertRecipeIngredient(int ingredientId, int recipeId, int unitId, decimal quantity)
        {
            try
            {
                if (GetRecipeIngredientsByIngredientIdAndRecipeId(ingredientId, recipeId) == null)
                {
                    _context.RecipeIngredients.Add(new RecipeIngredient
                    {
                        RecipeId = recipeId,
                        UnitId = unitId,
                        IngredientId = ingredientId,
                        Quantity = quantity
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