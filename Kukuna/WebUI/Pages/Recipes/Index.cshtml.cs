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

namespace WebUI.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DARecipes proxy;

        public IndexModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DARecipes(_context);
        }

        public IList<Recipe> Recipe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Recipe = proxy.GetRecipes();
        }
    }
}
