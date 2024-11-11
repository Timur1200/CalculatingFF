using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalculatingFF
{
    internal class Model1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public void Solve()
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
        private double _b;
        private double b6;
        private double b7;
        private double b8;
        private double b9;
        private double b10;
        private double b11;
        private double b12;
        private double b13;

        public double B { get { return _b; }set { _b = value;OnPropertyChanged("B");OnPropertyChanged("B1");Solve(); } }
        public double B6 { get { return b6; } set { b6 = value; OnPropertyChanged("B6"); Solve(); } }
        public double B7 { get { return b7; } set { b7 = value; OnPropertyChanged("B7"); Solve(); } }
        public double B8 { get { return b8; } set { b8 = value; OnPropertyChanged("B8"); Solve(); } }
        public double B9 { get { return b9; } set { b9 = value; OnPropertyChanged("B9"); Solve(); } }
        public double B10 { get { return b10; } set {b10 = value; OnPropertyChanged("B10"); Solve(); } }
        public double B11 { get { return b11; } set { b11 = value; OnPropertyChanged("11"); Solve(); } }
        public double B12 { get { return b12; } set { b12 = value; OnPropertyChanged("B12"); Solve(); } }
        public double B13 { get { return b13; } set { b13 = value; OnPropertyChanged("B13"); Solve(); } }

        public double B1 { get { return B * 3.14 / 180; } set { } }
        public double B2 { get { return B10 * Math.Cos(B6) * Math.Cos(B6) + B11 * Math.Sin(B6) * Math.Sin(B6); } set { } }
        public double B3 { get { return 0.5 * (B12 + B13) * B5 + B7; } set { } }
        public double B4 { get { return 0.5 * (B12 + B13) * B14 + B15; } set { } }
        public double B5 { get { return Math.Tan(B10) * Math.Cos(B6) * Math.Cos(B6) + Math.Tan(B11) * Math.Sin(B6) * Math.Sin(B6); } set { } }
        public double D1 { get { return D4 * D4 * (B12 - B13) * (B12 - B13) / (Math.Cos(B2) * Math.Cos(B2)) - 4 * B3 * B3 * (1 - B14 * Math.Cos(B2) * Math.Cos(B2) * (1 - 0.25 * B14)) - B19; } set { } }
        public double D2 { get { return Math.Cos(2 * B1) * (B3 * B18 + 0.5 * B4 * Math.Sin(2 * B6 - B2)) - Math.Sin(2 * B1) * (B3 * B17 + 0.5 * B4 * Math.Cos(2 * B6 - B2)); } set { } }
        public double B14 { get { return (Math.Tan(B10) - Math.Tan(B11)) * Math.Sin(2 * (B6)); } set { } }
        public double B15 { get { return -0.5 * (B8 - B9) * Math.Sin(2 * (B6)); } set { } }
        public double B16 { get { return -0.5 * (B10 - B11) * Math.Sin(2 * (B6)); } set { } }
        public double B17 { get { return Math.Sin(2 * B6 - B2) - 0.5 * B16 * Math.Sin(2 * B6) / Math.Cos(B2); } set { } }
        public double B18 { get { return COS(2 * B6 - B2) - 0.5 * B16 * COS(2 * B6) / COS(B2); } set { } }
        public double B19 { get { return -B3 * B4 * B14 * SIN(B2) * SIN(B2) + B4 * B4; } set { } }
        public double B20 { get { return COS(B2) / (1 - 0.5 * B16); } set { } }
        public double D4 { get { return (1 - 0.5 * B14 * COS(B2) * COS(B2)); } set { } }



    }
}
