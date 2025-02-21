using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public class MealPlanDTO
    {
        public int MealPlanId { get; set; }

        public DateOnly DayOfWeek { get; set; }
        public string DayName { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
    }
}
