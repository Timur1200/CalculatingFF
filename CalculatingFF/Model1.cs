using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatingFF
{
    internal class Model1 : INotifyPropertyChanged
    {
        public Model1()
        {

        }
        public Model1(bool testData)
        {
            if (testData)
            {
                B = 75;
                B6 = 1.06;
                
                B8 = 27.08;
                B9 = 18.11;
                B10 = 0.4630;
                B11 = 0.13;
                B12 = 59.5;
                B13 = 5;

            }
          
        }
        public string TestPrint()
        {
            string msg = $"r0= {B2} \n S={B3} \n S`={B4} \n K={B5} \n k={B14} \n c={B15} \n r0={B16} \n n1={B17} \n n2={B18} \n m1={B19} \n xsi={B20} \n F1={D1} \n F2={D2} \n p={D4}";
            return msg;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public void Solve()
        {
            OnPropertyChanged("B1");
            OnPropertyChanged("B2");
            OnPropertyChanged("B5");
            OnPropertyChanged("B3");

            OnPropertyChanged("B14");
            OnPropertyChanged("B15");
            OnPropertyChanged("B4");
            
            OnPropertyChanged("B7");
            
        
           
            OnPropertyChanged("B16");
            OnPropertyChanged("B17");
            OnPropertyChanged("B18");
            OnPropertyChanged("B19");
            OnPropertyChanged("B20");
            OnPropertyChanged("D4");
            OnPropertyChanged("D1");
            OnPropertyChanged("D2");
        }

        public void Selection()
        {
            // Начальные значения для B6, B12
            double[] initialValues = { B6, B12 };

            // Ограничения для B6, B12
            double[] lowerBounds = { 0, 0 };
            double[] upperBounds = { 3, 100 }; // Укажите верхние границы для B12, если они известны

            // Вызов метода покоординатного спуска
            double[] bestValues = CoordinateDescent(initialValues, lowerBounds, upperBounds);

            // Применение лучших значений
            B6 = bestValues[0];
            B12 = bestValues[1];

            Solve();
        }

        private double[] CoordinateDescent(double[] initialValues, double[] lowerBounds, double[] upperBounds)
        {
            int n = initialValues.Length;
            double[] currentValues = (double[])initialValues.Clone();
            double[] stepSizes = { 0.1, 0.1 }; // Начальные шаги для B6 и B12
            double tolerance = 1e-6; // Допуск для остановки

            while (true)
            {
                bool improved = false;

                for (int i = 0; i < n; i++)
                {
                    double bestError = EvaluateError1(currentValues);
                    double bestValue = currentValues[i];

                    // Пробуем увеличить значение
                    double newValue = Math.Min(currentValues[i] + stepSizes[i], upperBounds[i]);
                    currentValues[i] = newValue;
                    double newError = EvaluateError1(currentValues);

                    if (newError < bestError)
                    {
                        bestError = newError;
                        bestValue = newValue;
                        improved = true;
                    }
                    else
                    {
                        // Пробуем уменьшить значение
                        newValue = Math.Max(currentValues[i] - 2 * stepSizes[i], lowerBounds[i]);
                        currentValues[i] = newValue;
                        newError = EvaluateError1(currentValues);

                        if (newError < bestError)
                        {
                            bestError = newError;
                            bestValue = newValue;
                            improved = true;
                        }
                        else
                        {
                            // Возвращаемся к лучшему значению
                            currentValues[i] = bestValue;
                        }
                    }

                    // Обновляем шаг
                    stepSizes[i] *= 0.5;
                }

                // Проверка условия остановки
                if (!improved || stepSizes.Max() < tolerance)
                    break;
            }

            return currentValues;
        }

        private double EvaluateError1(double[] values)
        {
            B6 = values[0];
            B12 = values[1];

            Solve();

            return Math.Abs(D1) + Math.Abs(D2);
        }

        public void SelectionSimplex()
        {
            // Начальные значения для B6, B12
            double[] initialValues = { B6, B12 };

            // Ограничения для B6, B12
            double[] lowerBounds = { 0, 0 };
            double[] upperBounds = { 3, 100 }; // Укажите верхние границы для B12, если они известны

            // Вызов метода Нелдера-Мида
            double[] bestValues = NelderMead(initialValues, lowerBounds, upperBounds);

            // Применение лучших значений
            B6 = bestValues[0];
            B12 = bestValues[1];

            Solve();
        }

        private double[] NelderMead(double[] initialValues, double[] lowerBounds, double[] upperBounds)
        {
            int n = initialValues.Length;
            double[][] simplex = new double[n + 1][];
            double[] errors = new double[n + 1];

            // Инициализация симплекса
            for (int i = 0; i < n + 1; i++)
            {
                simplex[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        simplex[i][j] = initialValues[j] + 0.1 * (upperBounds[j] - lowerBounds[j]);
                    else
                        simplex[i][j] = initialValues[j];
                }
                errors[i] = EvaluateError(simplex[i]);
            }

            // Параметры метода Нелдера-Мида
            double alpha = 1; // Отражение
            double beta = 0.5; // Сжатие
            double gamma = 2; // Растяжение
            double sigma = 0.5; // Сжатие всего симплекса

            for (int iteration = 0; iteration < 1000; iteration++)
            {
                // Сортировка симплекса по ошибке
                Array.Sort(errors, simplex);

                // Вычисление центра тяжести без худшей точки
                double[] centroid = new double[n];
                for (int i = 0; i < n; i++)
                {
                    centroid[i] = 0;
                    for (int j = 0; j < n; j++)
                        centroid[i] += simplex[j][i];
                    centroid[i] /= n;
                }

                // Отражение
                double[] reflected = new double[n];
                for (int i = 0; i < n; i++)
                    reflected[i] = centroid[i] + alpha * (centroid[i] - simplex[n][i]);
                double reflectedError = EvaluateError(reflected);

                if (reflectedError < errors[0])
                {
                    // Растяжение
                    double[] expanded = new double[n];
                    for (int i = 0; i < n; i++)
                        expanded[i] = centroid[i] + gamma * (reflected[i] - centroid[i]);
                    double expandedError = EvaluateError(expanded);

                    if (expandedError < reflectedError)
                    {
                        simplex[n] = expanded;
                        errors[n] = expandedError;
                    }
                    else
                    {
                        simplex[n] = reflected;
                        errors[n] = reflectedError;
                    }
                }
                else if (reflectedError < errors[n - 1])
                {
                    simplex[n] = reflected;
                    errors[n] = reflectedError;
                }
                else
                {
                    // Сжатие
                    if (reflectedError < errors[n])
                    {
                        double[] contracted = new double[n];
                        for (int i = 0; i < n; i++)
                            contracted[i] = centroid[i] + beta * (reflected[i] - centroid[i]);
                        double contractedError = EvaluateError(contracted);

                        if (contractedError < reflectedError)
                        {
                            simplex[n] = contracted;
                            errors[n] = contractedError;
                        }
                        else
                        {
                            // Сжатие всего симплекса
                            for (int i = 1; i < n + 1; i++)
                            {
                                for (int j = 0; j < n; j++)
                                    simplex[i][j] = simplex[0][j] + sigma * (simplex[i][j] - simplex[0][j]);
                                errors[i] = EvaluateError(simplex[i]);
                            }
                        }
                    }
                    else
                    {
                        double[] contracted = new double[n];
                        for (int i = 0; i < n; i++)
                            contracted[i] = centroid[i] + beta * (simplex[n][i] - centroid[i]);
                        double contractedError = EvaluateError(contracted);

                        if (contractedError < errors[n])
                        {
                            simplex[n] = contracted;
                            errors[n] = contractedError;
                        }
                        else
                        {
                            // Сжатие всего симплекса
                            for (int i = 1; i < n + 1; i++)
                            {
                                for (int j = 0; j < n; j++)
                                    simplex[i][j] = simplex[0][j] + sigma * (simplex[i][j] - simplex[0][j]);
                                errors[i] = EvaluateError(simplex[i]);
                            }
                        }
                    }
                }
            }

            // Возвращаем лучшую точку
            return simplex[0];
        }

        private double EvaluateError(double[] values)
        {
            B6 = values[0];
            B12 = values[1];

            Solve();

            return Math.Abs(D1) + Math.Abs(D2);
        }

        double COS(double c)
        {
            return Math.Cos(c);
        }
        double SIN(double c)
        {
            return Math.Sin(c);
        }
        private double _b;
        private double b6;
        
        private double b8;
        private double b9;
        private double b10;
        private double b11;
        private double b12;
        private double b13;
        /// <summary>
        /// Бэта в градусах
        /// </summary>
        public double B { get { return _b; }set { _b = value;OnPropertyChanged("B");OnPropertyChanged("B1");Solve(); } }
        public double B6 { get { return b6; } set { b6 = value; OnPropertyChanged("B6"); Solve(); } }
        
        public double B8 { get { return b8; } set { b8 = value; OnPropertyChanged("B8"); Solve(); } }
        public double B9 { get { return b9; } set { b9 = value; OnPropertyChanged("B9"); Solve(); } }
        public double B10 { get { return b10; } set {b10 = value; OnPropertyChanged("B10"); Solve(); } }
        public double B11 { get { return b11; } set { b11 = value; OnPropertyChanged("11"); Solve(); } }
        public double B12 { get { return b12; } set { b12 = value; OnPropertyChanged("B12"); Solve(); } }
        public double B13 { get { return b13; } set { b13 = value; OnPropertyChanged("B13"); Solve(); } }
        /// <summary>
        /// Бэта в Радианах
        /// </summary>
        public double B1 { get { return B * 3.14 / 180; } set { } }
        public double B2 { get { return B10 * Math.Cos(B6) * Math.Cos(B6) + B11 * Math.Sin(B6) * Math.Sin(B6); } set { } }
        public double B3 { get { return 0.5 * (B12 + B13) * B5 + B7; } set { } }
        public double B7 { get { return B8 * COS(B6) * COS(B6) + B9 * SIN(B6) * SIN(B6); } set { } }
        public double B4 { get { return 0.5 * (B12 + B13) * B14 + B15; } set { } }
        public double B5 { get { return Math.Tan(B10) * Math.Cos(B6) * Math.Cos(B6) + Math.Tan(B11) * Math.Sin(B6) * Math.Sin(B6); } set { } }
        public double D1 { get { return D4 * D4 * (B12 - B13) * (B12 - B13) / (Math.Cos(B2) * Math.Cos(B2)) - 4 * B3 * B3 * (1 - B14 * Math.Cos(B2) * Math.Cos(B2) * (1 - 0.25 * B14)) - B19; } set { } }
        public double D2 { get { return Math.Cos(2 * B1) * (B3 * B18 + 0.5 * B4 * Math.Sin(2 * B6 - B2)) - Math.Sin(2 * B1) * (B3 * B17 + 0.5 * B4 * Math.Cos(2 * B6 - B2)); } set { } }
        public double B14 { get { return -(Math.Tan(B10) - Math.Tan(B11)) * Math.Sin(2 * (B6)); } set { } }
        public double B15 { get { return -0.5 * (B8 - B9) * Math.Sin(2 * (B6)); } set { } }
        public double B16 { get { return -0.5 * (B10 - B11) * Math.Sin(2 * (B6)); } set { } }
        public double B17 { get { return Math.Sin(2 * B6 - B2) - 0.5 * B16 * Math.Sin(2 * B6) / Math.Cos(B2); } set { } }
        public double B18 { get { return COS(2 * B6 - B2) - 0.5 * B16 * COS(2 * B6) / COS(B2); } set { } }
        public double B19 { get { return -B3 * B4 * B14 * SIN(B2) * SIN(B2) + B4 * B4; } set { } }
        public double B20 { get { return COS(B2) / (1 - 0.5 * B16); } set { } }
        public double D4 { get { return (1 - 0.5 * B14 * COS(B2) * COS(B2)); } set { } }



    }
}
