using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        DARecipes proxy = new DARecipes();

        public DeleteModel()
        {
            
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = proxy.GetRecipesById((int)id);

            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                Recipe = recipe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = proxy.GetRecipesById((int)id);
            if (recipe != null)
            {
                Recipe = recipe;
                proxy.DeleteRecipe((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
