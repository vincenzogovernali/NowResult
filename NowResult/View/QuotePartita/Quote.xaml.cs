using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NowResult
{
    /// <summary>
    /// Logica di interazione per Dettagli.xaml
    /// </summary>
    public partial class Quote : Window
    {
        private QuoteService quoteService = new QuoteService();
        private ComboBoxItem qc;
        private ComboBoxItem sc;
        private Thread thread;

        public Quote(String url, String risultatoMatch, String idDettaglio)
        {
            InitializeComponent();
            quoteService.parsingQuote(url, risultatoMatch, idDettaglio, this);
        }


        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            gifLoader.Position = TimeSpan.FromMilliseconds(1);
        }


        public void calcola(object sender, RoutedEventArgs e)
        {
            qc = (ComboBoxItem)quotaCalcolo.SelectedItem;
            sc = (ComboBoxItem)storicoCalcolo.SelectedItem;
            lista.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Hidden;
            away.Visibility = Visibility.Hidden;
            risultato.Visibility = Visibility.Hidden;
            quotaCalcolo.Visibility = Visibility.Hidden;
            storicoCalcolo.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            Calcola.Visibility = Visibility.Hidden;
            gifLoader.Visibility = Visibility.Visible;
            labelGif.Visibility = Visibility.Visible;
            gifLoader.Play();
            Application.Current.Dispatcher.Invoke(new Action(() => { UpdateLayout(); }), DispatcherPriority.ContextIdle);
            thread = new Thread(calcolaQuote);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void calcolaQuote()
        {
            quoteService.threadCalcolo(qc, sc, this);
        }
    }
}
