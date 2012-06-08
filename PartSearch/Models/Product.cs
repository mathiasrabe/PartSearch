using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PartSearch.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        //private string _description;
        private double _price;
        private int _quantity;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
/*
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }
*/
        public double Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
                RaisePropertyChanged("Price");
            }
        }

        public int quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                RaisePropertyChanged("quantity");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
