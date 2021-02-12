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
        public const string HundredGrams = "מחיר נוכחי למאה גרם";
        public const string Kilo = "מחיר נוכחי לקילו";
        public const string Unit = "מחיר נוכחי ליחידה";
        public const string Liter = "מחיר נוכחי לליטר";
        public const string Milliliter = "מחיר נוכחי למילי-ליטר";

        public static string GetUnitHebrewValue(MeasurmentUnit unit)
        {
            switch (unit)
            {
                case MeasurmentUnit.HundredGrams:
                    return HebMeasurmentValue(HundredGrams);
                case MeasurmentUnit.Kilo:
                    return HebMeasurmentValue(Kilo);
                case MeasurmentUnit.Unit:
                    return HebMeasurmentValue(Unit);
                case MeasurmentUnit.Liter:
                    return HebMeasurmentValue(Liter);
                case MeasurmentUnit.Milliliter:
                    return HebMeasurmentValue(Milliliter);
                default:
                    return null;
            }
        }

        private static string HebMeasurmentValue(string value)
        {
            return value.Replace("מחיר נוכחי ל", "");
        }
    }
}
