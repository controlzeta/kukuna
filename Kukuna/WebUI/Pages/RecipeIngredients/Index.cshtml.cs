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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Pages.RecipeIngredients
{
    public class IndexModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();
        DAIngredients proxyIng = new DAIngredients();
        DAUnits proxyUn = new DAUnits();
        DARecipes proxyRe = new DARecipes();

        public IndexModel()
        {

        }

        public IList<RecipeIngredientDTO> RecipeIngredient { get; set; } = default!;
        public string RecipeName { get; set; }  

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                var recipes = proxy.GetRecipeIngredientsDTO();
                RecipeIngredient = recipes;
                RecipeName = "";
                ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
            }
            else
            {

                var recipeingredient = proxy.GetRecipeIngredientsDTOById((int)id);
                if (recipeingredient == null)
                {
                    return NotFound();
                }
                RecipeIngredient = recipeingredient;
                RecipeName = recipeingredient.FirstOrDefault()?.RecipeName;
                var ingredients = proxyIng.GetIngredients();
                var units = proxyUn.GetUnits();
                var recipes = proxyRe.GetRecipes();
                ViewData["IngredientId"] = new SelectList(ingredients, "IngredientId", "IngredientName");
                ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "RecipeName");
                ViewData["UnitId"] = new SelectList(units, "UnitId", "UnitName");
            }
            return Page();
        }
    }
}
