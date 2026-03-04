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

namespace WebUI.Pages.MealPlans
{
    public class IndexModel : PageModel
    {
        private readonly SqlServerDbContext _context;
        DAMealPlans proxy;
        public IndexModel(SqlServerDbContext context)
        {
            _context = context;
            proxy = new DAMealPlans(_context);
        }
        public IList<MealPlanDTO> MealPlan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MealPlan = proxy.GetMealPlansDTO();
        }
    }
}
