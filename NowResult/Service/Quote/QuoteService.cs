using Jint;
using NowResult.Service.HttpRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NowResult
{
    class QuoteService
    {
        private List<List<String>> elementiEarly = new List<List<String>>();
        private List<List<String>> elementiLive = new List<List<String>>();
        private String casa;
        private List<List<List<String>>> oggetto;
        private String fuori;
        private String idDettaglio = String.Empty;
        private readonly HttpRequestService httpRequestService = new HttpRequestService();

        public void parsingQuote(string url, string risultatoMatch, string idDettaglio, NowResult.Quote quote)
        {
            this.idDettaglio = idDettaglio;
            try
            {
                string html = httpRequestService.call(url + ".js");
                var engine = new Engine()
                   .Execute(html);
                casa = engine.GetValue("hometeam").ToString();
                fuori = engine.GetValue("guestteam").ToString();
                quote.home.Content = engine.GetValue("hometeam").ToString();
                quote.away.Content = engine.GetValue("guestteam").ToString();
                quote.risultato.Content = risultatoMatch;

                var partita = engine.GetValue("game").ToObject();

                IEnumerable<object> valori = (IEnumerable<object>)partita;
                foreach (object valore in valori)
                {
                    string[] boomMakers = valore.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    List<String> listaEarly = new List<String>();
                    List<String> listaLive = new List<String>();

                    if (boomMakers.Length == 17)
                    {
                        listaLive.Add(boomMakers[2]);
                        listaLive.Add("Live");
                        listaLive.Add(boomMakers[3]);
                        listaLive.Add(boomMakers[4]);
                        listaLive.Add(boomMakers[5]);
                        listaLive.Add(boomMakers[6]);
                        listaLive.Add(boomMakers[7]);
                        listaLive.Add(boomMakers[8]);
                        listaLive.Add(boomMakers[9]);
                        listaLive.Add(boomMakers[0]);
                        quote.lista.Items.Add(new Quota { Nome = boomMakers[2], TipoQuota = "Live", Hw = boomMakers[3], Dw = boomMakers[4], Aw = boomMakers[5], pHw = boomMakers[6], pDw = boomMakers[7], pAw = boomMakers[8], pRe = boomMakers[9], idOdds = boomMakers[0] });
                    }
                    else
                    {
                        listaLive.Add(boomMakers[2]);
                        listaLive.Add("Live");
                        listaLive.Add(boomMakers[10]);
                        listaLive.Add(boomMakers[11]);
                        listaLive.Add(boomMakers[12]);
                        listaLive.Add(boomMakers[13]);
                        listaLive.Add(boomMakers[14]);
                        listaLive.Add(boomMakers[15]);
                        listaLive.Add(boomMakers[16]);
                        listaLive.Add(boomMakers[0]);
                        quote.lista.Items.Add(new Quota { Nome = boomMakers[2], TipoQuota = "Live", Hw = boomMakers[10], Dw = boomMakers[11], Aw = boomMakers[12], pHw = boomMakers[13], pDw = boomMakers[14], pAw = boomMakers[15], pRe = boomMakers[16], idOdds = boomMakers[0] });
                        listaEarly.Add(boomMakers[2]);
                        listaEarly.Add("Early");
                        listaEarly.Add(boomMakers[3]);
                        listaEarly.Add(boomMakers[4]);
                        listaEarly.Add(boomMakers[5]);
                        listaEarly.Add(boomMakers[6]);
                        listaEarly.Add(boomMakers[7]);
                        listaEarly.Add(boomMakers[8]);
                        listaEarly.Add(boomMakers[9]);
                        listaEarly.Add(boomMakers[0]);
                        quote.lista.Items.Add(new Quota { Nome = boomMakers[2], TipoQuota = "Early", Hw = boomMakers[3], Dw = boomMakers[4], Aw = boomMakers[5], pHw = boomMakers[6], pDw = boomMakers[7], pAw = boomMakers[8], pRe = boomMakers[9], idOdds = boomMakers[0] });
                    }
                    elementiEarly.Add(listaEarly);
                    elementiLive.Add(listaLive);
                }
            }
            catch
            {
                parsingQuote(url, risultatoMatch, idDettaglio, quote);
            }

        }




        public void threadCalcolo(ComboBoxItem qc, ComboBoxItem sc, NowResult.Quote quote)
        {
            String contenutoQc = null;
            String contenutoSc = null;
            if (qc != null && sc != null)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    contenutoQc = qc.Content.ToString();
                    contenutoSc = sc.Content.ToString();
                }), DispatcherPriority.ContextIdle);
                if (contenutoQc.Equals("Early"))
                {
                    if (contenutoSc.Equals("Early"))
                    {
                        oggetto = new CalcoloService().Calcolos(null, elementiEarly, 1);
                    }
                    else if (contenutoSc.Equals("Live"))
                    {
                        oggetto = new CalcoloService().Calcolos(null, elementiEarly, 2);
                    }
                    else
                    {
                        oggetto = new CalcoloService().Calcolos(null, elementiEarly, 3);
                    }
                }
                else if (contenutoQc.ToString().Equals("Live"))
                {
                    if (contenutoSc.Equals("Early"))
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, null, 1);
                    }
                    else if (contenutoSc.Equals("Live"))
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, null, 2);
                    }
                    else
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, null, 3);
                    }
                }
                else
                {
                    if (contenutoSc.Equals("Early"))
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, elementiEarly, 1);
                    }
                    else if (contenutoSc.Equals("Live"))
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, elementiEarly, 2);
                    }
                    else
                    {
                        oggetto = new CalcoloService().Calcolos(elementiLive, elementiEarly, 3);
                    }
                }
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    quote.Close();
                    new Risultati(oggetto[0], oggetto[1], casa, fuori).Show();
                }));
            }
            else
            {
                MessageBox.Show("Devi compilare i campi del form!", "Errore", MessageBoxButton.OK);
            }
        }


    }
}
