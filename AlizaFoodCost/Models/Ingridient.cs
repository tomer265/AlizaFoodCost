using AlizaFoodCost.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlizaFoodCost.Models
{
    public class Ingridient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public MeasurmentUnit MeasurmentUnit { get; set; }
        public DateTime UpsertDate { get; set; }
    }
}
