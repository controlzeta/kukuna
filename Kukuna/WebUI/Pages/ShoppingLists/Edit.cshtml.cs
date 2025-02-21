using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace WebUI.Pages.ShoppingLists
{
    public class EditModel : PageModel
    {
        private readonly AccesoDatos.Models.SqlServerDbContext _context;

        public EditModel(AccesoDatos.Models.SqlServerDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShoppingList ShoppingList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppinglist =  await _context.ShoppingLists.FirstOrDefaultAsync(m => m.ShoppingListId == id);
            if (shoppinglist == null)
            {
                return NotFound();
            }
            ShoppingList = shoppinglist;
           ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "IngredientName");
           ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitName");
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

            _context.Attach(ShoppingList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(ShoppingList.ShoppingListId))
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

        private bool ShoppingListExists(int id)
        {
            return _context.ShoppingLists.Any(e => e.ShoppingListId == id);
        }
    }
}
