using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;


namespace CalculatingFF
{
    public class Model1 : INotifyPropertyChanged
    {
        private void Base()
        {
            BettaList = new ObservableCollection<double> { 0, 35, 45, 60, 90 };
            Sig2List = new ObservableCollection<double> {0,5,10,15,20 };
            
        }
        public Model1()
        {
            Base();
        }
        public Model1(bool testData)
        {
            Base();
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
            double tolerance = 1e-8; // Допустимая погрешность для 4 знаков после запятой
            double step = 0.0001; // Шаг изменения переменных для 4 знаков после запятой

            // Подбор значения B6
            double b6Min = 0.00;
            double b6Max = 3.00;
            double b6Mid = (b6Min + b6Max) / 2;

            while (b6Max - b6Min > tolerance)
            {
                B6 = b6Mid;
                double d1Mid = D1;
                double d2Mid = D2;

                if (double.IsInfinity(d1Mid) || double.IsNaN(d1Mid) || double.IsInfinity(d2Mid) || double.IsNaN(d2Mid))
                {
                    b6Max = b6Mid;
                    b6Mid = (b6Min + b6Max) / 2;
                    continue;
                }

                B6 = b6Mid - step;
                double d1Left = D1;
                double d2Left = D2;

                if (double.IsInfinity(d1Left) || double.IsNaN(d1Left) || double.IsInfinity(d2Left) || double.IsNaN(d2Left))
                {
                    b6Max = b6Mid;
                    b6Mid = (b6Min + b6Max) / 2;
                    continue;
                }

                B6 = b6Mid + step;
                double d1Right = D1;
                double d2Right = D2;

                if (double.IsInfinity(d1Right) || double.IsNaN(d1Right) || double.IsInfinity(d2Right) || double.IsNaN(d2Right))
                {
                    b6Min = b6Mid;
                    b6Mid = (b6Min + b6Max) / 2;
                    continue;
                }

                double errorMid = Math.Abs(d1Mid) + Math.Abs(d2Mid);
                double errorLeft = Math.Abs(d1Left) + Math.Abs(d2Left);
                double errorRight = Math.Abs(d1Right) + Math.Abs(d2Right);

                if (errorLeft < errorMid)
                {
                    b6Max = b6Mid;
                }
                else if (errorRight < errorMid)
                {
                    b6Min = b6Mid;
                }
                else
                {
                    break;
                }

                b6Mid = (b6Min + b6Max) / 2;
            }

            B6 = Math.Round(b6Mid, 4); // Округляем до 4 знаков после запятой

            // Подбор значения B12
            double b12Min = 0.00;
            double b12Max = 1e308; // Максимальное значение, чтобы избежать переполнения
            double b12Mid = (b12Min + b12Max) / 2;

            while (b12Max - b12Min > tolerance)
            {
                B12 = Math.Round(b12Mid, 4); // Округляем до 4 знаков после запятой
                double d1Mid = D1;
                double d2Mid = D2;

                if (double.IsInfinity(d1Mid) || double.IsNaN(d1Mid) || double.IsInfinity(d2Mid) || double.IsNaN(d2Mid))
                {
                    b12Max = b12Mid;
                    b12Mid = (b12Min + b12Max) / 2;
                    continue;
                }

                B12 = Math.Round(b12Mid - step, 4); // Округляем до 4 знаков после запятой
                double d1Left = D1;
                double d2Left = D2;

                if (double.IsInfinity(d1Left) || double.IsNaN(d1Left) || double.IsInfinity(d2Left) || double.IsNaN(d2Left))
                {
                    b12Max = b12Mid;
                    b12Mid = (b12Min + b12Max) / 2;
                    continue;
                }

                B12 = Math.Round(b12Mid + step, 4); // Округляем до 4 знаков после запятой
                double d1Right = D1;
                double d2Right = D2;

                if (double.IsInfinity(d1Right) || double.IsNaN(d1Right) || double.IsInfinity(d2Right) || double.IsNaN(d2Right))
                {
                    b12Min = b12Mid;
                    b12Mid = (b12Min + b12Max) / 2;
                    continue;
                }

                double errorMid = Math.Abs(d1Mid) + Math.Abs(d2Mid);
                double errorLeft = Math.Abs(d1Left) + Math.Abs(d2Left);
                double errorRight = Math.Abs(d1Right) + Math.Abs(d2Right);

                if (errorLeft < errorMid)
                {
                    b12Max = b12Mid;
                }
                else if (errorRight < errorMid)
                {
                    b12Min = b12Mid;
                }
                else
                {
                    break;
                }

                b12Mid = (b12Min + b12Max) / 2;
            }

            B12 = Math.Round(b12Mid, 4); // Округляем до 4 знаков после запятой
        }

        public void SelectionSimplex()
        {

        }

        double COS(double c)
        {
            return Math.Cos(c);
        }
        double SIN(double c)
        {
            return Math.Sin(c);
        }
        public ObservableCollection<double> BettaList;
        public ObservableCollection<double> Sig2List;

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
        /// <summary>
        /// Psi
        /// </summary>
        public double B6 { get { return b6; } set { b6 = value; OnPropertyChanged("B6"); Solve(); } }
        /// <summary>
        /// C
        /// </summary>
        public double B8 { get { return b8; } set { b8 = value; OnPropertyChanged("B8"); Solve(); } }
        /// <summary>
        /// C90
        /// </summary>
        public double B9 { get { return b9; } set { b9 = value; OnPropertyChanged("B9"); Solve(); } }
        /// <summary>
        /// Ro0
        /// </summary>
        public double B10 { get { return b10; } set {b10 = value; OnPropertyChanged("B10"); Solve(); } }
        /// <summary>
        /// Ro90
        /// </summary>
        public double B11 { get { return b11; } set { b11 = value; OnPropertyChanged("11"); Solve(); } }
        /// <summary>
        /// Sig1
        /// </summary>
        public double B12 { get { return b12; } set { b12 = value; OnPropertyChanged("B12"); Solve(); } }
        /// <summary>
        /// Sig3
        /// </summary>
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
