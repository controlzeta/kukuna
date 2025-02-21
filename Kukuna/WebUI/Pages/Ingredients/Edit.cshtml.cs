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

namespace WebUI.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        DAIngredients proxy = new DAIngredients();

        public EditModel()
        {
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = proxy.GetIngredientsById((int)id);
            if (ingredient == null)
            {
                return NotFound();
            }
            Ingredient = ingredient;
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

            proxy.UpdateIngredient(Ingredient.IngredientId, Ingredient.IngredientName, Ingredient.Category);

            return RedirectToPage("./Index");
        }

        private bool IngredientExists(int id)
        {
            var ing =  proxy.GetIngredientsById((int)id);
            return ing != null;
        }
    }
}
