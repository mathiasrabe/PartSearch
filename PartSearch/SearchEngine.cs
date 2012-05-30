using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows;
/*
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;*/
//using System.Text;
//using System.Threading;

// nicht viel übernommen von: http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetresponse.aspx

namespace PartSearch
{
    public class SearchEngine
    {
        protected string myURI;
        protected string htmlText;

        /**
         * Konstruktor
         * 
         * Ableitende Klassen müssen in ihren Konstruktoren ihre URI initialisieren!
         **/
        public SearchEngine()
        {
            //TODO: dies sollte in den abgeleiteten Klassen deffiniert werden
            myURI = "http://www.google.de/#hl=de&q=";
        }

        /**
         * Sucht nach searchTerm unter der initialisierten URI
         **/
        public void GetWebText(string searchTerm)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(myURI + searchTerm));
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message); //FIXME
            }
        }

        /**
         * Wird aufgerufen, wenn die aufgerufte Homepage antwortet
         **/
        protected void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Make sure the process completed successfully
            if (e.Error == null)
            {
                // Use the result
                htmlText = e.Result;
                MessageBox.Show(e.Result); //FIXME
            }
            else
            {
                //FIXME
                MessageBox.Show("Error: " + e.Result);
            }
        }

        /**
         * Durchsucht den Webtext nach Bauteilen.
         * Wird von den abgeleitten Klassen mit override überschrieben.
         * 
         * Rückgabewert: null liste bei Fehler
         **/
        public virtual List<List<string>> GetParts()
        {
            List<List<string>> list = new List<List<string>>();
            return list;
        }
    }
}
