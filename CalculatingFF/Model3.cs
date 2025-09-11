using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
/*int B1;
        int B2S31;
        int B3S12;
        int B4S32;
        int B5S13;
        int B6S33;
        int B7A1;
        int B8A2;
        int B9A3;
        int B10P1;
        int B11P2;
        int B12P3;
        int B13K;
        int B14T1;
        int B15Z1;
        int B16Z2;
        int B17T2;
       
        int B18n1;
        int B19n2;
        int B20n3;
        int B21F;
        int Bb22C0;
        int B23C90;*/
namespace CalculatingFF
{
    public class Model3 : INotifyPropertyChanged
    {             
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public Model3() 
        {
            A1 = 30;
            A2 = 60;
            A3 = 45;
        }
        public Model3(bool Test)
        {
            A1 = 30;
            A2 = 60;
            A3 = 45;
            if (Test)
            {
                S11 = 41;
                S31 = 0;
                S12 = 31;
                S32 = 0;
                S13 = 31;
                S33 = 0;
                K = 0.3;
                T1 = 7.9;
            }
            
        }
        public void Selection()
        {
            double tolerance = 0.001; // Допустимая погрешность
            int maxIterations = 2200; // Максимальное количество итераций
            double stepSize = Settings.settings.Step; // Шаг изменения переменных с точностью до двух знаков после запятой
            for (int i = 0; i < maxIterations; i++)
            {
                double diff = Math.Abs(Z1 - Z2);

                if (diff < tolerance)
                {
                    Console.WriteLine($"Значения подобраны: K = {K}, T1 = {T1}, Z1 = {Z1}, Z2 = {Z2}");
                    break;
                }
                // Изменяем K
                double oldK = K;
                K += stepSize;
                if (K > 1.00) // Проверка на выход за пределы диапазона
                {
                    K = oldK - stepSize;
                    if (K < 0.00) // Проверка на выход за пределы диапазона
                    {
                        K = oldK;
                    }
                }
                Solve();
                double newDiff = Math.Abs(Z1 - Z2);

                if (newDiff < diff)
                {
                    // Улучшение, продолжаем в том же направлении
                    continue;
                }
                else
                {
                    // Ухудшение, меняем направление
                    K = oldK - stepSize;
                    if (K < 0.00) // Проверка на выход за пределы диапазона
                    {
                        K = oldK + stepSize;
                        if (K > 1.00) // Проверка на выход за пределы диапазона
                        {
                            K = oldK;
                        }
                    }
                    Solve();
                    newDiff = Math.Abs(Z1 - Z2);
                    if (newDiff < diff)
                    {                        
                        continue;// Улучшение, продолжаем в новом направлении
                    }
                    else
                    {                      
                        K = oldK;
                        Solve();// Возвращаем старое значение K
                    }
                }                
                double oldT1 = T1;// Изменяем T1
                T1 += stepSize;
                if (T1 < 0.00) // Проверка на неотрицательность
                {
                    T1 = oldT1;
                }
                Solve();
                newDiff = Math.Abs(Z1 - Z2);
                if (newDiff < diff)
                {
                    continue;// Улучшение, продолжаем в том же направлении
                }
                else
                {                   
                    T1 = oldT1 - stepSize; // Ухудшение, меняем направление
                    if (T1 < 0.00) // Проверка на неотрицательность
                    {
                        T1 = oldT1;
                    }
                    Solve();
                    newDiff = Math.Abs(Z1 - Z2);
                    if (newDiff < diff)
                    {                       
                        continue;// Улучшение, продолжаем в новом направлении
                    }
                    else
                    {                     
                        T1 = oldT1; // Возвращаем старое значение T1
                        Solve();
                    }
                }
                // Уменьшаем шаг, если нет улучшений
                stepSize *= 0.9;
            }
        }



        public void Solve()
        {
            OnPropertyChanged("P1");
            OnPropertyChanged("P2");
            OnPropertyChanged("P3");
            OnPropertyChanged("N1");
            OnPropertyChanged("N2");
            OnPropertyChanged("N3");
            OnPropertyChanged("T2");
            OnPropertyChanged("F");
            OnPropertyChanged("Z1");
            OnPropertyChanged("Z2");
            OnPropertyChanged("_C0");
            OnPropertyChanged("_C90");
        }

