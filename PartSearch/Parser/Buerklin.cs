using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using PartSearch.Models;

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
            _myURI = "http://www.buerklin.de";
        }

        public override List<Product> GetParts()
        {
            //TODO: Mit Leben füllen!
            //siehe Klasse HtmlDocument
            //HTML Text sollte in _htmlText stehen
            List<Product> list = new List<Product>();
            return list;
        }

    }
}
