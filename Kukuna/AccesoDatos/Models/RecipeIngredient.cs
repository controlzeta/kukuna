using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }

    public int? RecipeId { get; set; }

    public int? IngredientId { get; set; }

    public int? UnitId { get; set; }

    public decimal Quantity { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual Unit? Unit { get; set; }
}
