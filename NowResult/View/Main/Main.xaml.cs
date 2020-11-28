using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace NowResult
{
    public partial class Main : Window
    {
        private MainService mainService = new MainService();
        private String urlDettagli = Properties.Settings.Default.UrlDettagli;
        private Thread updateNowGoal;
        private Thread updateAsianBet;

        //MAIN METHOD FOR START THE THREADS
        public Main()
        {
            try
            {
                InitializeComponent();
                updateNowGoal = new Thread(aggiornaPartiteAsync);
                updateAsianBet = new Thread(aggiornaQuoteAsianBet);
                updateNowGoal.Start();
                updateAsianBet.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Object items = (sender as ListViewItem).Content;
            Partita valori = (Partita)items;
            String risultato = valori.risultato;
            String dettaglio = urlDettagli + valori.dettagli;
            Quote quote = new Quote(dettaglio, risultato, valori.dettagli);
            quote.Show();
        }

        //METHOD FOR UPDATE THE SHARES
        private void aggiornaQuoteAsianBet()
        {
            try
            {
                mainService.aggiornaQuoteAsianBet(this);
            }
            catch
            {
                this.aggiornaQuoteAsianBet();
            }
        }


        //METHOD FOR UPDATE THE MATCHES
        private void aggiornaPartiteAsync()
        {
            try
            {
                mainService.aggiornaPartiteAsync(this);
            }
            catch
            {
                this.aggiornaPartiteAsync();
            }
        }

        //METHOD FOR MANAGE CLOSE THE WINDOW
        private void Window_Closed(object sender, EventArgs ea)
        {
            try
            {
                updateNowGoal.Abort();
                updateAsianBet.Abort();
            }
            catch (Exception e)
            {
                updateNowGoal.Abort();
                updateAsianBet.Abort();
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}