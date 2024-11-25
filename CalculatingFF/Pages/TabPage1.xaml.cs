using CalculatingFF.Windows;
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

            _Model = new Model1(true);
            DataContext = _Model;
            
        }
        public static Model1 _Model;
        TableValueWindow _TableValueWindow { get; set; }

        private async void SelectionTableAsync()
        {
            bool IsLog = Settings.IsLogEnabled;
            List<string> logList = new List<string>();
            logList.Add("Таблица SIG");
            int rowBetta = _Model.BettaList.Count;
            int colSig = _Model.Sig2List.Count;
            nums = new double[rowBetta, colSig];
            for (int i = 0; i < rowBetta; i++)
            {
                _Model.B = _Model.BettaList[i];
                if (IsLog) logList.Add($" \n");
                for (int j = 0; j < colSig; j++)
                {
                    _Model.B13 = _Model.Sig2List[j];
                    _Model.Selection();
                    nums[i, j] = _Model.B12;
                    if (IsLog) logList.Add($" SIG={Math.Round(nums[i, j],2)} \t F1={Math.Round(_Model.D1,2)} \t F2={Math.Round(_Model.D2,2)};"  );
                }
            }
            if (IsLog)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LogWindow logWindow;
                    logWindow = new LogWindow(logList);
                    logWindow.Show();
                });
                
                
            }
        }
        private async void SelectionClick(object sender, RoutedEventArgs e)
        {
            if (TableCBox.IsChecked == false)
            await TaskManager._this.RunTask(() => _Model.Selection());
            else
            {
                await TaskManager._this.RunTask(() => SelectionTableAsync());
            }
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_Model.TestPrint());
        }

        private void ToExcelClick(object sender, RoutedEventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.ToExcel1(_Model ,nums);
        }


        double[,] nums;
        private async void SelectionSimplexTableAsync()
        {
            bool IsLog = true;

            int rowBetta = _Model.BettaList.Count;
            int colSig = _Model.Sig2List.Count;
            nums = new double[rowBetta, colSig];
            for (int i = 0; i < rowBetta; i++)
            {
                _Model.B = _Model.BettaList[i];
                for (int j = 0; j < colSig; j++)
                {
                    _Model.B13 = _Model.Sig2List[j];
                    _Model.SelectionSimplex();
                    nums[i, j] = _Model.B12;

                }
            }


            return;
            string testString = "";
            for (int i = 0; i < rowBetta; i++)
            {
                testString = testString + "\n";
                for (int j = 0; j < colSig; j++)
                {
                    testString = testString + ($" {nums[i, j]} ");

                }
            }
            MessageBox.Show(testString);
        }

        private async void SelectionSimplexClick(object sender, RoutedEventArgs e)
        {
            if (TableCBox.IsChecked == true)
            {
                await TaskManager._this.RunTask(() => SelectionSimplexTableAsync());
            }
            else
            {
                await TaskManager._this.RunTask(() => _Model.SelectionSimplex());
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TableClick(object sender, RoutedEventArgs e)
        {
            _TableValueWindow = new TableValueWindow();
            _TableValueWindow.ShowDialog();
        }
    }
}
