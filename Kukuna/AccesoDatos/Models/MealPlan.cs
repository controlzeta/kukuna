using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class MealPlan
{
    public int MealPlanId { get; set; }

    public DateOnly DayOfWeek { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
