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
    public class DeleteModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DAMealPlans proxy;

        public DeleteModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DAMealPlans(_context);
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealplan = proxy.GetMealPlansById((int)id);
            if (mealplan != null)
            {
                MealPlan = mealplan;
                proxy.DeleteMealPlan(MealPlan.MealPlanId);
            }

            return RedirectToPage("./Index");
        }
    }
}
