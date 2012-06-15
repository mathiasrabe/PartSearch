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
//compile with: reference 

namespace PartSearch.Parser
{
    public class Buerklin : SearchEngine  //: heisst erbt von Search Engine
    {
        //ObservableCollection<Product> Items;
        /**
        * Konstruktor
        **/
        public Buerklin()
        {
            //TODO: URI anpassen!
            _myURI = "http://www.buerklin.com/default.asp?event=ShowSE(";
            _backPartOfMyURI = ")";
            //Items = new ObservableCollection<Product>();
        }

        public override void FindItemsInHtml()
        {
            // Hilfsvariablen begindn mit h

            string name;
            string price;
        
            /*
            string hname;
            string hprice;*/
           
           // ObservableCollection<Gericht> tmpGerichte = new ObservableCollection<Gericht>();

           //Erstellung HTML Documentes und runtergeladenen HTML text da rein
            HtmlDocument doc = new HtmlDocument();
           doc.LoadHtml(_htmlText);

           if (doc.ParseErrors != null)
           {
               foreach (HtmlParseError error in doc.ParseErrors)
               {
                   MessageBox.Show("Parse Error in: " + error.SourceText);
               }
           }

           foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table[@class = 'Artikelliste']/tbody/tr"))
           {
               //MessageBox.Show(row.SelectSingleNode("//td[@class = 'Typ']/a").InnerText);

               name = row.SelectSingleNode("//td[@class = 'Typ']/a").InnerText;
               //der Brutto-Preis für ein Stück:
               price = row.SelectSingleNode("//td[@class = 'Brutto']/table/tr/td[@class = 'Preis']").InnerText;
               price.Remove(0, 7); //löscht die ersten 7 Stellen da darin steht: "&euro; "

               this.Items.Add(new Product() { Name = name, Price = price });
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
            
            //return this.Items;
        }

    }
}
