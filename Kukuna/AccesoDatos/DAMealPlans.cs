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
        public List<MealPlan> GetMealPlans()
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.MealPlans.OrderBy(i => i.DayOfWeek).ToList();
                }
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
                using (var context = new SqlServerDbContext())
                {

                    return (from ri in context.MealPlans
                            join r in context.Recipes on ri.RecipeId equals r.RecipeId
                            select new MealPlanDTO
                            {
                                MealPlanId = ri.MealPlanId,
                                DayOfWeek = ri.DayOfWeek,
                                RecipeId = (int)ri.RecipeId,
                                RecipeName = r.RecipeName,
                                DayName = ri.DayOfWeek.ToString("dddd", new CultureInfo("es-ES"))
                            }).ToList();
                }
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
                using (var context = new SqlServerDbContext())
                {
                    return (from mp in context.MealPlans
                            where mp.DayOfWeek >= lastDay
                            select mp
                        ).ToList();
                }
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
                    return (from mp in context.MealPlans
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
                using (var context = new SqlServerDbContext())
                {
                    if (i != null)
                    {
                        i.DayOfWeek = dayOfWeek;
                        i.RecipeId = recipeId;
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

        public bool DeleteMealPlan(int id)
        {
            MealPlan i = GetMealPlansById(id);
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


        public bool InsertMealPlan(DateOnly dayOfWeek, int recipeId)
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    context.MealPlans.Add(new MealPlan
                    {
                        DayOfWeek = dayOfWeek,
                        RecipeId = recipeId
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
