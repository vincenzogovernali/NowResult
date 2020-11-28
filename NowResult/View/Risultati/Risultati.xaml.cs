using System;
using System.Collections.Generic;
using System.Windows;

namespace NowResult
{
    public partial class Risultati : Window
    {
        private RisultatiService risultatiService = new RisultatiService();
        private int nLive = 0;
        private int nEarly = 0;

        public Risultati(List<List<String>> listeLive, List<List<String>> listeEarly, String casa, String fuori)
        {
            InitializeComponent();
            if (listeEarly != null)
            {
                nEarly = listeEarly.Count;
                nStoriciEarly.Content = nEarly;
                foreach (List<String> storici in listeEarly)
                {
                    risultatiService.calcolaEarly(storici);
                }

            }
            if (listeLive != null)
            {
                nLive = listeLive.Count;
                nStoriciLive.Content = nLive;
                foreach (List<String> storici in listeLive)
                {
                    risultatiService.calcolaLive(storici);
                }
            }
            risultatiService.generaExcel(casa, fuori);
        }
    }
}