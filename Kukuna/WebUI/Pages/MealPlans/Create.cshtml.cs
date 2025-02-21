using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.MealPlans
{
    public class CreateModel : PageModel
    {
        DAMealPlans proxy = new DAMealPlans();
        DARecipes proxyRe = new DARecipes();

        public IActionResult OnGet()
        {
            var recipes = proxyRe.GetRecipes();
            ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
            return Page();
        }

        [BindProperty]
        public MealPlan MealPlan { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            proxy.InsertMealPlan(MealPlan.DayOfWeek, (int)MealPlan.RecipeId);

            return RedirectToPage("./Index");
        }
    }
}
