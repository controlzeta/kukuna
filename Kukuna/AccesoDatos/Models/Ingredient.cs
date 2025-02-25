using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }
    [Display(Name = "Ingrediente")]
    public string IngredientName { get; set; } = null!;
    [Display(Name = "Categoría")]
    public string? Category { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
}
