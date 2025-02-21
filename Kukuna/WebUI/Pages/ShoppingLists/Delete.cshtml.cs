using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace WebUI.Pages.ShoppingLists
{
    public class DeleteModel : PageModel
    {
        private readonly AccesoDatos.Models.SqlServerDbContext _context;

        public DeleteModel(AccesoDatos.Models.SqlServerDbContext context)
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

            var shoppinglist = await _context.ShoppingLists.FirstOrDefaultAsync(m => m.ShoppingListId == id);

            if (shoppinglist == null)
            {
                return NotFound();
            }
            else
            {
                ShoppingList = shoppinglist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppinglist = await _context.ShoppingLists.FindAsync(id);
            if (shoppinglist != null)
            {
                ShoppingList = shoppinglist;
                _context.ShoppingLists.Remove(ShoppingList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
