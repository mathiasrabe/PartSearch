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

namespace PartSearch.Parser
{
    public class Buerklin : SearchEngine  //: heisst erbt von Search Engine
    {
        /**
* Konstruktor
**/
        public Buerklin()
        {
            //TODO: URI anpassen!
            _myURI = "http://www.buerklin.com/default.asp?event=ShowSE(";
            _backPartOfMyURI = ")";
        }

        public override List<Product> GetParts()
        {
            // Hilfsvariablen begindn mit h

            private string _name;
            private double _price;
        

            private string hname;
            private string hprice;
           
           // ObservableCollection<Gericht> tmpGerichte = new ObservableCollection<Gericht>();

           HtmlDocument doc = new HtmlDocument();   //Erstellung HTML Documentes und runtergeladenen HTML text da rein
           doc.LoadHtml(HTML);
              
           var hell = doc.DocumentNode.SelectNodes("//table[@class='hell']");

           // erstellen der Hilfsinstanz von Produk
           Product tmpProduct = new Product();

           foreach (HTMLNode hell in hell) 
            {

            // Auslesen Der Daten
            var name = doc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
            var hprice = doc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
       

            //Typecasting                               umwandlung von Strings in Zahlen
            price = (double)hprice;
           

            //Daten in Hilfsmodell schreiben
                    tmpProduct.name = name;
                    tmpProduct.price = price;
                

            //Hinzufuegen der Daten des Hilfmodelles in das richtige
                    Product.Add(tmpProduct);

            }
   
           var dunkel = doc.DocumentNode.SelectNodes("//table[@class='dunkel']");
           foreach (HTMLNode dunkel in dunkel) 
            {

            // Auslesen Der Daten
            var name = doc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
            var hprice = doc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
        
            //Typecasting                               umwandlung von Strings in Zahlen
            price = (double)hprice;
                       

            //Daten in Hilfsmodell schreiben
                    tmpProduct.name = name;
                    tmpProduct.price = price;


            //Hinzufuegen der Daten des Hilfmodelles in das richtige
                    ProductList.Add(tmpProduct);

            }
            //return list;
        }

    }
}
