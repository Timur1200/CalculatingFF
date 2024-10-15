using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace CalculatingFF
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public Model()
        {
            A1 = 30;
            A2 = 60;
        }
        public Model(bool b)
        {
            A1 = 30;
            A2 = 60;
            if (b)
            {
                S11 = 41;
                S31 = 0;
                S21 = 31;
                S23 = 0;
                S13 = 70;
                S33 = 0;
                C0 = 0;
                //C0 = 26.08;
                Ksr = 0.5;
            }
        }
        /// <summary>
        /// Вычисляет и отображает значения
        /// </summary>
        public void Solve()
        {
            OnPropertyChanged("Tn1");
            OnPropertyChanged("Sn1");
            OnPropertyChanged("Tn2");
            OnPropertyChanged("Sn2");
            OnPropertyChanged("K90");
            OnPropertyChanged("C90");
            OnPropertyChanged("M1");
            OnPropertyChanged("M2");
            OnPropertyChanged("M3");
            OnPropertyChanged("F");
        }
        /// <summary>
        /// Подбор значений для F->0 при С0
        /// </summary>
        public void Selection()
        {
            
            
            List<double> list = new List<double>();
           
            int j = 0;
            while (Math.Abs(F)>200)
            {
                C0 = C0 + 1;
                j++;
                if (j > 1000)
                {
                    MessageBox.Show("Не получилось");
                    return;
                }
            }
            
            
            list.Add(F);
            C0 = C0 + 0.01;
            list.Add(F);
            int i = 0;
            i++;
            j = 0;
            do
            {
                Trace.WriteLine(Math.Abs(list[i - 1]) + " and " + Math.Abs(list[i]));
                C0 = C0 + 0.01;
                list.Add(F);
                i++;
                j++;
                if (j > 1000)
                {
                    
                    MessageBox.Show("Не получилось.");
                    return;
                }
            }
            while (Math.Abs(list[i-1]) > Math.Abs(list[i]));
            C0 = C0 - 0.01; 

            Solve();
        }


        
        private double _s11;
        private double _s31;
        private double _s21;
        private double _s23;
        private double _a1;
        private double _a2;
        private double _a3;
        private double _s13;
        private double _s33;
        private double _c0;
        private double _ksr;

        

        public double S11 { get { return _s11; } set { _s11 = value; OnPropertyChanged("S11"); } }
        public double S31 { get { return _s31; } set { _s31 = value; OnPropertyChanged("S31"); } }
        public double S21 { get { return _s21; } set { _s21 = value; OnPropertyChanged("S21"); } }
        public double S23 { get { return _s23; } set { _s23 = value; OnPropertyChanged("S23"); } }
        public double A1 { get { return _a1; } set { _a1 = value;OnPropertyChanged("A1");OnPropertyChanged("A1rad"); } }
        public double A2 { get { return _a2; } set { _a2 = value; OnPropertyChanged("A2"); OnPropertyChanged("A2rad"); } }
        public double A3 { get { return _a3; } set { _a3 = value; OnPropertyChanged("A3"); OnPropertyChanged("A3rad"); } }
        public double S13 { get { return _s13; } set { _s13 = value; OnPropertyChanged("S13"); } }
        public double S33 { get { return _s33; } set { _s33 = value; OnPropertyChanged("S33"); } }
        public double C0 { get { return _c0; } set { _c0 = value; OnPropertyChanged("C0"); } }
        public double Ksr { get { return _ksr; } set { _ksr = value; OnPropertyChanged("Ksr"); } }
        /// <summary>
        /// Альфа1 в радианах
        /// </summary>
        public double A1rad {get {return (A1 * 3.14 / 180);  }}
        public double A2rad { get { return (A2 * 3.14 / 180); } }
        public double A3rad { get { return (A3 * 3.14 / 180); } }
        public double Tn1 { get { return 0.5 * (S11 - S31) * Math.Sin(2 * A1rad); } }
        public double Sn1 {get { return 0.5 * (S11 + S31) + 0.5 * (S11 - S31) * Math.Cos(2 * A1rad); } }
        public double Tn2 { get { return 0.5 * (S21 - S23) * Math.Sin(2 * A2rad); } }
        public double Sn2 { get { return 0.5 * (S21 + S23) + 0.5 * (S21 - S23) * Math.Cos(2 * A2rad); } }
        public double K90 {get { return (Tn1 - Tn2) / (Sn1-Sn2); } }
        public double C90 {get {return  Tn1 - Sn1 * K90;} }
        //public double M1 { get { return (S13 - S33) * (Math.Cos(2*A3rad)+K90*Math.Sin(2 * A3rad)); } }
        //public double M2 { get { return (S13-S33)* (Math.Sin(2*A3rad) - K90 * Math.Cos(2*A3rad))+ C0-C90; } }
        //public double M3 {get {return (S13 + S33)*K90 + C0+C90;} }  
        //public double F {  get { return } }
        public double M1 { get { return (S13-S33)*(Math.Cos(2*A3rad)+Ksr*Math.Sin(2*A3rad)) ; }set { }  }
        public double M2 { get { return (S13 - S33) * (Math.Sin(2 * A3rad) - Ksr * Math.Cos(2 * A3rad)) + C0 - C90; } set { } }
        public double M3 { get { return (S13 + S33) * Ksr + C0 + C90; } set { } }
        public double F {get { return M1 * M1+M2*M2 - M3* M3; } set { } }
    }
}
