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

namespace WebUI.Pages.RecipeIngredients
{
    public class EditModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();
        DAIngredients proxyIng = new DAIngredients();
        DAUnits proxyUn = new DAUnits();
        DARecipes proxyRe= new DARecipes();
        public EditModel()
        {
            
        }

        [BindProperty]
        public RecipeIngredient RecipeIngredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeingredient = proxy.GetRecipeIngredientsById((int)id);
            if (recipeingredient == null)
            {
                return NotFound();
            }
            RecipeIngredient = recipeingredient;
            var ingredients = proxyIng.GetIngredients();
            var units = proxyUn.GetUnits();
            var recipes = proxyRe.GetRecipes();
            ViewData["IngredientId"] = new SelectList(ingredients, "IngredientId", "IngredientName");
            ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
            ViewData["UnitId"] = new SelectList(units, "UnitId", "UnitName");
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
                var recipeIngredient = proxy.GetRecipeIngredientsById(RecipeIngredient.RecipeIngredientId);
                proxy.UpdateRecipeIngredients(RecipeIngredient.RecipeIngredientId, RecipeIngredient.UnitId, RecipeIngredient.Quantity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(RecipeIngredient.RecipeIngredientId))
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

        private bool RecipeIngredientExists(int id)
        {
            var recipeIngredient = proxy.GetRecipeIngredientsById(id);
            return recipeIngredient != null;
        }
    }
}
