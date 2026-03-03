using AccesoDatos;
using AccesoDatos.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.ShoppingLists
{
    public class IndexModel : PageModel
    {
        private readonly DAShoppingLists _shoppingListsProxy;

        public IndexModel(DAShoppingLists shoppingListsProxy)
        {
            _shoppingListsProxy = shoppingListsProxy;
        }

        public IList<ShoppingListDTO> ShoppingList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ShoppingList = await _shoppingListsProxy.GetShoppingListsAsync();
        }
    }
}
