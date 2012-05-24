using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
//using System.Text;
//using System.Threading;

// viel übernommen von: http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetresponse.aspx

namespace PartSearch
{
    public class SearchEngine
    {
        protected string myURI;
        protected string htmlText;

        public SearchEngine()
        {
            myURI = "http://www.google.de/";
        }

        public void GetWebText(string searchTerm)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(myURI));
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Make sure the process completed successfully
            if (e.Error == null)
            {
                // Use the result
                MessageBox.Show(e.Result);
            }
            else
            {
                MessageBox.Show("Error: " + e.Result);
            }
        }
    }
}
