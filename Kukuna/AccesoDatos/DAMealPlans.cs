using AccesoDatos.Entities;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DAMealPlans
    {
        private readonly SqlServerDbContext _context;

        public DAMealPlans(SqlServerDbContext context)
        {
            _context = context;
        }
        public List<MealPlan> GetMealPlans()
        {
            try
            {
                return _context.MealPlans.OrderBy(i => i.DayOfWeek).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MealPlanDTO> GetMealPlansDTO()
        {
            try
            {
                return (from ri in _context.MealPlans
                        join r in _context.Recipes on ri.RecipeId equals r.RecipeId
                        select new MealPlanDTO
                        {
                            MealPlanId = ri.MealPlanId,
                            DayOfWeek = ri.DayOfWeek,
                            RecipeId = (int)ri.RecipeId,
                            RecipeName = r.RecipeName,
                            DayName = ri.DayOfWeek.ToString("dddd", new CultureInfo("es-ES"))
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MealPlan> GetMealPlans(DateOnly lastDay)
        {
            try
            {
                return (from mp in _context.MealPlans
                        where mp.DayOfWeek >= lastDay
                        select mp
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MealPlan GetMealPlansById(int id)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return (from mp in _context.MealPlans
                            where mp.MealPlanId == id
                            select mp
                        ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMealPlan(int id, DateOnly dayOfWeek, int recipeId)
        {
            MealPlan i = GetMealPlansById(id);
            try
            {
                if (i != null)
                {
                    i.DayOfWeek = dayOfWeek;
                    i.RecipeId = recipeId;
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

        public bool DeleteMealPlan(int id)
        {
            MealPlan i = GetMealPlansById(id);
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


        public bool InsertMealPlan(DateOnly dayOfWeek, int recipeId)
        {
            try
            {
                _context.MealPlans.Add(new MealPlan
                {
                    DayOfWeek = dayOfWeek,
                    RecipeId = recipeId
                });
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
