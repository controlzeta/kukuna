using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    [Display(Name = "Receta")]
    public string RecipeName { get; set; } = null!;
    [Display(Name = "Instrucciones")]
    public string? Instructions { get; set; }
    [Display(Name = "Porciones")]
    public int? Servings { get; set; }

    public virtual ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
