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
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();           
        }
        public LogWindow(List<string> logList)
        {
            InitializeComponent();
            _LogList = logList;
            LogListBox.ItemsSource = _LogList;
        }
        public List<string> _LogList = new List<string>();
        private void WinLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
