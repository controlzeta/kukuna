using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.RecipeIngredients
{
    public class DeleteModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();

        public DeleteModel()
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
            else
            {
                RecipeIngredient = recipeingredient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeingredient = proxy.GetRecipeIngredientsById((int)id);
            if (recipeingredient != null)
            {
                RecipeIngredient = recipeingredient;
                proxy.DeleteRecipeIngredient(RecipeIngredient.RecipeIngredientId);
            }

            return RedirectToPage("./Index");
        }
    }
}
