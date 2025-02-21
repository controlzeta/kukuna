using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entities
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = null!;
        public string Instructions { get; set; }
        public int Servings { get; set; }
    }
}
