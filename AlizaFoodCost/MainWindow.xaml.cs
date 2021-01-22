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
            HideAllGridsButSelected(Grid_New_Ingridient);
        }

        private void Btn_Edit_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            HideAllGridsButSelected(Grid_Update_Existing_Ingridient);
            Ingridients = Functions.GetIngridientsList();
            Cb_Update_Ingridients.Items.Clear();
            foreach (Ingridient ingridient in this.Ingridients)
            {
                Cb_Update_Ingridients.Items.Add(new ComboBoxItem() { Content = ingridient.Name });
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
            HideAllGridsButSelected(Grid_Ingridients_Menu);
        }

        private void Btn_Add_New_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            HideAllGridsButSelected(Grid_New_Ingridient);
            Ingridient newIngridient = new Ingridient()
            {
                Name = this.Tb_New_Ingridient_Name.Text,
                ImagePath = selectedFilePath,
                MeasurmentUnit = (MeasurmentUnit)Select_New_Ingridient_M_Unit.SelectedIndex,
                Price = decimal.Parse(Tb_New_Ingridient_Price.Text),
                UpsertDate = DateTime.Now
            };



            Ingridients = Functions.GetIngridientsList();
            if (Ingridients.Select(i => i.Name == newIngridient.Name).Any())
            {
                MessageBox.Show("כבר קיים מוצר בשם " + newIngridient.Name + ". יש לבחור שם אחר או לערוך את חומר הגלם .הקיים",
                    "תקלה - מוצר קיים",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign);
                return;
            }

            Functions.CreateUpdateIngridientsXml<Ingridient>(typeof(Ingridient), newIngridient);

            Img_New_Product.Source = null;
            Tb_New_Ingridient_Name.Text = string.Empty;
            Tb_New_Ingridient_Price.Text = string.Empty;
            Select_New_Ingridient_M_Unit.SelectedIndex = 0;


        }

        private void Cb_Update_Ingridients_Selected(object sender, RoutedEventArgs e)
        {
            HideAllGridsButSelected(Grid_Update_Selected_Ingridient, Grid_Update_Existing_Ingridient);
            string selectedComboboxValue = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            Ingridient selectedIngridient = Ingridients.Where(i => i.Name == selectedComboboxValue).FirstOrDefault();
            string imagePath = System.IO.Path.GetFullPath(selectedIngridient.ImagePath);
            Image_Selected_Ingridient.Source = new BitmapImage(new Uri(imagePath));

            switch (selectedIngridient.MeasurmentUnit)
            {
                case MeasurmentUnit.Gram:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.Gram;
                    break;
                case MeasurmentUnit.HundredGrams:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.HundredGrams;
                    break;
                case MeasurmentUnit.Kilo:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.Kilo;
                    break;
                case MeasurmentUnit.Liter:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.Liter;
                    break;
                case MeasurmentUnit.Milliliter:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.Milliliter;
                    break;
                case MeasurmentUnit.Unit:
                    Lbl_Update_Selected_Ingridient_Price.Content = MeasurmentUnitHeb.Unit;
                    break;
            }

            Tb_Update_Selected_Ingridient_Price.Text = selectedIngridient.Price.ToString();

        }

        private void HideAllGridsButSelected(params Grid[] gridsToShow)
        {
            Grid_Welcome.Visibility = Visibility.Hidden;
            Grid_Ingridients_Menu.Visibility = Visibility.Hidden;
            Grid_New_Ingridient.Visibility = Visibility.Hidden;
            Grid_Update_Existing_Ingridient.Visibility = Visibility.Hidden;
            Grid_Update_Selected_Ingridient.Visibility = Visibility.Hidden;
            foreach (Grid gridToShow in gridsToShow)
            {
                gridToShow.Visibility = Visibility.Visible;
            }

        }

        private void Btn_Update_Ingridient_Click(object sender, RoutedEventArgs e)
        {
            string selectedComboboxValue = ((Cb_Update_Ingridients).SelectedItem as ComboBoxItem).Content as string;
            Ingridients.First(i => i.Name == selectedComboboxValue).Price = decimal.Parse(Tb_Update_Selected_Ingridient_Price.Text);

            Functions.CreateUpdateIngridientsXml<List<Ingridient>>(typeof(List<Ingridient>), Ingridients);

            MessageBox.Show("המחיר לחומר הגלם " + selectedComboboxValue + " עודכן בהצלחה", "הודעה על עדכון חומר גלם",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign);
        }
    }
}
