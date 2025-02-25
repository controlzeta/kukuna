using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public class MealPlanDTO
    {
        public int MealPlanId { get; set; }
        [Display(Name = "Fecha")]
        public DateOnly DayOfWeek { get; set; }
        [Display(Name = "Día de la Semana")]
        public string DayName { get; set; }
        public int RecipeId { get; set; }
        [Display(Name = "Receta")]
        public string RecipeName { get; set; }
    }
}
