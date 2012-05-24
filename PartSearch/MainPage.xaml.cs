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

            //auch wenn keine Eingabe erfolgte sollen die bookmarks angezeigt werden.
            this.searchBox.MinimumPrefixLength = 0;  //FIXME: Wird beim ersten mal nicht angezeigt!
        }
        
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchEngine search = new SearchEngine();
            search.GetWebText("");

        }

        private void bookmarkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resultListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}