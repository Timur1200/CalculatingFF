using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CalculatingFF.Pages
{
    /// <summary>
    /// Логика взаимодействия для TabPage1.xaml
    /// </summary>
    public partial class TabPage1 : Page
    {
        public TabPage1()
        {
            InitializeComponent();
            _model = new Model1(true);
            DataContext = _model;
        }
        Model1 _model;
        
        private async void SelectionClick(object sender, RoutedEventArgs e)
        {
            await TaskManager._this.RunTask(() => _model.Selection());
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_model.TestPrint());
        }

        private void ToExcelClick(object sender, RoutedEventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.ToExcel1(_model);
        }

       

        private async void SelectionSimplexClick(object sender, RoutedEventArgs e)
        {
            await TaskManager._this.RunTask(() => _model.SelectionSimplex());
            
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
