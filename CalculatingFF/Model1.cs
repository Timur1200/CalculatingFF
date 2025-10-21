using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;


namespace CalculatingFF
{
    public class Model1 : INotifyPropertyChanged
    {
        private void Base()
        {
            BettaList = new ObservableCollection<double> { 0, 30, 45, 60, 90 };
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
            double step = 0.05;//шаг
            double tolerance = 0.01;// точность
            double bestB6 = B6;//psi 0 до 3
            double bestB12 = B12;//sig1 не отриц.
            double bestError = double.MaxValue;//разница(лучшая погрешность)
            for (double b6 = 0; b6 <= 3; b6 += step)
            {
                for (double b12 = 0; b12 <= 100; b12 += step)                                   // Можно выбрать больший диапазон, если нужно
                {
                    B6 = b6;
                    B12 = b12;

                    double error = Math.Abs(D1) + Math.Abs(D2);

                    if (error < bestError)
                    {
                        bestError = error;
                        bestB6 = b6;// psi
                        bestB12 = b12;// sig1
                    }

                    if (bestError < tolerance)
                    {
                        B6 = bestB6;
                        B12 = bestB12;
                        return;
                    }
                }
            }
            B6 = bestB6;
            B12 = bestB12;
            BestError = bestError;
        }

        public void SelectionViewDiplom()
        {
            double step = Settings.settings.Step;//шаг 0 05
            double tolerance = Settings.settings.Tolerance;// точность 0 01
            double bestPsi = B6;//psi
            double bestSig1 = B12;//sig1
            double bestError = double.MaxValue;//лучшее отклонение
            for (double psi = 0; psi <= 3; psi += step)
            {
                for (double sig1 = 0; sig1 <= 100; sig1 += step)                                   // Можно выбрать больший диапазон, если нужно
                {
                    B6 = psi;
                    B12 = sig1;
                    double error = Math.Abs(D1) + Math.Abs(D2);
                    //Суммарное отклонение F1 и F2
                    if (error < bestError)
                    {
                        bestError = error;
                        bestPsi = psi;// psi
                        bestSig1 = sig1;// sig1
                    }

                    if (bestError < tolerance)
                    {
                        MessageBox.Show("идеальное значение");
                        B6 = bestPsi;
                        B12 = bestSig1;
                        return;
                    }
                }
            }
            B6 = bestPsi;
            B12 = bestSig1;
        }

        public void Selection(bool IsRo0)
        {
            if (!IsRo0) 
            { 
                Selection();
                return; 
            }
            double step = 0.05;
            double tolerance = 0.01;

            double bestB6 = B6;
            double bestB12 = B10;
            double bestError = double.MaxValue;

            for (double b6 = 0; b6 <= 3; b6 += step)
            {
                for (double b12 = 0; b12 <= 100; b12 += step) // Можно выбрать больший диапазон, если нужно
                {
                    B6 = b6;
                    B10 = b12;

                    double error = Math.Abs(D1) + Math.Abs(D2);

                    if (error < bestError)
                    {
                        bestError = error;
                        bestB6 = b6;
                        bestB12 = b12;
                    }

                    if (bestError < tolerance)
                    {
                        B6 = bestB6;
                        B10 = bestB12;
                        return;
                    }
                }
            }

            B6 = bestB6;
            B10 = bestB12;
        }

        public void SelectionParallel()
        {
            
        }

        public void SelectionSimplex()
        {
            SelectionParallel();
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
        private double besterror;
        public double BestError { get { return besterror; } set { besterror = value; OnPropertyChanged("BestError"); } }
       
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
        /// Sig3 Sig2
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
