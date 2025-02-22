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
        //public List<RecipeIngredient> GetRecipeIngredients()
        //{
        //    using (var context = new SqlServerDbContext())
        //    {
        //        return context.RecipeIngredients.Include
        //        .Include(r => r.Ingredient)
        //        .Include(r => r.Recipe)
        //        .Include(r => r.Unit).ToList();
        //    }
        //}
        public List<RecipeIngredientDTO> GetRecipeIngredientsDTO()
        {
            List<RecipeIngredientDTO> res = new List<RecipeIngredientDTO>();

            using (var context = new SqlServerDbContext())
            {
                res = (from ri in context.RecipeIngredients
                       join i in context.Ingredients on ri.IngredientId equals i.IngredientId
                       join u in context.Units on ri.UnitId equals u.UnitId
                       join r in context.Recipes on ri.RecipeId equals r.RecipeId
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
        }

        public RecipeIngredient GetRecipeIngredientsById(int id)
        {
            using (var context = new SqlServerDbContext())
            {
                return context.RecipeIngredients.Where(m => m.RecipeIngredientId == id).FirstOrDefault();
            }
        }

        public RecipeIngredient GetRecipeIngredientsByIngredientId(int id)
        {
            using (var context = new SqlServerDbContext())
            {
                return context.RecipeIngredients.Where(m => m.IngredientId == id).FirstOrDefault();
            }

        }

        public RecipeIngredient GetRecipeIngredientsByIngredientIdAndRecipeId(int ingredientId, int recipeId)
        {
            using (var context = new SqlServerDbContext())
            {
                return context.RecipeIngredients.Where(m => m.IngredientId == ingredientId && m.RecipeId == recipeId).FirstOrDefault();
            }

        }

        public List<RecipeIngredientDTO> GetRecipeIngredientsDTOById(int id)
        {
            List<RecipeIngredientDTO> res = new List<RecipeIngredientDTO>();

            using (var context = new SqlServerDbContext())
            {
                res = (from ri in context.RecipeIngredients
                       join i in context.Ingredients on ri.IngredientId equals i.IngredientId
                       join u in context.Units on ri.UnitId equals u.UnitId
                       join r in context.Recipes on ri.RecipeId equals r.RecipeId
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
                using (var context = new SqlServerDbContext())
                {
                    if (i != null)
                    {
                        if(unitId != null)
                            i.UnitId = unitId;
                        if(quantity != null)
                            i.Quantity = (decimal)quantity;
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

        public bool DeleteRecipeIngredient(int id)
        {
            RecipeIngredient i = GetRecipeIngredientsById(id);
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


        public bool InsertRecipeIngredient(int ingredientId, int recipeId, int unitId, decimal quantity)
        {
            try
            {
                if (GetRecipeIngredientsByIngredientIdAndRecipeId(ingredientId, recipeId) == null)
                {
                    using (var context = new SqlServerDbContext())
                    {
                        context.RecipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            UnitId = unitId,
                            IngredientId = ingredientId,
                            Quantity = quantity
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