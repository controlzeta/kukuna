using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class ShoppingList
{
    public int ShoppingListId { get; set; }

    public int? IngredientId { get; set; }

    public int? UnitId { get; set; }

    public decimal TotalQuantity { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual Unit? Unit { get; set; }
}
