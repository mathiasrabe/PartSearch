using System;
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
using PartSearch.Parser;

namespace PartSearch
{
    public partial class MainPage : PhoneApplicationPage
    {
        Buerklin _DistributorBuerklin;
        // Konstruktor
        public MainPage()
        {
            InitializeComponent();

            // Datenkontext des Listenfeldsteuerelements auf die Beispieldaten festlegen
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            // erzeuge Distributor Modul Buerklin
            _DistributorBuerklin = new Buerklin();

            //Erstelle Liste aller Distributor
            List<String> DistributorList = new List<String>();
            DistributorList.Add(_DistributorBuerklin.URL);

            //füttere den ListPicker mit Distributoren
            this.distributorListPicker.ItemsSource = DistributorList;
        }

        // Daten für die ViewModel-Elemente laden
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            _DistributorBuerklin.GetWebText(searchBox.Text);
            //search.GetParts();

        }

        private void bookmarkButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}