        private double _s11;
        private double _s31;
        private double _s12;
        private double _s32;
        private double _s13;
        private double _s33;
        private double _a1;
        private double _a2;
        private double _a3;
        
        private double _k;
        private double _t1;
        private double _c0;
        private double _c90;
       
        public double S11 { get { return _s11; } set { _s11 = value; OnPropertyChanged("S11");Solve(); } }
        public double S31 { get { return _s31; } set { _s31 = value; OnPropertyChanged("S31"); Solve(); } }
        public double S12 { get { return _s12; } set { _s12 = value; OnPropertyChanged("S12"); Solve(); } }
        public double S32 { get { return _s32; } set { _s32 = value; OnPropertyChanged("S32"); Solve(); } }
        public double S13 { get { return _s13; } set { _s13 = value; OnPropertyChanged("S13"); Solve(); } }
        public double S33 { get { return _s33; } set { _s33 = value; OnPropertyChanged("S33"); Solve(); } }
        public double A1 { get { return _a1; } set { _a1 = value; OnPropertyChanged("A1"); OnPropertyChanged("A1rad"); Solve(); } }
        public double A2 { get { return _a2; } set { _a2 = value; OnPropertyChanged("A2"); OnPropertyChanged("A2rad"); Solve(); } }
        public double A3 { get { return _a3; } set { _a3 = value; OnPropertyChanged("A3"); OnPropertyChanged("A3rad"); Solve(); } }
        public double C0 { get { return _c0; } set { _c0 = value; OnPropertyChanged("C0"); Solve(); } }
        public double C90 { get { return _c90; } set { _c90 = value; OnPropertyChanged("C90"); Solve(); } }
        //подбираемые значения
        public double K { get { return _k; } set { _k = value; OnPropertyChanged("K"); OnPropertyChanged("R90"); Solve(); } }
        public double T1 { get { return _t1; } set { _t1 = value; OnPropertyChanged("T1"); Solve(); } }
        /// <summary>
        /// Альфа1 в радианах
        /// </summary>
        public double A1rad { get { return (A1 * 3.14 / 180); } }
        public double A2rad { get { return (A2 * 3.14 / 180); } }
        public double A3rad { get { return (A3 * 3.14 / 180); } }

        public double P1 { get { return Math.Cos(2 * A1rad) + K * Math.Sin(2 * A1rad); } set { } }
        public double P2 {get { return Math.Cos(2 * A2rad) + K * Math.Sin(2 * A2rad); } set { } }
        public double P3 { get { return Math.Cos(2 * A3rad) + K * Math.Sin(2 * A3rad); } set { } }
        public double N1 {get { return Math.Sin(2 * A1rad) - K * Math.Cos(2 * A1rad); } set { } }
        public double N2 { get { return Math.Sin(2 * A2rad) - K * Math.Cos(2 * A2rad); } set { } }
        public double N3 { get { return Math.Sin(2 * A3rad) - K * Math.Cos(2 * A3rad); } set { } }
        public double R90 { get { return Math.Atan(K); } set { } }
        public double T2 { get {return Math.Sqrt((S11 - S31) * (S11 - S31) * P1 * P1 + ((S11 - S31) * N1 + T1) * ((S11 - S31) * N1 + T1))
                    -  (S11 + S31) * K; } set { } }
       
        public double F { get {
                return (S12 - S32) * (S12 - S32) * P2 * P2 + ((S12 - S32) * N2 + (C0 - C90)) * (S12 - S32) * N2 + (C0 - C90)
                    - ((S12 - S32) * K + C0 + C90) * ((S12 - S32) * K + C0 + C90);
            } set { } }
        public double Z1 { get {
                return (S12 - S32) * (S12 - S32) * P2 * P2 + ((S12 - S32) * N2 + T1) *
                    ((S12 - S32) * N2 + T1) - ((S12 + S32) * K + T2) * ((S12 + S32) * K + T2); } set { } }
        public double Z2 { get { return (S13 - S33) * (S13 - S33) * P3 * P3 + ((S13 - S33) * N3 + T1)
                    * ((S13 - S33) * N3 + T1) - ((S13 + S33) * K + T2) * ((S13 + S33) * K + T2);} set { } }
        public double _C0 { get { return (T1 + T2) / 2; } set { } }
        public double _C90 { get { return (T2 - T1) / 2; } set { } }
    }
}
