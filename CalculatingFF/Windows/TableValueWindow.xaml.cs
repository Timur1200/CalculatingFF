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
    /// Логика взаимодействия для TableValueWindow.xaml
    /// </summary>
    public partial class TableValueWindow : Window
    {
        public TableValueWindow()
        {
            InitializeComponent();
            _Model = TabPage1._Model;
        }
        Model1 _Model;
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
        void Load()
        {
            BettaLBox.ItemsSource = _Model.BettaList;
            SigLBox.ItemsSource = _Model.Sig2List;
        }
        private void AddBettaClick(object sender, RoutedEventArgs e)
        {
            double val = 0;
            bool OK = double.TryParse(BettaTextBox.Text,out val);
            if (OK)
            {
                _Model.BettaList.Add(val);
                Load();
                BettaTextBox.Text = "";
               
                BettaTextBox.Focus();
            }
        }

        private void RemoveBettaClick(object sender, RoutedEventArgs e)
        {
            if (BettaLBox.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать число из списка");
                return;
            }
            else
            {
                int index = BettaLBox.SelectedIndex;
                double val = Convert.ToDouble(BettaLBox.SelectedItem);
                _Model.BettaList.Remove(val);
                Load();
                
                if (index < _Model.BettaList.Count) BettaLBox.SelectedIndex = index;
                else BettaLBox.SelectedIndex = _Model.BettaList.Count - 1;
                BettaLBox.Focus();
                
            }
        }

        private void AddSigClick(object sender, RoutedEventArgs e)
        {
            double val = 0;
            bool OK = double.TryParse(SigTextBox.Text, out val);
            if (OK)
            {
                _Model.Sig2List.Add(val);
                SigTextBox.Text = "";
                Load();
                SigTextBox.Focus();
            }
        }

        private void RemoveSigClick(object sender, RoutedEventArgs e)
        {
            if (SigLBox.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать число из списка");
                return;
            }
            else
            {
                int index = SigLBox.SelectedIndex;
                double val = Convert.ToDouble(SigLBox.SelectedItem);
                _Model.Sig2List.Remove(val);
                if (index < _Model.Sig2List.Count) SigLBox.SelectedIndex = index;
                else SigLBox.SelectedIndex = _Model.Sig2List.Count - 1;
               
                Load();
                SigLBox.Focus();

            }
        }

        private void ClearBettaClick(object sender, RoutedEventArgs e)
        {
            _Model.BettaList.Clear();
            Load();
        }

        private void ClearSigClick(object sender, RoutedEventArgs e)
        {
            _Model.Sig2List.Clear();
            Load();
        }
    }
}
