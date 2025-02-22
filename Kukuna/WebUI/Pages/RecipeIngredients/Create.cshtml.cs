using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.RecipeIngredients
{
    public class CreateModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();
        DAIngredients proxyIng = new DAIngredients();
        DAUnits proxyUn = new DAUnits();
        DARecipes proxyRe = new DARecipes();

        public CreateModel()
        {
            
        }

        public IActionResult OnGet()
        {
            var ingredients = proxyIng.GetIngredients();
            var units = proxyUn.GetUnits();
            var recipes = proxyRe.GetRecipes();
            ViewData["IngredientId"] = new SelectList(ingredients, "IngredientId", "IngredientName");
            ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
            ViewData["UnitId"] = new SelectList(units, "UnitId", "UnitName");
            return Page();
        }

        [BindProperty]
        public RecipeIngredient RecipeIngredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            proxy.InsertRecipeIngredient((int)RecipeIngredient.IngredientId, (int)RecipeIngredient.RecipeId, (int)RecipeIngredient.UnitId, RecipeIngredient.Quantity);

            return RedirectToPage("./Index", new { id = RecipeIngredient.RecipeId });
        }
    }
}
