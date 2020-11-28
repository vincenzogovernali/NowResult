using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace NowResult
{
    class RisultatiService
    {
        private List<List<String>> newEarly = new List<List<string>>();
        private List<List<String>> newLive = new List<List<string>>();
        private List<List<String>> calcoloEarly = new List<List<string>>();
        private List<List<String>> calcoloLive = new List<List<string>>();
        private ExcelPackage excel = new ExcelPackage();


        private void stampaEarlyeLive(ExcelWorksheet file)
        {
            int riga = 3;
            int colonna = 1;
            file.Column(1).Width = 20;
            file.Column(2).Width = 20;
            file.Column(3).Width = 20;
            file.Column(4).Width = 20;
            file.Column(5).Width = 20;
            file.Column(6).Width = 20;
            file.Column(7).Width = 20;
            String[] headers = new String[] { "Campionato", "Data", "Squadra di casa", "Squadra ospite", "Risultato finale", "Primo tempo", "Conteggio", "Early" };
            foreach (string header in headers)
            {
                file.Cells[riga, colonna].LoadFromText(header);
                colonna++;
            }
            colonna = 1;
            riga++;
            foreach (List<String> test in newEarly)
            {
                foreach (String stringa in test)
                {
                    file.Cells[riga, colonna].LoadFromText(stringa);
                    colonna++;
                }
                riga++;
                colonna = 1;
            }
            foreach (List<String> test in newLive)
            {
                foreach (String stringa in test)
                {
                    file.Cells[riga, colonna].LoadFromText(stringa);
                    colonna++;
                }
                riga++;
                colonna = 1;
            }
        }


        public void calcolaLive(List<String> valori)
        {
            bool test = false;
            int indice = 0;
            int newCount = 0;
            if (newLive.Count == 0)
            {
                newLive.Add(new List<string> { valori[3], valori[4], valori[10], valori[11], "'" + valori[12] + "-" + valori[13], "'" + valori[14] + "-" + valori[15], "1", "Live" });
            }
            else
            {
                foreach (List<String> dett in newLive)
                {
                    if (dett[4].Equals("'" + valori[12] + "-" + valori[13]) && dett[5].Equals("'" + valori[14] + "-" + valori[15]))
                    {
                        test = true;
                        indice = newLive.IndexOf(dett);
                        newCount = int.Parse(dett[6]);
                        newCount++;
                    }
                }
                if (test)
                {
                    List<String> change = newLive[indice];
                    change[6] = newCount.ToString();
                }
                else
                {
                    newLive.Add(new List<string> { valori[3], valori[4], valori[10], valori[11], "'" + valori[12] + "-" + valori[13], "'" + valori[14] + "-" + valori[15], "1", "Live" });
                }
            }
        }


        public void generaExcel(string casa, string fuori)
        {
            ExcelWorksheet file = excel.Workbook.Worksheets.Add("Storici");
            file.Cells[1, 1].LoadFromText("Squadra di casa");
            file.Cells[1, 2].LoadFromText(casa);
            file.Cells[1, 3].LoadFromText("-");
            file.Cells[1, 4].LoadFromText("Squadra fuori casa");
            file.Cells[1, 5].LoadFromText(fuori);
            stampaEarlyeLive(file);
            stampaAsianBet(file, casa, fuori);
            FileInfo excelFile = new FileInfo(@"./excelExport.xlsx");
            excel.SaveAs(excelFile);
        }


        public void calcolaEarly(List<String> valori)
        {
            bool test = false;
            int indice = 0;
            int newCount = 0;
            if (newEarly.Count == 0)
            {
                newEarly.Add(new List<string> { valori[3], valori[4], valori[10], valori[11], "'" + valori[12] + "-" + valori[13], "'" + valori[14] + "-" + valori[15], "1", "Early" });
            }
            else
            {
                foreach (List<String> dett in newEarly)
                {
                    if (dett[4].Equals("'" + valori[12] + "-" + valori[13]) && dett[5].Equals("'" + valori[14] + "-" + valori[15]))
                    {
                        test = true;
                        indice = newEarly.IndexOf(dett);
                        newCount = int.Parse(dett[6]);
                        newCount++;
                    }
                }
                if (test)
                {
                    List<String> change = newEarly[indice];
                    change[6] = newCount.ToString();
                }
                else
                {
                    newEarly.Add(new List<string> { valori[3], valori[4], valori[10], valori[11], "'" + valori[12] + "-" + valori[13], "'" + valori[14] + "-" + valori[15], "1", "Early" });
                }
            }
        }

        private void stampaAsianBet(ExcelWorksheet file, String casa, String fuori)
        {
            try
            {
                int riga = 1;
                int colonna = 1;
                ExcelWorksheet files = file.Workbook.Worksheets.Add("AsianBet");
                for (int i = 1; i <= 20; i++)
                {
                    files.Column(i).Width = 20;
                }
                String[] headers = new String[] {"ORARIO", "SQUADRACASA", "CURRENTSPREADCASA","CURRENTODDSCASA","OPENSPREADCASA", "OPENODDSCASA", "TOTALCURRENTCASA",
                "TOTALOPENCASA", "TIPOCASA", "CORRENTECASA", "APERTURACASA", "SQUADRAOSPITE","CURRENTSPREADOSPITE", "CURRENTODDSOSPITE", "OPENSPREADOSPITE", "OPENODDSOSPITE", "TIPOOSPITE", "CORRENTEOSPITE", "APERTURAOSPITE", "ORARIOGRAB"};

                foreach (string header in headers)
                {
                    files.Cells[riga, colonna].LoadFromText(header);
                    colonna++;
                }
                colonna = 1;
                riga++;
                Database db = new Database();
                db.apriConnessione();
                List<List<String>> asianBet = db.selectFromCasaFuori(casa, fuori);
                foreach (List<String> a in asianBet)
                {
                    foreach (String st in a)
                    {
                        files.Cells[riga, colonna].LoadFromText(st.ToString());
                        colonna++;
                    }
                    colonna = 1;
                    riga++;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
