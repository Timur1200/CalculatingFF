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

namespace CalculatingFF
{
    /// <summary>
    /// Логика взаимодействия для TabPage3.xaml
    /// </summary>
    public partial class TabPage3 : Page
    {
        public TabPage3()
        {
            InitializeComponent();
            _Model3 = new Model3(true); 
            DataContext = _Model3;
        }
        public static Model3 _Model3 { get; set; }

        private void SelectionClick(object sender, RoutedEventArgs e)
        {
           TaskManager._this.RunTask(()=> _Model3.Selection());
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            _Model3.Solve();
        }

        private void ToExcelClick(object sender, RoutedEventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.ToExcel3(_Model3);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Settings.SyncInputData)
            {
                _Model3 = Settings.SyncModel( TabPage2._Model, _Model3);
            }
        }

       
        private void box_TextChanged(object sender, TextChangedEventArgs e)
        {
            double diff = _Model3.BestError;

            if (_Model3.BestError < (double)3)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color);
                    diffBox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    diffBox.Background = new SolidColorBrush(Colors.LightGreen);
                }
            }
            else if (_Model3.BestError < (double)6)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color1);
                    diffBox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    diffBox.Background = new SolidColorBrush(Colors.Yellow);
                }

            }
            else if (_Model3.BestError < (double)9)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color2);
                    diffBox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    diffBox.Background = new SolidColorBrush(Colors.DarkOrange);
                }

            }
            else
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color3);
                    diffBox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    diffBox.Background = new SolidColorBrush(Colors.Red);
                }
            }

        }
    }
}
