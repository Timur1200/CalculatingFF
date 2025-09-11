using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            DataContext = Settings.settings;
            Settings.settings.LoadFromJson();


        }
        Settings _settings = Settings.settings;
        private void SaveToJson_Click(object sender, RoutedEventArgs e)
        {
            SaveToJson();
        }

        public void SaveToJson()
        {
           
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string json = JsonSerializer.Serialize((Settings.settings), options);
                 //json = JsonSerializer.Serialize(Settings.Tolerance, options);
                File.WriteAllText("settings.json", json);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{_settings.Step} {_settings.Tolerance}");
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            _settings.Step = 0.05;
            _settings.Tolerance = 0.01;

        }
    }
}
