using CalculatingFF.Pages;
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
using System.Windows.Shapes;

namespace CalculatingFF.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmptyWindow.xaml
    /// </summary>
    public partial class EmptyWindow : Window
    {
        public EmptyWindow()
        {
            InitializeComponent();
        }
        public EmptyWindow(List<TableSig> table, double[,] nums)
        {
            InitializeComponent();
            WinFrame.Navigate(new TableSigPage(table, nums));
        }

    }
}
