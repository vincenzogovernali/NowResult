using Jint;
using NowResult.Service.HttpRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NowResult
{
    class CalcoloService
    {
        public String baseUrl = Properties.Settings.Default.GoalData;
        public int page = 1;
        public List<List<String>> listeEarly = new List<List<String>>();
        public List<List<String>> listeLive = new List<List<String>>();
        private readonly HttpRequestService httpRequestService = new HttpRequestService();

        public List<List<List<String>>> Calcolos(List<List<String>> listaLive, List<List<String>> listaEarly, int operazione)
        {

            //QUOTE EARLY
            if (listaLive == null)
            {
                //STORICO EARLY
                if (operazione == 1)
                {
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("QUOTE EARLY");
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(new List<List<String>>());
                    lista.Add(listeEarly);
                    return lista;
                }
                //STORICO LIVE
                else if (operazione == 2)
                {
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("QUOTE LIVE");
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeLive);
                    lista.Add(new List<List<String>>());
                    return lista;
                }
                //TUTTI I STORICI
                else
                {
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("QUOTE EARLY E LIVE");
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeLive);
                    lista.Add(listeEarly);
                    return lista;
                }
            }
            //QUOTE LIVE
            else if (listaEarly == null)
            {
                //STORICO EARLY
                if (operazione == 1)
                {
                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("STORICO EARLY");
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(new List<List<String>>());
                    lista.Add(listeEarly);
                    return lista;
                }
                //STORICO LIVE
                else if (operazione == 2)
                {
                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("STORICO LIVE");
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeLive);
                    lista.Add(new List<List<String>>());
                    return lista;
                }
                else
                {
                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            Console.WriteLine("STORICO EARLY E LIVE");
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeEarly);
                    lista.Add(listeLive);
                    return lista;
                }
            }
            //TUTTE LE QUOTE
            else
            {
                if (operazione == 1)
                {

                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(new List<List<String>>());
                    lista.Add(listeEarly);
                    return lista;
                }
                else if (operazione == 2)
                {
                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeLive);
                    lista.Add(new List<List<String>>());
                    return lista;
                }
                else
                {
                    foreach (List<String> liste in listaLive)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    foreach (List<String> liste in listaEarly)
                    {
                        if (liste.Count != 0)
                        {
                            chiamataEarly(liste[2], liste[3], liste[4], liste[9]);
                            chiamataLive(liste[2], liste[3], liste[4], liste[9]);
                        }
                    }
                    List<List<List<String>>> lista = new List<List<List<String>>>();
                    lista.Add(listeEarly);
                    lista.Add(listeLive);
                    return lista;
                }
            }
        }



        public void chiamataEarly(String hWin, String aWin, String dWin, String cid)
        {
            String urlBase = baseUrl + "p=" + page +
                           "&t=5" + "&hw=" + hWin + "&gw=" + dWin + "&g=" + aWin + "&cid=" + cid;
            try
            {
                string html = httpRequestService.call(urlBase);

                var engine = new Engine()
                   .Execute(html);

                String[] pagine = engine.GetValue("goalPageInfo").ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                var oggetti = engine.GetValue("goalPageData").ToObject();

                IEnumerable<object> quote = (IEnumerable<object>)oggetti;
                foreach (object quota in quote)
                {
                    Console.WriteLine("QUOTA");
                    List<String> array = new List<String>();
                    IEnumerable<object> statistiche = (IEnumerable<object>)quota;
                    foreach (var statistica in statistiche)
                    {
                        array.Add(statistica.ToString());
                    }
                    listeEarly.Add(array);
                }
                Console.WriteLine("--- Prima Pagina Early Grabbata ---");
                if (pagine[0] != pagine[1])
                {
                    chiamataEarlyTuttePagine(hWin, aWin, dWin, cid, pagine[0]);
                }
            }
            catch { Console.WriteLine("--- Errore Nel Grab Della Prima Pagina Early ---"); }
        }

        public void chiamataLive(String hWin, String aWin, String dWin, String cid)
        {
            String urlBase = baseUrl + "p=" + page +
                           "&t=6" + "&hw=" + hWin + "&gw=" + dWin + "&g=" + aWin + "&cid=" + cid;
            try
            {
                string html = httpRequestService.call(urlBase);
                var engine = new Engine()
                   .Execute(html);

                String[] pagine = engine.GetValue("goalPageInfo").ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                var oggetti = engine.GetValue("goalPageData").ToObject();

                IEnumerable<object> quote = (IEnumerable<object>)oggetti;
                foreach (object quota in quote)
                {
                    Console.WriteLine("QUOTA");
                    List<String> array = new List<String>();
                    IEnumerable<object> statistiche = (IEnumerable<object>)quota;
                    foreach (var statistica in statistiche)
                    {
                        array.Add(statistica.ToString());
                    }
                    listeLive.Add(array);
                }
                Console.WriteLine("--- Prima Pagina Early Grabbata ---");
                if (pagine[0] != pagine[1])
                {
                    chiamataLiveTuttePagine(hWin, aWin, dWin, cid, pagine[0]);
                }
            }
            catch { Console.WriteLine("--- Errore Nel Grab Della Prima Pagina Live ---"); }
        }


        public void chiamataLiveTuttePagine(String hWin, String aWin, String dWin, String cid, String pagina)
        {
            for (int i = 2; i <= int.Parse(pagina); i++)
            {
                String urlBase = baseUrl + "p=" + i +
                          "&t=6" + "&hw=" + hWin + "&gw=" + dWin + "&g=" + aWin + "&cid=" + cid;
                try
                {
                    string html = httpRequestService.call(urlBase);
                    var engine = new Engine()
                       .Execute(html);

                    var oggetti = engine.GetValue("goalPageData").ToObject();

                    IEnumerable<object> quote = (IEnumerable<object>)oggetti;
                    foreach (object quota in quote)
                    {
                        Console.WriteLine("QUOTA");
                        List<String> array = new List<String>();
                        IEnumerable<object> statistiche = (IEnumerable<object>)quota;
                        foreach (var statistica in statistiche)
                        {
                            array.Add(statistica.ToString());
                            Console.WriteLine("Chiamata");
                        }
                        listeLive.Add(array);
                    }
                }
                catch { Console.WriteLine("--- Errore Nel Grab Della " + i + "° Pagina Live ---"); }
            }


        }

        public void chiamataEarlyTuttePagine(String hWin, String aWin, String dWin, String cid, String pagina)
        {
            for (int i = 2; i <= int.Parse(pagina); i++)
            {
                String urlBase = baseUrl + "p=" + i +
                           "&t=5" + "&hw=" + hWin + "&gw=" + dWin + "&g=" + aWin + "&cid=" + cid;
                try
                {
                    string html = httpRequestService.call(urlBase);
                    var engine = new Engine()
                       .Execute(html);

                    var oggetti = engine.GetValue("goalPageData").ToObject();

                    IEnumerable<object> quote = (IEnumerable<object>)oggetti;

                    foreach (object quota in quote)
                    {
                        List<String> array = new List<String>();
                        IEnumerable<object> statistiche = (IEnumerable<object>)quota;
                        foreach (var statistica in statistiche)
                        {
                            array.Add(statistica.ToString());
                            Console.WriteLine("Chiamata");
                        }
                        listeEarly.Add(array);
                    }

                }
                catch { Console.WriteLine("--- Errore Nel Grab Della " + i + "° Pagina Early ---"); }
            }
        }
    }
}
