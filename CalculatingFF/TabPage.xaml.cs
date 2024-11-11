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

    public partial class TabPage : Page
    {
        public TabPage()
        {
            InitializeComponent();
            _Model = new Model2(true);
            DataContext = _Model;
          
            
        }

        Model2 _Model;
        private void SelectionClick(object sender, RoutedEventArgs e)
        {
            _Model.Selection();
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
    }
}
