using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace NowResult
{
    class Database
    {
        public SQLiteConnection connessione = new SQLiteConnection();
        public void apriConnessione()
        {
            try
            {
                connessione = new SQLiteConnection("Data Source=Resource/Database/Database.db;Version=3;");
                connessione.Open();
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
        public void chiudiConnessione()
        {
            try
            {
                connessione.Close();
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
        public List<List<String>> selectFromCasaFuori(String casa, String fuori)
        {
            String sql = "SELECT * FROM AsianBet WHERE SQUADRACASA LIKE '" + casa[0] + casa[1] + casa[2] + casa[3] + "%' and SQUADRAOSPITE LIKE '" + fuori[0] + fuori[1] + fuori[2] + fuori[3] + "%';";
            try
            {
                List<List<String>> listaPrincipale = new List<List<String>>();
                SQLiteCommand command = new SQLiteCommand(sql, connessione);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<String> listaSecondaria = new List<String>();
                    listaSecondaria.Add(reader["ORARIO"].ToString());
                    listaSecondaria.Add(reader["SQUADRACASA"].ToString());
                    listaSecondaria.Add(reader["CURRENTSPREADCASA"].ToString());
                    listaSecondaria.Add(reader["CURRENTODDSCASA"].ToString());
                    listaSecondaria.Add(reader["OPENSPREADCASA"].ToString());
                    listaSecondaria.Add(reader["OPENODDSCASA"].ToString());
                    listaSecondaria.Add(reader["TOTALCURRENTCASA"].ToString());
                    listaSecondaria.Add(reader["TOTALOPENCASA"].ToString());
                    listaSecondaria.Add(reader["TIPOCASA"].ToString());
                    listaSecondaria.Add(reader["CORRENTECASA"].ToString());
                    listaSecondaria.Add(reader["APERTURACASA"].ToString());
                    listaSecondaria.Add(reader["SQUADRAOSPITE"].ToString());
                    listaSecondaria.Add(reader["CURRENTSPREADOSPITE"].ToString());
                    listaSecondaria.Add(reader["CURRENTODDSOSPITE"].ToString());
                    listaSecondaria.Add(reader["OPENSPREADOSPITE"].ToString());
                    listaSecondaria.Add(reader["OPENODDSOSPITE"].ToString());
                    listaSecondaria.Add(reader["TIPOOSPITE"].ToString());
                    listaSecondaria.Add(reader["CORRENTEOSPITE"].ToString());
                    listaSecondaria.Add(reader["APERTURAOSPITE"].ToString());
                    listaSecondaria.Add(reader["ORARIOGRAB"].ToString());
                    listaPrincipale.Add(listaSecondaria);
                }
                return listaPrincipale;
            }
            catch { return new List<List<String>>(); }

        }


        public void insert(List<String> lista)
        {
            String sql = "INSERT INTO AsianBet (ORARIO, SQUADRACASA, CURRENTSPREADCASA, CURRENTODDSCASA, OPENSPREADCASA, OPENODDSCASA, TOTALCURRENTCASA, " +
                "TOTALOPENCASA, TIPOCASA, CORRENTECASA, APERTURACASA, SQUADRAOSPITE,CURRENTSPREADOSPITE, CURRENTODDSOSPITE, OPENSPREADOSPITE, OPENODDSOSPITE, TIPOOSPITE, CORRENTEOSPITE, APERTURAOSPITE, ORARIOGRAB) " +
                "VALUES('" + lista[0] + "','" + lista[1] + "','" + lista[2] + "','" + lista[3] + "','" + lista[4] + "','" + lista[5] + "','" + lista[6] + "','"
                + lista[7] + "','" + lista[8] + "','" + lista[9] + "','" + lista[10] + "','" + lista[11] + "','" + lista[12] + "','" + lista[13] + "','"
               + lista[14] + "','" + lista[15] + "','" + lista[16] + "','" + lista[17] + "','" + lista[18] + "','" + lista[19] + "');";
            SQLiteCommand command = new SQLiteCommand(sql, connessione);
            command.ExecuteNonQuery();
        }
    }
}
