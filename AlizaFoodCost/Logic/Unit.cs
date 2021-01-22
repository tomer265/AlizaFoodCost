using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlizaFoodCost.Logic
{
    public enum MeasurmentUnit
    {
        Gram = 0,
        HundredGrams = 1,
        Kilo = 2,
        Unit = 3,
        Liter = 4,
        Milliliter = 5
    }

    public partial class MeasurmentUnitHeb
    {
        public const string Gram = "מחיר נוכחי לגרם";
        public const string HundredGrams = "מחיר נוכחי למאה גרם";
        public const string Kilo = "מחיר נוכחי לקילו";
        public const string Unit = "מחיר נוכחי ליחידה";
        public const string Liter = "מחיר נוכחי לליטר";
        public const string Milliliter = "מחיר נוכחי למילי-ליטר";
    }
}
