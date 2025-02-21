using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;
using AccesoDatos.Entities;

namespace WebUI.Pages.RecipeIngredients
{
    public class DetailsModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();
        public DetailsModel()
        {
        }

        public RecipeIngredientDTO RecipeIngredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeingredient = proxy.GetRecipeIngredientsDTOById((int)id);
            if (recipeingredient == null)
            {
                return NotFound();
            }
            else
            {
                RecipeIngredient = recipeingredient.FirstOrDefault();
            }
            return Page();
        }
    }
}
