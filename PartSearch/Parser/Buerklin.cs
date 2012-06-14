//TODO::
            //Typecastin                                    (erledigt 08.06.2012 Michi und Benny)
            //-Übergabe an Modell                           (erledigt 08.06.2012 Michi und Benny)
            //-Eventuell Strings weiter Bearbeiten
            //Bedinung in foreach eintragen (bei.....)      (erledigt 08.06.2012 Benny)
            //SET Routinen fuer Product einarbeiten


using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using PartSearch.Models;
using HtmlAgilityPack;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml;

namespace PartSearch.Parser
{
    public class Buerklin : SearchEngine  //: heisst erbt von Search Engine
    {
        ObservableCollection<Product> Items;
        /**
        * Konstruktor
        **/
        public Buerklin()
        {
            //TODO: URI anpassen!
            _myURI = "http://www.buerklin.com/default.asp?event=ShowSE(";
            _backPartOfMyURI = ")";
            Items = new ObservableCollection<Product>();
        }

        public override ObservableCollection<Product> GetParts()
        {
            // Hilfsvariablen begindn mit h

            string name;
            string price;
        
            /*
            string hname;
            string hprice;*/
           
           // ObservableCollection<Gericht> tmpGerichte = new ObservableCollection<Gericht>();

           //Erstellung HTML Documentes und runtergeladenen HTML text da rein
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
           doc.LoadHtml(_htmlText);

           foreach (IEnumerable<HtmlNode> table in doc.DocumentNode.Descendants("//table[@class='Artikelliste']"))
           {
               //FIXME!!!
               foreach (HtmlNode line in table)
               {
                   MessageBox.Show(line.ToString());
               }
           }
           /*var hell = doc.DocumentNode.SelectNodes("//table[@class='hell']");

           // erstellen der Hilfsinstanz von Produk
           Product tmpProduct = new Product();

       
           // Auslesen Der Daten
           HtmlAgilityPack.HtmlDocument newDoc = new HtmlAgilityPack.HtmlDocument();
           newDoc.LoadHtml(hell.ToString());
           name = newDoc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
           price = newDoc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
       

           //Typecasting                               umwandlung von Strings in Zahlen
           //price = (double)hprice;
           

	       // Datenübergabe
	       this.Items.Add(new Product() { Name = name, Price = price });

           
   
           var dunkel = doc.DocumentNode.SelectNodes("//table[@class='dunkel']");
           

           // Auslesen Der Daten
           //HtmlDocument newDoc = new HtmlDocument();
           newDoc.LoadHtml(dunkel.ToString());
           name = newDoc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
           price = newDoc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
          
        
           //Typecasting                               umwandlung von Strings in Zahlen
           //price = (double)hprice;
                       

            // Datenübergabe
	        this.Items.Add(new Product() { Name = name, Price = price });*/

            NotifyPropertyChanged("SampleProperty");
            
            return this.Items;
        }

    }
}
