using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CalculatingFF
{
    class Settings : INotifyPropertyChanged
    {
       
        public Settings()
        {
                        
        }

        /// <summary>
        /// синхронизация входных данных между 2 и 3 моделью
        /// </summary>
        public static bool SyncInputData { get; set; }
        /// <summary>
        /// Отвечает за создание окна с логами
        /// </summary>
        public static bool IsLogEnabled { get; set; }
        public static ProgressBar ProgressBar { get; set; }

        private  double step { get; set; }
        public  double Step { get { return step; } set { step = value; settings.OnPropertyChanged("Step"); } }

        public  double Tolerance { get { return tolerance; } set { tolerance = value;settings.OnPropertyChanged("Tolerance"); } }
        private  double tolerance { get; set; }

        private string color { get; set; }
        public string Color { get { return color; } set { color = value; settings.OnPropertyChanged("Color"); } }
        private string color1 { get; set; }
        public string Color1 { get { return color1; } set { color1 = value; settings.OnPropertyChanged("Color1"); } }
        private string color2 { get; set; }
        public string Color2 { get { return color2; } set { color2 = value; settings.OnPropertyChanged("Color2"); } }
        private string color3 { get; set; }
        public string Color3 { get { return color3; } set { color3 = value; settings.OnPropertyChanged("Color3"); } }

        public static Model3 SyncModel(Model2 m2,Model3 m3) 
        {
            //m3 => m2
           
            m3.S11 = m2.S11;
            m3.S31 = m2.S31;
            m3.A1 = m2.A1;
            m3.A2 = m2.A2;
            m3.A3 = m2.A3;
            m3.S13 = m2.S13;
            m3.S33 = m2.S33;
            return m3;
        }
        public static Model2 SyncModel(Model3 m3, Model2 m2)
        {           
            m2.S11 = m3.S11;
            m2.S31 = m3.S31;
            m2.A1 = m3.A1;
            m2.A2 = m3.A2;
            m2.A3 = m3.A3;
            m2.S13 = m3.S13;
            m2.S33 = m3.S33;
            return m2;
        }

        public static Settings settings = new Settings();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void LoadFromJson()
        {
            try
            {
                if (File.Exists("settings.json"))
                {
                    string json = File.ReadAllText("settings.json");
                    var loadedSettings = JsonSerializer.Deserialize<Settings>(json);

                    settings.Tolerance = loadedSettings.Tolerance;
                    settings.Step = loadedSettings.Step;
                    settings.Color = loadedSettings.Color;
                    settings.Color1 = loadedSettings.Color1;
                    settings.Color2 = loadedSettings.Color2;
                    settings.Color3 = loadedSettings.Color3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}");
            }
        }
    }
}
