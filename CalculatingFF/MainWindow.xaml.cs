using System;
using System.Windows;

using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Linq;

namespace CalculatingFF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data = new Data();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Choose_Values(object sender, RoutedEventArgs e)
        {
            try
            {
                data.c0 = Convert.ToDouble(txtBoxC0.Text.Replace(".", ",").Trim());
                data.c90 = Convert.ToDouble(txtBoxC90.Text.Replace(".", ",").Trim());
                data.ro0 = Convert.ToDouble(txtBoxRo0.Text.Replace(".", ",").Trim());
                data.ro90 = Convert.ToDouble(txtBoxRo90.Text.Replace(".", ",").Trim());
                data.SIG2 = Convert.ToDouble(txtBoxSIG2.Text.Replace(".", ",").Trim());
                data.SIG3 = Convert.ToDouble(txtBoxSIG3.Text.Replace(".", ",").Trim());
                data.Betta = Convert.ToDouble(txtBoxBetta.Text.Replace(".", ",").Trim());

                double abs_value_max = Convert.ToDouble(txtBoxAbsValueMax.Text.Replace(".", ",").Trim());

                data.ChooseValues(abs_value_max);

                txtBoxPsi.Text = data.psi.ToString();
                txtBoxSIG1.Text = data.SIG1.ToString();
                txtBoxF1.Text = data.F1.ToString();
                txtBoxF2.Text = data.F2.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка! " + ex.Message + "\n" +
                        ex.StackTrace + "\n" +
                        ex.TargetSite + "\n" +
                        ex.HelpLink);
            }
        }

        private void Button_Click_Export_Excel(object sender, RoutedEventArgs e)
        {
            using (var path_dialog = new System.Windows.Forms.SaveFileDialog())
            {
                path_dialog.Filter = "Excel файлы(*.xlsx)|Все файлы (*.*)";

                if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    try
                    {
                        if (path_dialog.FileName.TakeLast(5).ToString() != ".xlsx")
                            path_dialog.FileName += ".xlsx";

                        ExportToExcel(path_dialog.FileName);

                        Process.Start(new ProcessStartInfo(path_dialog.FileName) { UseShellExecute = true });
                    }
                    catch(Exception ex)
                    {
                        System.Windows.MessageBox.Show("Ошибка! " + ex.Message + "\n" +
                        ex.StackTrace + "\n" +
                        ex.TargetSite + "\n" +
                        ex.HelpLink);
                    }
            }
        }

        private void SetCell(Excel.Worksheet worksheet, int lineNumber, int columnNumber, string valueToSet)
        {
            worksheet.Cells[lineNumber, columnNumber].FormulaLocal = valueToSet;
        }

        private void ExportToExcel(string pathToFile)
        {
            Excel.Application app = new Excel.Application
            {
                Visible = false,
                SheetsInNewWorkbook = 1
            };
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);

            app.DisplayAlerts = false;

            Excel.Worksheet list1 = (Excel.Worksheet)app.Worksheets.get_Item(1);
            list1.Name = "Лист1";

            string s;
            SetCell(list1, 1, 1, "betta");
            SetCell(list1, 1, 2, data.Betta.ToString());

            SetCell(list1, 2, 1, "ro");
            SetCell(list1, 2, 2, "=B10*COS(B6)*COS(B6)+B11*SIN(B6)*SIN(B6)");

            SetCell(list1, 3, 1, "S");
            SetCell(list1, 3, 2, "=0,5*D19*B5+B7");

            SetCell(list1, 4, 1, "S,");
            SetCell(list1, 4, 2, "=0,5*D19*B14+B15");

            SetCell(list1, 5, 1, "k=tan(psi)");
            SetCell(list1, 5, 2, "=TAN(B10)*COS(B6)*COS(B6)+TAN(B11)*SIN(B6)*SIN(B6)");

            SetCell(list1, 6, 1, "psi");
            SetCell(list1, 6, 2, data.psi.ToString().Replace(".", ","));

            SetCell(list1, 7, 1, "c");
            SetCell(list1, 7, 2, "=E1");

            SetCell(list1, 8, 1, "c0");
            SetCell(list1, 8, 2, data.c0.ToString().Replace(".", ","));

            SetCell(list1, 9, 1, "c90");
            SetCell(list1, 9, 2, data.c90.ToString().Replace(".", ","));

            SetCell(list1, 10, 1, "ro0");
            SetCell(list1, 10, 2, data.ro0.ToString().Replace(".", ","));

            SetCell(list1, 11, 1, "ro90");
            SetCell(list1, 11, 2, data.ro90.ToString().Replace(".", ","));

            SetCell(list1, 12, 1, "SIG1");
            SetCell(list1, 12, 2, data.SIG1.ToString().Replace(".", ","));

            SetCell(list1, 13, 1, "SIG3");
            SetCell(list1, 13, 2, data.SIG3.ToString().Replace(".", ","));

            SetCell(list1, 14, 1, "k,");
            SetCell(list1, 14, 2, "=-(TAN(B10)-TAN(B11))*SIN(2*(B6))");

            SetCell(list1, 15, 1, "c,");
            SetCell(list1, 15, 2, "=-0,5*(B8-B9)*SIN(2*(B6))");

            SetCell(list1, 16, 1, "ro,");
            SetCell(list1, 16, 2, "=-0,5*(B10-B11)*SIN(2*(B6))");

            SetCell(list1, 17, 1, "n1");
            SetCell(list1, 17, 2, "=SIN(2*B6-B2)-0,5*B16*SIN(2*B6)/COS(B2)");

            SetCell(list1, 18, 1, "n2");
            SetCell(list1, 18, 2, "=COS(2*B6-B2)-0,5*B16*COS(2*B6)/COS(B2)");

            SetCell(list1, 19, 1, "m1");
            SetCell(list1, 19, 2, "=-B3*B4*B14*SIN(B2)*SIN(B2)+B4*B4");

            SetCell(list1, 20, 1, "xsi");
            SetCell(list1, 20, 2, "=COS(B2)/(1-0,5*B16)");

            SetCell(list1, 21, 1, "SIG2");
            SetCell(list1, 21, 2, data.SIG2.ToString().Replace(".", ","));


            SetCell(list1, 1, 3, "F1");
            SetCell(list1, 1, 4, "=D4*D4*(D18)*(D18)/(COS(B2)*COS(B2))-4*B3*B3*(1-B14*COS(B2)*COS(B2)*(1-0,25*B14))-B19");
            SetCell(list1, 2, 3, "F2");

            SetCell(list1, 2, 4, "=COS(2*B1)*(B3*B18+0,5*B4*SIN(2*B6-B2))-SIN(2*B1)*(B3*B17+0,5*B4*COS(2*B6-B2))");

            SetCell(list1, 4, 3, "p");
            SetCell(list1, 4, 4, "=(1-0,5*B14*COS(B2)*COS(B2))");

            SetCell(list1, 5, 3, "F3");
            SetCell(list1, 5, 4, "=B12-0,5*(B12+B13)+(B3*B17-0,5*B4*COS(2*B6-B2))*B20");

            SetCell(list1, 6, 3, "F4");
            SetCell(list1, 6, 4, "=B3*B18+0,5*B4*SIN(2*B6-B2)");

            SetCell(list1, 7, 3, "Sx-Sy");
            SetCell(list1, 7, 4, "=(B3*B17+0,5*B4*COS(2*B6-B2))*B20*2");

            SetCell(list1, 8, 3, "Txy");
            SetCell(list1, 8, 4, "=-(B3*B18+0,5*B4*SIN(2*B6-B2))*B20");

            SetCell(list1, 9, 3, "F5");
            SetCell(list1, 9, 4, "=B12-B13-D7*COS(2*B1)-2*D8*SIN(2*B1)");

            SetCell(list1, 10, 3, "F6");
            SetCell(list1, 10, 4, "=-2*D8-D7*TAN(2*B1)");

            SetCell(list1, 11, 3, "F7");
            SetCell(list1, 11, 4, "=B12-B13+D7/(COS(2*B1))");

            SetCell(list1, 12, 3, "F8");
            SetCell(list1, 12, 4, "=B12-B13+2*D8/SIN(2*B1)");

            SetCell(list1, 13, 3, "F9");
            // SetCell(list1, 13, 4, "F9");

            SetCell(list1, 15, 3, "Эта");
            SetCell(list1, 15, 4, "=КОРЕНЬ((B12-B13)*(B12-B13)+(B12-B21)*(B12-B21)+(B13-B21)*(B13-B21))/КОРЕНЬ(3)");

            SetCell(list1, 16, 3, "КСИ");
            SetCell(list1, 16, 4, "=(B12+B13+B21)/КОРЕНЬ(3)");

            SetCell(list1, 17, 3, "ТЭТА");
            SetCell(list1, 17, 4, "=(0*3,14)/180");

            SetCell(list1, 18, 3, "SIG1-SIG3");
            SetCell(list1, 18, 4, "=0,816*D15*(COS(D17)-(COS(D17+(2*3,14/3))))");

            SetCell(list1, 19, 3, "SIG1+SIG3");
            SetCell(list1, 19, 4, "=2/КОРЕНЬ(3)*D16+(0,816*D15*(COS(D17)+(COS(D17+(2*3,14/3)))))");

            SetCell(list1, 23, 3, "K-M в X-B");
            SetCell(list1, 23, 4, "=D15*((1,73*SIN(D17+(1,046))-(COS(D17+(1,046))*SIN(B2))))-1,41*D16*SIN(B2)");

            SetCell(list1, 24, 4, "=2,44*B7*COS(B2)");


            SetCell(list1, 1, 5, "=B8*COS(B6)*COS(B6)+B9*SIN(B6)*SIN(B6)");
            SetCell(list1, 1, 6, "=EXP(-0,03*50)");

            SetCell(list1, 17, 5, "sig1");
            SetCell(list1, 17, 6, "=0,816*D15*COS(D17-1,046)-D16*0,577");
            SetCell(list1, 18, 5, "sig2");
            SetCell(list1, 18, 6, "=0,816*D15*COS(D17+1,046)-D16*0,577");
            SetCell(list1, 19, 5, "sig3");
            SetCell(list1, 19, 6, "=(-0,816)*D15*COS(D17)-D16*0,577");


            SetCell(list1, 4, 9, "=1,732/4");
            SetCell(list1, 5, 9, "=39*3,14/180");
            SetCell(list1, 6, 9, "1,9636");
            SetCell(list1, 7, 9, "=0,984*180/3,14");
            SetCell(list1, 8, 9, "=112,563");

            SetCell(list1, 10, 9, "=0,857*180/3,14");
            SetCell(list1, 11, 9, "=ATAN(0,62)");
            SetCell(list1, 12, 9, "=ATAN(0,85)*180/3,14");
            SetCell(list1, 13, 9, "=ATAN(0,457)");

            SetCell(list1, 15, 9, "=TAN(0,7273)");
            SetCell(list1, 16, 9, "=ATAN(0,585)");
            SetCell(list1, 17, 9, "=0,882*180/3,14");

            SetCell(list1, 4, 10, "=(0,5*54-0,433*52)/(0,5*54-0,25*52)");
            SetCell(list1, 5, 10, "=0,5*54*0,434");
            SetCell(list1, 6, 10, "=36,6*3,14/180");


            workbook.SaveAs(pathToFile);

            workbook.Close();
            app.Quit();
        }
    }

    public class Data
    {
        public double c0 { get; set; }
        public double c90 { get; set; }
        public double ro0 { get; set; }
        public double ro90 { get; set; }
        public double SIG2 { get; set; }
        public double SIG3 { get; set; }
        public double Betta { get; set; }


        // Calculating parametrs
        public double psi { get; set; }
        public double SIG1 { get; set; }

        private double ro;
        private double S;
        private double S_;
        private double k_tag_psi;
        private double c;
        private double k_;
        private double c_;
        private double ro_;
        private double n1;
        private double n2;
        private double m1;
        private double p;

        private double ЭТА;
        private double КСИ;
        private double ТЭТА;
        private double SIG1_MINES_SIG3;
        private double SIG1_PLUS_SIG3;

        // Result values
        private double PI = 3.14;// Math.PI;
        public double F1 => p * p * SIG1_MINES_SIG3 * SIG1_MINES_SIG3 / (Math.Cos(ro) * Math.Cos(ro)) - 4 * S * S * (1 - k_ * Math.Cos(ro) * Math.Cos(ro) * (1 - 0.25 * k_)) - m1;
        public double F2 => Math.Cos(2 * Betta) * (S * n2 + 0.5 * S_ * Math.Sin(2 * psi - ro)) - Math.Sin(2 * Betta) * (S * n1 + 0.5 * S_ * Math.Cos(2 * psi - ro));

        public void CalculateValues()
        {
            SIG2 = SIG3 * 1.5;

            ЭТА = Math.Sqrt((SIG1 - SIG3) * (SIG1 - SIG3) + (SIG1 - SIG2) * (SIG1 - SIG2) + (SIG3 - SIG2) * (SIG3 - SIG2)) / Math.Sqrt(3);
            КСИ = (SIG1 + SIG3 + SIG2) / Math.Sqrt(3);
            ТЭТА = (0 * PI) / 180;

            SIG1_MINES_SIG3 = 0.816 * ЭТА * (Math.Cos(ТЭТА) - Math.Cos(ТЭТА + (2 * PI / 3)));
            SIG1_PLUS_SIG3 = 2 / Math.Sqrt(3) * КСИ + (0.816 * ЭТА * (Math.Cos(ТЭТА) + Math.Cos(ТЭТА + (2 * PI / 3))));

            ro = ro0 * Math.Cos(psi) * Math.Cos(psi) + ro90 * Math.Sin(psi) * Math.Sin(psi);
            c = c0 * Math.Cos(psi) * Math.Cos(psi) + c90 * Math.Sin(psi) * Math.Sin(psi);
            c_ = -0.5 * (c0 - c90) * Math.Sin(2 * psi);
            k_ = -(Math.Tan(ro0) - Math.Tan(ro90)) * Math.Sin(2 * psi);
            k_tag_psi = Math.Tan(ro0) * Math.Cos(psi) * Math.Cos(psi) + Math.Tan(ro90) * Math.Sin(psi) * Math.Sin(psi);
            m1 = -S * S_ * k_ * Math.Sin(ro) * Math.Sin(ro) + S_ * S_;
            S = 0.5 * SIG1_PLUS_SIG3 * k_tag_psi + c;
            S_ = 0.5 * SIG1_PLUS_SIG3 * k_ + c_;
            ro_ = -0.5 * (ro0 - ro90) * Math.Sin(2 * psi);
            n1 = Math.Sin(2 * psi - ro) - 0.5 * ro_ * Math.Sin(2 * psi) / Math.Cos(ro);
            n2 = Math.Cos(2 * psi - ro) - 0.5 * ro_ * Math.Cos(2 * psi) / Math.Cos(ro);
            p = 1 - 0.5 * k_ * Math.Cos(ro) * Math.Cos(ro);
        }

        public void ChooseValues(double abs_value_max)
        {
            double a = 0, c = 0, b = 0;
            double a_value = 0, b_value = 0, c_value = 0;

            // F2 dependents psi
            psi = 0;
            a = 0;
            b = 3;
            c = (a + b) / 2;

            psi = a;
            CalculateValues();
            a_value = F2;

            psi = b;
            CalculateValues();
            b_value = F2;

            psi = c;
            CalculateValues();
            c_value = F2;

            while (Math.Abs(c_value) > abs_value_max)
            {
                if (Math.Abs(b_value) > Math.Abs(a_value))
                {
                    double new_b = a;

                    do
                    {
                        new_b = (new_b + b) / 2;

                        psi = new_b;
                        CalculateValues();
                        b_value = F2;
                    } while (b_value < 0);

                    b = new_b;
                }
                else
                {
                    double new_a = b;

                    do
                    {
                        new_a = (a + new_a) / 2;

                        psi = new_a;
                        CalculateValues();
                        a_value = F2;
                    } while (a_value > 0);

                    a = new_a;
                }

                c = (a + b) / 2;

                psi = c;
                CalculateValues();
                c_value = F2;
            }

            // F1 dependents SIG1
            a = 0;
            b = 500;
            c = (a + b) / 2;

            SIG1 = a;
            CalculateValues();
            a_value = F1;

            SIG1 = b;
            CalculateValues();
            b_value = F1;

            SIG1 = c;
            CalculateValues();
            c_value = F1;

            while (Math.Abs(c_value) > abs_value_max)
            {
                if (Math.Abs(b_value) > Math.Abs(a_value))
                {
                    double new_b = a;

                    do
                    {
                        new_b = (new_b + b) / 2;

                        SIG1 = new_b;
                        CalculateValues();
                        b_value = F1;
                    } while (b_value < 0);

                    b = new_b;
                }
                else
                {
                    double new_a = b;

                    do
                    {
                        new_a = (a + new_a) / 2;

                        SIG1 = new_a;
                        CalculateValues();
                        a_value = F1;
                    } while (a_value > 0);

                    a = new_a;
                }

                c = (a + b) / 2;

                SIG1 = c;
                CalculateValues();
                c_value = F1;
            }
        }
    }
}
