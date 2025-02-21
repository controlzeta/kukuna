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
    public class IndexModel : PageModel
    {
        DARecipeIngredients proxy = new DARecipeIngredients();

        public IndexModel()
        {
                
        }

        public IList<RecipeIngredientDTO> RecipeIngredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            RecipeIngredient = proxy.GetRecipeIngredientsDTO();
        }
    }
}
