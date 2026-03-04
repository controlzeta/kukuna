using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;

namespace WebUI.Pages.Ingredients
{
    public class DetailsModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DAIngredients proxy;

        public DetailsModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DAIngredients(_context);
        }

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
            else
            {
                Ingredient = ingredient;
            }
            return Page();
        }
    }
}
