using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;
using AccesoDatos;
using Microsoft.Data.SqlClient;

namespace WebUI.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        private readonly DAIngredients proxy;

        public IndexModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DAIngredients(_context);
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ingredient = proxy.GetIngredients();
        }
    }
}
