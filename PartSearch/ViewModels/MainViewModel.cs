﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using PartSearch.Models;


namespace PartSearch
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<Product>();
        }

	public SearchEngin Engin{get; set;}

        /// <summary>
        /// Eine Auflistung für ItemViewModel-Objekte.
        /// </summary>
        public ObservableCollection<Product> Items { get; private set; }

        private string _sampleProperty = "Beispielwert für die Laufzeiteigenschaft";
        /// <summary>
        /// ViewModel-Beispieleigenschaft. Diese Eigenschaft wird in der Ansicht verwendet, um den Wert unter Verwendung einer Bindung anzuzeigen
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Erstellt und fügt einige ItemViewModel-Objekte zur Items-Auflistung hinzu.
        /// </summary>

	public void LoadData(string name, string preis)
        {
            
           
	    
    

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}