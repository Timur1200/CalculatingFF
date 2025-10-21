using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatingFF.Pages
{
    /// <summary>
    /// Логика взаимодействия для HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = System.IO.Path.Combine(appDirectory, "Help.htm");

            // Создаем абсолютный URI
            Uri pdfUri = new Uri(pdfPath, UriKind.Absolute);

            // Загружаем в WebBrowser
            webBrowser.Navigate(pdfUri);

        }
    }
}
