using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.MealPlans
{
    public class EditModel : PageModel
    {
        DAMealPlans proxy = new DAMealPlans();
        DARecipes proxyRe = new DARecipes();
        [BindProperty]
        public MealPlan MealPlan { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealplan =  proxy.GetMealPlansById((int)id);
            if (mealplan == null)
            {
                return NotFound();
            }
            MealPlan = mealplan;
            var recipes = proxyRe.GetRecipes();
            ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                proxy.UpdateMealPlan(MealPlan.MealPlanId, MealPlan.DayOfWeek, (int)MealPlan.RecipeId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealPlanExists(MealPlan.MealPlanId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MealPlanExists(int id)
        {
            var mp = proxy.GetMealPlansById((int)id);
            return mp != null;
        }
    }
}
