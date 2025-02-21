using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccesoDatos.Models;

namespace WebUI.Pages.ShoppingLists
{
    public class CreateModel : PageModel
    {
        private readonly AccesoDatos.Models.SqlServerDbContext _context;

        public CreateModel(AccesoDatos.Models.SqlServerDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "IngredientName");
        ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitName");
            return Page();
        }

        [BindProperty]
        public ShoppingList ShoppingList { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShoppingLists.Add(ShoppingList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
