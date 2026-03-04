using AccesoDatos;
using AccesoDatos.Entities;
using AccesoDatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly DAShoppingLists _shoppingListsProxy;
        private readonly SqlServerDbContext _context;

        public CatalogController(ILogger<CatalogController> logger, DAShoppingLists shoppingListsProxy, SqlServerDbContext context)
        {
            _logger = logger;
            _shoppingListsProxy = shoppingListsProxy;
            _context = context;
        }

        [HttpGet]
        [Route("GetIngredients")]
        public IEnumerable<Ingredient> GetIngredients()
        {
            DAIngredients proxy = new DAIngredients(_context);

            return proxy.GetIngredients();
        }

        [HttpGet]
        [Route("GetRecipes")]
        public IEnumerable<Recipe> GetRecipes()
        {
            DARecipes proxy = new DARecipes(_context);

            return proxy.GetRecipes();
        }

        [HttpGet]
        [Route("GetMealPlans")]
        public IEnumerable<MealPlan> GetMealPlans()
        {
            DAMealPlans proxy = new DAMealPlans(_context);

            return proxy.GetMealPlans();
        }

        [HttpGet]
        [Route("GetRecipeIngredients")]
        public IEnumerable<RecipeIngredientDTO> GetRecipeIngredients()
        {
            DARecipeIngredients proxy = new DARecipeIngredients(_context);

            return proxy.GetRecipeIngredientsDTO();
        }

        [HttpGet]
        [Route("GetShoppingLists")]
        public IEnumerable<ShoppingListDTO> GetShoppingLists()
        {
            return (IEnumerable<ShoppingListDTO>)_shoppingListsProxy.GetShoppingListsAsync();
        }

        [HttpGet]
        [Route("GetUnits")]
        public IEnumerable<Unit> GetUnits()
        {
            DAUnits proxy = new DAUnits();

            return proxy.GetUnits();
        }
    }
}
