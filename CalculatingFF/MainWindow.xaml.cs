using System;
using System.Windows;
using CalculatingFF.Pages;
using CalculatingFF.Windows;

namespace CalculatingFF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            TaskManager._this = new TaskManager();
            TabFrame1.NavigationService.Navigate(new TabPage1());
            TabFrame2.NavigationService.Navigate(new TabPage2());
            TabFrame3.NavigationService.Navigate(new TabPage3());
            Settings.settings.LoadFromJson();
            Settings.SyncInputData = SyncToggleBtn.IsChecked.Value;
            Settings.IsLogEnabled = LogToggleBtn.IsChecked.Value;
            Settings.ProgressBar = PBar;
        }
        
        private void StopClick(object sender, RoutedEventArgs e)
        {
            TaskManager._this.CancelAllTasks();
        }

        private void MasterToExcelClick(object sender, RoutedEventArgs e)
        {

        }

        private void SyncToggleBtnClick(object sender, RoutedEventArgs e)
        {                     
                Settings.SyncInputData = !(Settings.SyncInputData);            
        }

        private void LogToggleBtnClick(object sender, RoutedEventArgs e)
        {
            Settings.IsLogEnabled = !(Settings.IsLogEnabled);
        }

        private void SettingClick(object sender, RoutedEventArgs e)
        {
            EmptyWindow win = new EmptyWindow(new SettingPage(),"Настройки");
            win.Show();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {

        }
    }
   
}
