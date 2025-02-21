using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public  class RecipeIngredientDTO
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
    }
}
