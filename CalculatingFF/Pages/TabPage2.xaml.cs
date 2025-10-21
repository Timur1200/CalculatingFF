using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatingFF
{

    public partial class TabPage2 : Page
    {
        public TabPage2()
        {
            InitializeComponent();
            _Model = new Model2(true);
            DataContext = _Model;
            
            
        }

        public static Model2 _Model;
        private void SelectionClick(object sender, RoutedEventArgs e)
        {
           TaskManager._this.RunTask(() => _Model.Selection());
        }
        private void SolveClick(object sender, RoutedEventArgs e)
        {
            _Model.Solve();
        }

      
        private void ToExcelClick(object sender, RoutedEventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.ToExcel2(_Model);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Settings.SyncInputData)
            {
                _Model = Settings.SyncModel(TabPage3._Model3, _Model);
            }
        }
        public void ChangeColor()
        {
            

            if (_Model.F < (double)3)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color);
                    Fbox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    Fbox.Background = new SolidColorBrush(Colors.LightGreen);
                }
            }
            else if (_Model.F < (double)6)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color1);
                    Fbox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    Fbox.Background = new SolidColorBrush(Colors.Yellow);
                }

            }
            else if (_Model.F < (double)9)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color2);
                    Fbox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    Fbox.Background = new SolidColorBrush(Colors.DarkOrange);
                }

            }
            else
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(Settings.settings.Color3);
                    Fbox.Background = new SolidColorBrush(color);
                }
                catch
                {
                    // Если цвет не распознан, используем цвет по умолчанию
                    Fbox.Background = new SolidColorBrush(Colors.Red);
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColor();
        }
    }
}
