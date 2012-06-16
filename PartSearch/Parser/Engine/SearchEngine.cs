﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using PartSearch.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PartSearch
{
    public class SearchEngine : INotifyPropertyChanged
    {
        protected string _myURI;
        protected string _htmlText;
        protected string _backPartOfMyURI = ""; //falls der searchTerm inmitten der URI ist, kann hier der Teil hinter dem searchTerm gespeichert werden
        //protected ObservableCollection<Product> list; //hier stehen alle Produkte drin

        /**
         * Konstruktor
         * 
         * Ableitende Klassen müssen in ihren Konstruktoren ihre URI initialisieren!
         **/
        public SearchEngine()
        {
            Items = new ObservableCollection<Product>();
        }

        /**
         * Gibt Host URL der Domain zurück
         **/
        public string URL
        {
            get
            {
                Uri URI = new Uri(_myURI);
                return URI.Host;
            }
        }

        /**
         * behandelt _backPartOfMyURI
         **/
        public string backPartOfMyURI
        {
            get
            {
                return _backPartOfMyURI;
            }
            set
            {
                _backPartOfMyURI = value;
            }
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
                client.DownloadStringAsync(new Uri(_myURI + searchTerm + _backPartOfMyURI));
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                _htmlText = e.Result;
                //MessageBox.Show(e.Result); //FIXME
                this.FindItemsInHtml();
            }
            else
            {
                MessageBox.Show("Error: " + e.Error);
            }
        }

        /**
         * Durchsucht den Webtext nach Bauteilen.
         * Wird von den abgeleitten Klassen mit override überschrieben.
         * 
         * Rückgabewert: null liste bei Fehler
         **/
        public virtual void FindItemsInHtml()
        {
            Items = new ObservableCollection<Product>();
            NotifyPropertyChanged("NewParts");
        }

        public ObservableCollection<Product> Items;
        /*{
            protected set
            {
                Items = value;
            }
            get
            {
                return Items;
            }
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
