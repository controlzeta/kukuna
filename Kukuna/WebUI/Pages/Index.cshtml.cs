using AccesoDatos;
using AccesoDatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public int ingridientId { get; set; }
        public List<SelectListItem> ingredients { get; set; }
        public List<Ingredient> lstIngredients { get; set; }
        private DAIngredients proxy;
        
        [BindProperty]
        public Ingredient ingredient { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            proxy = new DAIngredients();
        }

        public void OnGet()
        {
            lstIngredients = proxy.GetIngredients();
            ingredients = lstIngredients.Select(m =>
            new SelectListItem { Text = m.IngredientName, Value = m.IngredientId.ToString() }).ToList();
            //ingredient = proxy.GetIngredientsById((int)1);
        }


        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id != null)
        //    {

        //        ingredient = proxy.GetIngredientsById((int)id);

        //        if (ingredient == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return Page();
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ingredient != null)
            {
                if (proxy.InsertIngredient(ingredient.IngredientName, ingredient.Category))
                {
                    ingredient = new Ingredient();
                }
            }
            lstIngredients = proxy.GetIngredients();
            return RedirectToPage("./Index");
        }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    DAIngredients proxy = new DAIngredients();
        //    lstIngredients = proxy.GetIngredients();
        //    ingredients = lstIngredients.Select(m =>
        //    new SelectListItem { Text = m.IngredientName, Value = m.IngredientId.ToString() }).ToList        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ingredient = proxy.GetIngredientsById((int)id);

        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}
    }
}
