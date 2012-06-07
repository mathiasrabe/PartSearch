using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using PartSearch.Models;
using HtmlAgilityPack;
using System.ComponentModel;

namespace PartSearch.Parser
{
    public class Buerklin : SearchEngine
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
             List<Product> list = new List<Product>();
            // Hilfsvariablen begindn mit h

            private string _name;
            private double _price;
            private int _quantity;

            private string hname;
            private string hprice;
            private string hquantity

            ObservableCollection<Gericht> tmpGerichte = new ObservableCollection<Gericht>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML);
            // Auslesen Der Daten
            var hname = doc.DocumentNode.SelectSingleNode("//td[@class='Typ']").InnerText;
            var hprice = doc.DocumentNode.SelectSingleNode("//td[@class='Brutto']").InnerText;
            var hquantity = doc.DocumentNode.SelectSingleNode("//td[@class='/*Hir Suchbegriff Eintragen*/']").InnerText;

            //TODO::
            //-Typcasting
            //-Übergabe an Modell
            //-Eventuell Strings weiter Bearbeiten

            /* Vorlage München
            var gerichtNummer = doc.DocumentNode.SelectSingleNode("//td[@class='gericht']").InnerText;
            var gericht1 = doc.DocumentNode.SelectSingleNode("//td[@class='beschreibung']/span").InnerText;

            var dates = doc.DocumentNode.SelectNodes("//table[@class='menu']/tr/td/span/a");
            var date1 = dates[0].InnerText;

            var menus = doc.DocumentNode.SelectNodes("//table[@class='menu']");

            foreach (HtmlNode menu in menus)
            {
                HtmlDocument newDoc = new HtmlDocument();
                newDoc.LoadHtml(menu.InnerHtml);
                var names = newDoc.DocumentNode.SelectNodes("//td[@class='beschreibung']/span[@style='float:left']");

                var date = newDoc.DocumentNode.SelectSingleNode("//tr/td/span/a").InnerText;

                foreach (HtmlNode name in names)
                {