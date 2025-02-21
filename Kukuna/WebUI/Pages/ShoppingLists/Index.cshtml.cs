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

namespace WebUI.Pages.ShoppingLists
{
    public class IndexModel : PageModel
    {
        DAShoppingLists proxy = new DAShoppingLists();


        public IList<ShoppingListDTO> ShoppingList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ShoppingList = proxy.GetShoppingLists();
        }
    }
}
