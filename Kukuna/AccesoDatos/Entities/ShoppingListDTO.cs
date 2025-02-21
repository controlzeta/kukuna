using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public  class ShoppingListDTO
    {
        public int ShoppingListId { get; set; }

        public int? IngredientId { get; set; }
        public string IngredientName { get; set; }

        public int? UnitId { get; set; }
        public string UnitName { get; set; }

        public decimal TotalQuantity { get; set; }
    }
}
