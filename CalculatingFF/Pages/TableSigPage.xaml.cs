using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
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
using System.Diagnostics;

namespace CalculatingFF.Pages
{
    /// <summary>
    /// Логика взаимодействия для TableSigPage.xaml
    /// </summary>
    public partial class TableSigPage : Page
    {
        public TableSigPage(List<TableSig> table,double[,] nums )
        {
            InitializeComponent();
            _table = table;
            
            DGrid.ItemsSource = _table;
            List<List<TableSig>> list = new List<List<TableSig>>();
            for (int i=0;i< TabPage1._Model.BettaList.Count; i++)
            {
                list.Add(new List<TableSig>());
            }
            MessageBox.Show($"Count(B) list = {list.Count}");
            double lastValue = 0;int j = 0;
            foreach (var item in _table)
            {
                if (lastValue != item.betta)
                {
                    lastValue = item.betta;
                    j = 0;
                }
                list[j].Add(item);
                j++;

            }

            PlotModel = new PlotModel { Title = "График" };

            // Добавление осей (опционально)
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Betta" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Sig" });

            // Получение данных (можно заменить на загрузку из другого источника)
            

            // Добавление линий для каждого списка
            for (int i = 0; i < list.Count; i++)
            {
                var series = new LineSeries
                {
                    Title = $"Линия {i + 1}",  // Название для легенды
                    MarkerType = MarkerType.Circle,  // Маркеры точек
                    MarkerSize = 4,
                    MarkerStroke = OxyColors.Automatic,
                    MarkerFill = OxyColors.Automatic,
                    StrokeThickness = 2
                };

                // Заполнение данными
                foreach (var point in list[i])
                {
                    series.Points.Add(new DataPoint(point.betta, point.sig));
                }

                PlotModel.Series.Add(series);
            }

            DataContext = this;
        }
        public PlotModel PlotModel { get; set; }
        List<TableSig> _table;
    }
}
