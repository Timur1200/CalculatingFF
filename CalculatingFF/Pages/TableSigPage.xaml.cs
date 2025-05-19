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
using MathNet.Numerics.LinearAlgebra;

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
            
            for (int i=0;i< TabPage1._Model.BettaList.Count; i++)
            {
                list.Add(new List<TableSig>());
            }
            
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

            Plot();// строить обычный график в 2 вкладке

            for (int i = 0; i < list.Count; i++)
            {
                PlotComboBox.Items.Add($"{i+1}");
            }

            BuildPlotWithParabola(0);// строит линейную 
        }
        public PlotModel PlotModel { get; set; }
        public PlotModel PlotModel1 { get; set; }
        List<TableSig> _table;
        List<List<TableSig>> list = new List<List<TableSig>>();

        public void Plot()
        {
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

            MyPlotView.Model = PlotModel;
            PlotLinear(0);
        }
        public static (double a, double b, double c) FitParabola(List<TableSig> data)
        {
            // Подготовка матриц для МНК
            var x = data.Select(p => p.betta).ToArray();
            var y = data.Select(p => p.sig).ToArray();

            // Матрица Vandermonde для квадратичной аппроксимации: [1, x, x²]
            var design = Matrix<double>.Build.DenseOfRowArrays(
                x.Select(xi => new[] { 1.0, xi, xi * xi })
            );

            var rhs = Vector<double>.Build.Dense(y);
            var p = design.TransposeThisAndMultiply(design).LU().Solve(design.TransposeThisAndMultiply(rhs));

            return (p[2], p[1], p[0]); // a, b, c (y = ax² + bx + c)
        }
        public void BuildPlotWithParabola(int i)
        {
            var plotModel = new PlotModel { Title = "Параболическая аппроксимация" };

            // Исходные данные
            var rawSeries = new LineSeries
            {
                Title = "Исходные данные",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                Color = OxyColors.Blue
            };

            foreach (var point in list[i])
            {
                rawSeries.Points.Add(new DataPoint(point.betta, point.sig));
            }
            plotModel.Series.Add(rawSeries);

            // Аппроксимация параболой
            var (a, b, c) = FitParabola(list[i]);
            var parabola = new LineSeries
            {
                Title = $"Аппроксимация: y = {a:F2}x² + {b:F2}x + {c:F2}",
                Color = OxyColors.Red,
                StrokeThickness = 2
            };

            // Генерация точек параболы
            double xMin = list[i].Min(p => p.betta);
            double xMax = list[i].Max(p => p.betta);
            for (double x = xMin; x <= xMax; x += 0.1)
            {
                double y = a * x * x + b * x + c;
                parabola.Points.Add(new DataPoint(x, y));
            }

            plotModel.Series.Add(parabola);

            // Оси
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Betta" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Sig" });

            MyPlotView1.Model = plotModel; // Если используется x:Name
        }

        public void PlotLinear(int i)
        {
            if (i < 0 || list.Count < i) return;
            PlotModel1 = new PlotModel { Title = "Линейная аппроксимация" };

            // Добавление исходных данных
            var rawSeries = new LineSeries
            {
                Title = "Исходные данные",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Blue
            };

            foreach (var point in list[i])
            {
                rawSeries.Points.Add(new DataPoint(point.betta, point.sig));
            }
            PlotModel1.Series.Add(rawSeries);

            // Расчет аппроксимации
            var (k, b) = CalculateLinearRegression(list[i]);

            // Добавление линии регрессии
            var trendLine = new LineSeries
            {
                Title = $"Аппроксимация: y = {k:F2}x + {b:F2}",
                Color = OxyColors.Red,
                StrokeThickness = 2
            };

            // Вычисляем две крайние точки для прямой
            double xMin = list[i].Min(p => p.betta);
            double xMax = list[i].Max(p => p.betta);
            trendLine.Points.Add(new DataPoint(xMin, k * xMin + b));
            trendLine.Points.Add(new DataPoint(xMax, k * xMax + b));

            PlotModel1.Series.Add(trendLine);

            // Настройка осей
            PlotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Betta" });
            PlotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Sig" });
            MyPlotView1.Model = PlotModel1;
        }
        public static (double k, double b) CalculateLinearRegression(List<TableSig> data)
        {
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
            int n = data.Count;

            foreach (var point in data)
            {
                sumX += point.betta;
                sumY += point.sig;
                sumXY += point.betta * point.sig;
                sumX2 += point.betta * point.betta;
            }

            double k = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
            double b = (sumY - k * sumX) / n;

            return (k, b);
        }

        private void PlotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BuildPlotWithParabola(PlotComboBox.SelectedIndex);
        }
    }
}
