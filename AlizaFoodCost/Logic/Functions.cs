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
        private const string ImagesFolderPath = "../../Images";
        public static void CreateUpdateIngridientsXml<T>(Type dataType, T newItem)
        {
            CheckCreateDatasetDirectories();

            if (dataType == typeof(Ingridient))
            {
                List<Ingridient> ingridientsList = new List<Ingridient>();
                bool isFileExists = CheckIfDatasetFileExists(IngridientsXmlFilePath);
                if (isFileExists)
                {
                    ingridientsList = GetDataFromFile<List<Ingridient>>(IngridientsXmlFilePath);
                }
                (newItem as Ingridient).Id = ingridientsList.Count + 1;
                ingridientsList.Add(newItem as Ingridient);
                FileInfo fi = new FileInfo((newItem as Ingridient).ImagePath);
                string imageName = fi.Name;
                string imagesFolderPath = CheckCreateSpecificImagesDirectory("Ingridients");
                if (!File.Exists($"{imagesFolderPath}\\{imageName}"))
                {
                    File.Copy((newItem as Ingridient).ImagePath, $"{imagesFolderPath}\\{imageName}");
                }
                (newItem as Ingridient).ImagePath = Path.Combine(imagesFolderPath, imageName);
                CreateUpdateFile<List<Ingridient>>(IngridientsXmlFilePath, ingridientsList);
            }

        }

        public static List<Ingridient> GetIngridientsList()
        {
            return GetDataFromFile<List<Ingridient>>(IngridientsXmlFilePath);
        }

        private static void CheckCreateDatasetDirectories()
        {
            if (!Directory.Exists(DataSetFolderPath))
            {
                Directory.CreateDirectory(DataSetFolderPath);
            }

            if (!Directory.Exists(ImagesFolderPath))
            {
                Directory.CreateDirectory(ImagesFolderPath);
            }
        }

        private static string CheckCreateSpecificImagesDirectory(string folderName)
        {
            string fullPath = $@"{ImagesFolderPath}/{folderName}";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }


        private static T GetDataFromFile<T>(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(sr);
            }
        }

        private static void CreateUpdateFile<T>(string filePath, T data)
        {
            TextWriter tw = new StreamWriter(filePath);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(tw, data);
            tw.Dispose();

        }
        private static bool CheckIfDatasetFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            return true;
        }
    }
}
