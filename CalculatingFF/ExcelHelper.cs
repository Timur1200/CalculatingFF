﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
//using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
using System.Linq;

namespace CalculatingFF
{
    internal class ExcelHelper
    {
        private void ReplaceTemplateValues(string templatePath, string outputPath, Dictionary<string, double> replacements)
        {
            using (var workbook = new XLWorkbook(templatePath))
            {
                foreach (var worksheet in workbook.Worksheets)
                {
                    foreach (var cell in worksheet.Cells())
                    {
                        if (cell.HasFormula)
                        {
                            // Если ячейка содержит формулу, пропускаем её
                            continue;
                        }

                        foreach (var replacement in replacements)
                        {
                            if (cell.Value.ToString() == replacement.Key)
                            {
                                cell.Value = replacement.Value;
                            }
                        }
                    }
                }

                workbook.SaveAs(outputPath);
            }
        }

        public void ToExcel1(Model1 q, double[,] nums)
        {
            // Путь к шаблону
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "excel1.xlsx");

            // Словарь замен
            var replacements = new Dictionary<string, double>
    {
        { "_b1", q.B },
        { "_b6", q.B6 },
        { "_b8", q.B8 },
        { "_b9", q.B9 },
        { "_b10", q.B10 },
        { "_b11", q.B11 },
        { "_b12", q.B12 },
        { "_b13", q.B13 },
    };

            // Диалоговое окно для выбора пути сохранения
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string outputPath = saveFileDialog.FileName;

                // Замена значений в шаблоне
                ReplaceTemplateValues(templatePath, outputPath, replacements);

                // Заполнение таблицы значениями из nums
                if (nums != null) FillTable(outputPath, q, nums);


                MessageBox.Show("Файл успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void FillTable(string outputPath, Model1 q, double[,] nums)
        {
            using (var workbook = new XLWorkbook(outputPath))
            {
                var worksheet = workbook.Worksheet(1); // Предполагаем, что таблица находится на первом листе

                // Определяем начальную ячейку для верхней шапки (E15)
                int startRowHeader = 15;
                int startColHeader = 4; // E

                // Определяем начальную ячейку для левой шапки (C16)
                int startRowLeftHeader = 16;
                int startColLeftHeader = 3; // C

                // Определяем начальную ячейку для значений таблицы (D16)
                int startRowValues = 16;
                int startColValues = 4; // D

                // Заполняем верхнюю шапку таблицы значениями из _Model.Sig2List
                for (int col = 0; col < q.Sig2List.Count; col++)
                {
                    worksheet.Cell(startRowHeader, startColHeader + col).Value = q.Sig2List[col];
                }

                // Заполняем левую шапку таблицы значениями из _Model.BettaList
                for (int row = 0; row < q.BettaList.Count; row++)
                {
                    worksheet.Cell(startRowLeftHeader + row, startColLeftHeader).Value = q.BettaList[row];
                }

                // Заполняем значения таблицы
                for (int row = 0; row < q.BettaList.Count; row++)
                {
                    for (int col = 0; col < q.Sig2List.Count; col++)
                    {
                        worksheet.Cell(startRowValues + row, startColValues + col).Value = nums[row, col];
                    }
                }

                workbook.Save();
            }
        }

        public void ToExcel2(Model2 q)
        {
            // Путь к шаблону
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "excel2.xlsx");

            // Словарь замен
            var replacements = new Dictionary<string, double>
            {
                { "_s11", q.S11 },
                { "_s31", q.S31 },
                { "_s21", q.S21 },
                { "_s23", q.S23 },
                { "_s13", q.S13 },
                { "_s33", q.S33 },
                { "_a1", q.A1 },
                { "_a2", q.A2 },
                { "_a3", q.A3 },
                { "_ksr", q.Ksr },
               
                { "_c0", q.C0 },
                { "_c90", q.C90 },

                //{"_", q. },
               
            };

            // Диалоговое окно для выбора пути сохранения
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string outputPath = saveFileDialog.FileName;
                ReplaceTemplateValues(templatePath, outputPath, replacements);
                MessageBox.Show("Файл успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ToExcel3(Model3 q)
        {
            // Путь к шаблону
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "excel3.xlsx");

            // Словарь замен
            var replacements = new Dictionary<string, double>
            {
                { "_s11", q.S11 },
                { "_s31", q.S31 },
                { "_s12", q.S12 },
                { "_s32", q.S32 },
                { "_s13", q.S13 },
                { "_s33", q.S33 },
                { "_a1", q.A1 },
                { "_a2", q.A2 },
                { "_a3", q.A3 },
                { "_k", q.K },
                { "_t1", q.T1 },
                { "_c0", q.C0 },
                { "_c90", q.C90 },
               
                
            };

            // Диалоговое окно для выбора пути сохранения
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string outputPath = saveFileDialog.FileName;
                ReplaceTemplateValues(templatePath, outputPath, replacements);
                MessageBox.Show("Файл успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
   
}

           

           
     

