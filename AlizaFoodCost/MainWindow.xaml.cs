using AlizaFoodCost.Logic;
using AlizaFoodCost.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlizaFoodCost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Ingridient> Ingridients = new List<Ingridient>();
        public MainWindow()
        {
            InitializeComponent();
        }

        string selectedFilePath = string.Empty;

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_New_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            Grid_Ingridients_Menu.Visibility = Visibility.Hidden;
            Grid_New_Ingridient.Visibility = Visibility.Visible;
            Grid_Update_Existing_Ingridient.Visibility = Visibility.Hidden;
        }

        private void Btn_Edit_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            Grid_Ingridients_Menu.Visibility = Visibility.Hidden;
            Grid_New_Ingridient.Visibility = Visibility.Hidden;
            Grid_Update_Existing_Ingridient.Visibility = Visibility.Visible;

            Ingridients = Functions.GetIngridientsList();
            foreach (Ingridient ingridient in this.Ingridients)
            {
                bool itemExists = false;
                if (Cb_Update_Ingridients.Items.Count > 0)
                {

                    foreach (ComboBoxItem cbi in Cb_Update_Ingridients.Items)
                    {
                        itemExists = cbi.Content.Equals(ingridient.Name);
                        if (itemExists)
                        {
                            break;
                        }
                        Cb_Update_Ingridients.Items.Add(new ComboBoxItem() { Content = ingridient.Name });
                    }
                }
                else
                {
                    Cb_Update_Ingridients.Items.Add(new ComboBoxItem() { Content = ingridient.Name });
                }
            }
        }

        private void Btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                selectedFilePath = ofd.FileName;
                Img_New_Product.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void Btn_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Ingridients_Menu.Visibility = Visibility.Visible;
        }

        private void Btn_Add_New_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            Ingridient newIngridient = new Ingridient()
            {
                Name = this.Tb_New_Ingridient_Name.Text,
                ImagePath = selectedFilePath,
                MeasurmentUnit = (MeasurmentUnit)Select_New_Ingridient_M_Unit.SelectedIndex,
                Price = decimal.Parse(Tb_New_Ingridient_Price.Text),
                UpsertDate = DateTime.Now
            };

            Functions.CreateUpdateIngridientsXml<Ingridient>(typeof(Ingridient), newIngridient);
        }

        private void Cb_Update_Ingridients_Selected(object sender, RoutedEventArgs e)
        {
            string selectedComboboxValue = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            Ingridient selectedIngridient = Ingridients.Where(i => i.Name == selectedComboboxValue).FirstOrDefault();
            string imagePath = System.IO.Path.GetFullPath(selectedIngridient.ImagePath);
            Image_Selected_Ingridient.Source = new BitmapImage(new Uri(imagePath));
        }
    }
}
