using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlizaFoodCost.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public List<IngridientUsage> Ingridients { get; set; }
        public string Description { get; set; }

        public Recipe()
        {
            this.Ingridients = new List<IngridientUsage>();
        }
    }
}
