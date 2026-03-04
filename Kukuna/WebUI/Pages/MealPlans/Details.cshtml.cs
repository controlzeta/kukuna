using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.MealPlans
{
    public class DetailsModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DAMealPlans proxy;
        public DetailsModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DAMealPlans(_context);
        }
        public MealPlan MealPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealplan = proxy.GetMealPlansById((int)id);
            if (mealplan == null)
            {
                return NotFound();
            }
            else
            {
                MealPlan = mealplan;
            }
            return Page();
        }
    }
}
