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
        Model3 _Model3 { get; set; }

        private void SelectionClick(object sender, RoutedEventArgs e)
        {
            _Model3.Selection();
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            _Model3.Solve();
        }
    }
}
