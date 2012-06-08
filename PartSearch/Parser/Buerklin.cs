
//TODO::
            //Typecastin                                    (erledigt 08.06.2012 Michi und Benny)
            //-Übergabe an Modell                           (erledigt 08.06.2012 Michi und Benny)
            //-Eventuell Strings weiter Bearbeiten
            //Bedinung in foreach eintragen (bei.....)
            //Bedingung bei Suchbegriff fuer Anzahl eintragen
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
            private int _quantity;

            private string hname;
            private string hprice;
            private string hquantity

           // ObservableCollection<Gericht> tmpGerichte = new ObservableCollection<Gericht>();

            HtmlDocument doc = new HtmlDocument();   //Erstellung HTML Documentes und runtergeladenen HTML text da rein
            doc.LoadHtml(HTML);
            foreach (HTMLNode .......) 
   

            {

            // Auslesen Der Daten
            var name = doc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
            var hprice = doc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
            var hquantity = doc.DocumentNode.SelectSingleNode("//td[@class='/*Hir Suchbegriff Eintragen*/']").InnerText;

            //Typecasting                               umwandlung von Strings in Zahlen
            price = (double)hprice;
            quantity = (int)hquantity;
            

            //Daten in Hilfsmodell schreiben
            Product tmpProduct = new Product();
                    tmpProduct.name = name;
                    tmpProduct.price = price;
                    tmpProduct.quantity = quantity;

            //Hinzufuegen der Daten des Hilfmodelles in das richtige
                    Product.Add(tmpProduct);

            }
   
           
            //return list;
        }

    }
}
