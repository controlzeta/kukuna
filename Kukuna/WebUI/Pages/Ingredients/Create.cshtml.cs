using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        DAIngredients proxy = new DAIngredients();

        public CreateModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            proxy.InsertIngredient(Ingredient.IngredientName, Ingredient.Category);

            return RedirectToPage("./Index");
        }
    }
}
