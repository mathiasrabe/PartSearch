using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PartSearch
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        // Konstruktor
        public MainPage()
        {
            InitializeComponent();

            List<string> bookmarks = new List<string>();

            // überprüfe ob eine bookmarks.txt existiert
            string[] fileNames = isoStore.GetFileNames("bookmarks.txt");
            foreach (string file in fileNames)
            {
                if (file == "bookmarks.txt")
                {
                    //MessageBox.Show("Bookmarks gefunden!");
                    // hole bookmarks aus dem ISF
                    try
                    {
                        IsolatedStorageFileStream myStream =
                            new IsolatedStorageFileStream("bookmarks.txt",
                                FileMode.Open, isoStore);
                        StreamReader reader = new StreamReader(myStream);
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            bookmarks.Add(line);
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    //MessageBox.Show(bookmarks.First().ToString());
                    this.searchBox.ItemsSource = bookmarks;
                }
            }
            //auch wenn keine Eingabe erfolgte sollen die bookmarks angezeigt werden.
            this.searchBox.MinimumPrefixLength = 0;  //FIXME: Wird beim ersten mal nicht angezeigt!
        }
        
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            //FIXME: er überschreibt immer die erste Zeile
            //FIXME: Wenn Bookmark gespeichert wurde, sollte die bookmarkliste aktualisiert werden
            // speicher den Inhalt der SearchBox als Bookmark
            try
            {
                IsolatedStorageFileStream myStream =
                    new IsolatedStorageFileStream("bookmarks.txt",
                        FileMode.Create, isoStore);
                StreamWriter writer = new StreamWriter(myStream);
                writer.WriteLine(searchBox.Text.ToString());
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void resultListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}