using System;
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
using PartSearch.Parser;


namespace PartSearch
{
    public class MainViewModel : INotifyPropertyChanged
    {
        protected List<SearchEngine> DistributorList;
        public Buerklin DistributorBuerklin;
        public ObservableCollection<Product> Items { private set; get; }

        public MainViewModel()
        {
            // erzeuge Distributor Modul Buerklin
            DistributorBuerklin = new Buerklin();
            DistributorList = new List<SearchEngine>();
            DistributorList.Add(DistributorBuerklin);

            this.Items = new ObservableCollection<Product>();
            Items.Add(new Product() {Name = "Keine Ergebnisse"});

            //Engine = DistributorList[0];
            DistributorBuerklin.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.GetItems);
        }
        /*
        public SearchEngine Engine
        {
            get
            {
                return Engine;
            }
            set
            {
                Engine = value;
                Engine.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.GetItems);
            }
        }*/

        public List<SearchEngine> GetDistributors()
        {
            return DistributorList;
        }

        public void GetItems(object sender, EventArgs e)
        {
            if (sender == DistributorBuerklin)
            {
                this.Items = ((SearchEngine)sender).Items;
            }
        }

        /// <summary>
        /// Eine Auflistung für ItemViewModel-Objekte.
        /// </summary>
        //public ObservableCollection<Product> Items { get; private set; }

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

	    public void LoadData()
        {
            //FIXME

            this.IsDataLoaded = true;
        }

        public void SearchData(string searchString)
        {
            DistributorBuerklin.GetWebText(searchString);
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