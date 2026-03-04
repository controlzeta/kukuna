using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DARecipes proxy;

        public CreateModel(SqlServerDbContext context)
        {
            proxy = new DARecipes(_context);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            proxy.InsertRecipe(Recipe.RecipeName, Recipe.Instructions, Recipe.Servings);

            return RedirectToPage("./Index");
        }
    }
}
