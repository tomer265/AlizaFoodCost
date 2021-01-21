using AlizaFoodCost.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AlizaFoodCost.Logic
{
    public static class Functions
    {
        private const string IngridientsXmlFilePath = "../../Data Sets/Ingridients DataSet.xml";
        private const string DataSetFolderPath = "../../Data Sets";
        public static void CheckCreateIngridientsXml()
        {
            CheckCreateDatasetDirectory();

            CheckCreateDatasetFile<List<Ingridient>>(IngridientsXmlFilePath, typeof(List<Ingridient>));

        }

        private static void CheckCreateDatasetDirectory()
        {
            if (!Directory.Exists(IngridientsXmlFilePath))
            {
                Directory.CreateDirectory(DataSetFolderPath);
            }
        }

        private static void CreateFile<T>(string filePath, T data)
        {
            TextWriter tw = new StreamWriter(filePath);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(tw, data);

        }
        private static T CheckCreateDatasetFile<T>(string filePath, Type dataType)
        {
            if (!File.Exists(filePath))
            {
                CreateFile(filePath, dataType);
            }
        }
    }
}
