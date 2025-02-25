﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public  class ShoppingListDTO
    {
        public int ShoppingListId { get; set; }

        public int? IngredientId { get; set; }
        [Display(Name = "Ingrediente")]
        public string IngredientName { get; set; }

        public int? UnitId { get; set; }
        [Display(Name = "Unidad")]
        public string UnitName { get; set; }
        [Display(Name = "Cantidad")]
        public decimal TotalQuantity { get; set; }
    }
}
