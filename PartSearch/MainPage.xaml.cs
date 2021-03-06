﻿using System;
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
using System.IO.IsolatedStorage;
using System.IO;
using PartSearch.Filewrapper;
using System.Reflection;

namespace PartSearch
{
    public partial class MainPage : PhoneApplicationPage
    {
        StringStorage bookmarks;

        // Konstruktor
        public MainPage()
        {
            InitializeComponent();

            //Bookmarkzeugs initialisieren
            bookmarks = new StringStorage("bookmarks.txt");
            searchBox.ItemsSource = bookmarks.getItemList();
            bookmarks.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.bookmarksChanged);

            // Datenkontext des Listenfeldsteuerelements auf die Beispieldaten festlegen
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //Erstelle Liste aller Distributor
            List<SearchEngine> DistributorList = App.ViewModel.GetDistributors();

            //füttere den ListPicker mit Distributoren
            this.distributorListPicker.ItemsSource = DistributorList;

	        //App.ViewModel.Engine = _DistributorBuerklin;
            App.ViewModel.DistributorBuerklin.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.ComponentsLoaded);
        }

        // Daten für die ViewModel-Elemente laden
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }*/
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            //_DistributorBuerklin.GetWebText(searchBox.Text);
            //search.GetParts();
            this.search();
        }

        private void search()
        {
            if (searchBox.Text.Length == 0)
            {
                MessageBox.Show("Bitte geben Sie einen Suchbegriff ein.");
                return;
            }
            App.ViewModel.SearchData(searchBox.Text);
            PivotControle.SelectedIndex = 1;
            SearchScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void HideSearchScreen()
        {
            SearchScreen.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void bookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            if (bookmarks.doesExist(searchBox.Text))
            {
                try
                {
                    bookmarks.removeItem(searchBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    bookmarks.addItem(searchBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bookmarksChanged(object sender, EventArgs ea)
        {
            if (sender == bookmarks)
            {
                //if (ea.ToString == "list")
                //{
                    searchBox.ItemsSource = bookmarks.getItemList();
                //}
            }
        }

        private void ComponentsLoaded(object sender, EventArgs e)
        {
            //Was soll hier geschehen, wenn die Suchergebnisse da sind?
            this.HideSearchScreen();
            SecondListBox.ItemsSource = App.ViewModel.Items;
        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bookmarks.doesExist(searchBox.Text))
            {
                //ändere das style des buttons
                //bookmarkButton.Style = (Style)this.Ressources
            }
        }

        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Focus();
                this.search();
            }
        }

        private void LoadingCancelButton_Click(object sender, RoutedEventArgs e)
        {
            //cancel the search
            PivotControle.SelectedIndex = 0;
            this.HideSearchScreen();
            App.ViewModel.DistributorBuerklin.CancelSearch();
        }
    }
}