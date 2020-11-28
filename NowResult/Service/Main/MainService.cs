using HtmlAgilityPack;
using Jint;
using NowResult.Service.HttpRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace NowResult
{
    class MainService
    {
        private readonly String urlBase = Properties.Settings.Default.UrlBase;
        private readonly String urlDati = Properties.Settings.Default.UrlDati;
        private readonly ArrayList elementi = new ArrayList();
        private readonly List<String> listaAsian = new List<String>();
        private readonly HttpRequestService httpRequestService = new HttpRequestService();

        public void aggiornaPartiteAsync(NowResult.Main main)
        {
            while (true)
            {
                try
                {
                    string html = httpRequestService.call(urlBase + urlDati);
                    html = html.Replace("ShowBf();", "");
                    var engine = new Engine()
                        .Execute(html)
                        .GetValue("A");

                    IEnumerable<object> matchs = (IEnumerable<object>)engine.ToObject();
                    foreach (object match in matchs)
                    {
                        if (match != null)
                        {
                            elementi.Clear();
                            IEnumerable<object> statistiche = (IEnumerable<object>)match;
                            foreach (var statistica in statistiche)
                            {
                                if (statistica != null)
                                {
                                    elementi.Add(statistica);
                                }
                            }
                            HtmlDocument htmlDoc = new HtmlDocument();
                            htmlDoc.LoadHtml(elementi[4].ToString());
                            string casa = htmlDoc.DocumentNode.InnerText;
                            htmlDoc.LoadHtml(elementi[5].ToString());
                            string fuori = htmlDoc.DocumentNode.InnerText;
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                main.lista.Items.Add(new Partita { inizio = elementi[6].ToString().Replace(",", ":"), tempo = elementi[7].ToString().Replace(",", ":"), casa = casa.Replace("  ", " "), risultato = elementi[9].ToString() + "-" + elementi[10].ToString(), fuori = fuori.Replace("  ", " "), dettagli = elementi[0].ToString() });
                            }), DispatcherPriority.ContextIdle);

                        }
                    }
                    Thread.Sleep(30000);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        main.lista.Items.Clear();
                    }), DispatcherPriority.ContextIdle);
                }
                catch
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Il sito è a pieno carico, riprova più tardi", "Errore", MessageBoxButton.OK);
                        aggiornaPartiteAsync(main);
                    }), DispatcherPriority.ContextIdle);
                }
            }
        }

        public void aggiornaQuoteAsianBet(NowResult.Main main)
        {
            String asianBetUrl = Properties.Settings.Default.AsianBet;
            while (true)
            {
                string html = httpRequestService.call(asianBetUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                Database db = new Database();
                db.apriConnessione();
                foreach (HtmlNode righe in doc.DocumentNode.SelectNodes("//tr[@class='main']"))
                {
                    for (int i = 0; i < righe.SelectNodes("td").Count; i++)
                    {
                        listaAsian.Add(righe.SelectNodes("td")[i].InnerText);
                        if (listaAsian.Count == 19)
                        {
                            listaAsian.Add(DateTime.Now.ToString());
                            db.insert(listaAsian);
                            listaAsian.Clear();
                        }
                    }
                }
                Thread.Sleep(180000);
            }
        }
    }
}
