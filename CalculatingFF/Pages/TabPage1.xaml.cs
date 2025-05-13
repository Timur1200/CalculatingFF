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
            PoleComboBox.Items.Add("SIG1");
            PoleComboBox.Items.Add("ro0");
            PoleComboBox.SelectedIndex = 0;

        }
        public static Model1 _Model;
        TableValueWindow _TableValueWindow { get; set; }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            BtnSelection2.Visibility = Visibility.Collapsed;
        }
        private async void SelectionTableAsync()
        {
           List<TableSig> _table = new List<TableSig>();

            List<string> logList = new List<string>();
            logList.Add("Таблица SIG");
            int rowBetta = _Model.BettaList.Count;
            int colSig = _Model.Sig2List.Count;
            nums = new double[rowBetta, colSig];

            

            for (int i = 0; i < rowBetta; i++)
            {
                _Model.B = _Model.BettaList[i];
                if (Settings.IsLogEnabled) logList.Add($" \n");
                for (int j = 0; j < colSig; j++)
                {
                    _Model.B13 = _Model.Sig2List[j];
                    _Model.Selection(_isRo0);
                    nums[i, j] = _Model.B12;
                    TableSig rowSig = new TableSig {sig= Math.Round(nums[i, j], 2)
                        , psi = _Model.B6 
                        , f1 = Math.Round(_Model.D1, 2)
                        , f2 = Math.Round(_Model.D2, 2)
                        , betta = _Model.B//
                        , sig2 = _Model.B13
                        
                    }; 
                    _table.Add(rowSig);
                    if (Settings.IsLogEnabled) logList.Add($" SIG={Math.Round(nums[i, j],2)} \t PSI={_Model.B6} " +
                        $"\t F1={Math.Round(_Model.D1,2)} \t F2={Math.Round(_Model.D2,2)};"  );
                }
            }
            if (Settings.IsLogEnabled)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LogWindow logWindow;
                    logWindow = new LogWindow(logList);
                    logWindow.Show();
                });         
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                EmptyWindow emptyWindow;
                emptyWindow = new EmptyWindow(_table,nums);
                emptyWindow.Show();
            });
            }
        private async void SelectionClick(object sender, RoutedEventArgs e)
        {
            if (TableCBox.IsChecked == false)
            await TaskManager._this.RunTask(() => _Model.Selection(_isRo0));
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
           
           
            List<string> logList = new List<string>();
            logList.Add("Таблица SIG");
            int rowBetta = _Model.BettaList.Count;
            int colSig = _Model.Sig2List.Count;
            nums = new double[rowBetta, colSig];
            for (int i = 0; i < rowBetta; i++)
            {
                _Model.B = _Model.BettaList[i];
                if (Settings.IsLogEnabled) logList.Add($" \n");
                for (int j = 0; j < colSig; j++)
                {
                    _Model.B13 = _Model.Sig2List[j];
                    _Model.SelectionSimplex();
                    nums[i, j] = _Model.B12;
                    if (Settings.IsLogEnabled) logList.Add($" SIG={Math.Round(nums[i, j], 2)} PSI={_Model.B6} \t \t F1={Math.Round(_Model.D1, 2)} \t F2={Math.Round(_Model.D2, 2)};");

                }
            }
            if (Settings.IsLogEnabled)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LogWindow logWindow;
                    logWindow = new LogWindow(logList);
                    logWindow.Show();
                });
            }
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

        

        private void TableClick(object sender, RoutedEventArgs e)
        {
            _TableValueWindow = new TableValueWindow();
            _TableValueWindow.ShowDialog();
        }
        bool _isRo0 = false;
        private void PoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PoleComboBox.SelectedIndex == 0)
            {//SIG1
                _isRo0 = false;
            }
            else if (PoleComboBox.SelectedIndex == 1)
            {//ro0
                _isRo0 = true;
            }
        }
    }
}
