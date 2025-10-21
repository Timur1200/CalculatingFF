using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Border = System.Windows.Controls.Border;
using Color = System.Windows.Media.Color;

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
            ColorComboBoxInit(colorComboBox);
            ColorComboBoxInit(colorComboBox1);
            ColorComboBoxInit(colorComboBox2);
            ColorComboBoxInit(colorComboBox3);

            SelectColorByName(colorComboBox, _settings.Color);
            SelectColorByName(colorComboBox1, _settings.Color1);
            SelectColorByName(colorComboBox2, _settings.Color2);
            SelectColorByName(colorComboBox3, _settings.Color3);
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

        void ColorComboBoxInit(ComboBox cb)
        {
            var colorProperties = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (var property in colorProperties)
            {
                if (property.PropertyType == typeof(Color))
                {
                    var color = (Color)property.GetValue(null);
                    var brush = new SolidColorBrush(color);

                    // Создаем контейнер для отображения
                    var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                    // Добавляем цветной прямоугольник
                    var border = new Border
                    {
                        Width = 20,
                        Height = 20,
                        Background = brush,
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(0, 0, 10, 0)
                    };

                    // Добавляем текст с названием
                    var textBlock = new TextBlock
                    {
                        Text = property.Name,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    stackPanel.Children.Add(border);
                    stackPanel.Children.Add(textBlock);

                    // Сохраняем кисть в Tag для дальнейшего использования
                    stackPanel.Tag = brush;

                    cb.Items.Add(stackPanel);
                   
                }
            }

            //colorComboBox.SelectedIndex = 0;
        }

       
        private void SelectColorByName(ComboBox cb,string colorName)
        {
           
            foreach (StackPanel item in cb.Items)
            {
                if (item.Children[1] is TextBlock textBlock && textBlock.Text == colorName)
                {
                    cb.SelectedItem = item;
                    return;
                }
            }

            // Если цвет не найден, выбираем первый элемент
            cb.SelectedIndex = 0;
        }
        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem is StackPanel stackPanel && stackPanel.Tag is SolidColorBrush selectedBrush)
            {


                // Или можно получить название цвета из TextBlock
                if (stackPanel.Children[1] is TextBlock textBlock)
                {
                    string colorName = textBlock.Text;
                    _settings.Color = colorName;
                    // MessageBox.Show($"Выбран цвет: {colorName}");
                }
            }
        }

        private void colorComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem is StackPanel stackPanel && stackPanel.Tag is SolidColorBrush selectedBrush)
            {
       

                // Или можно получить название цвета из TextBlock
                if (stackPanel.Children[1] is TextBlock textBlock)
                {
                    string colorName = textBlock.Text;
                    _settings.Color1 = colorName;
                    // MessageBox.Show($"Выбран цвет: {colorName}");
                }
            }
        }

        private void colorComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem is StackPanel stackPanel && stackPanel.Tag is SolidColorBrush selectedBrush)
            {
             

                // Или можно получить название цвета из TextBlock
                if (stackPanel.Children[1] is TextBlock textBlock)
                {
                    string colorName = textBlock.Text;
                    _settings.Color2 = colorName;
                    // MessageBox.Show($"Выбран цвет: {colorName}");
                }
            }
        }

        private void colorComboBox_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem is StackPanel stackPanel && stackPanel.Tag is SolidColorBrush selectedBrush)
            {
           

                // Или можно получить название цвета из TextBlock
                if (stackPanel.Children[1] is TextBlock textBlock)
                {
                    string colorName = textBlock.Text;
                    _settings.Color3 = colorName;
                    // MessageBox.Show($"Выбран цвет: {colorName}");
                }
            }
        }

        private void ResetClickColor(object sender, RoutedEventArgs e)
        {
            _settings.Color = "LightGreen";
            _settings.Color1 = "Yellow";
            _settings.Color2 = "DarkOrange";
            _settings.Color3 = "Red";
            SelectColorByName(colorComboBox, _settings.Color);
            SelectColorByName(colorComboBox1, _settings.Color1);
            SelectColorByName(colorComboBox2, _settings.Color2);
            SelectColorByName(colorComboBox3, _settings.Color3);
        }
    }
}
