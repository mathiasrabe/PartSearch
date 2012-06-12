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
using System.IO.IsolatedStorage;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PartSearch.Filewrapper
{

    public class Favorit
    {

        //Konstruktor
        public Favorit(string fileName)
        {
            this._fileName = fileName;
        }

        private  string _fileName
        {
            private set
            {
                _fileName = value;
            }

            get
            {
                return _fileName;
            }
        }

        // gebe alle Bookmarks wieder
        public List<String> getBookmarkList()
        {
            List<String> bookmarks = new List<string>();

            // überprüfe ob eine bookmarks.txt existiert
            string[] fileNames = isoStore.GetFileNames(this._fileName);
 	 	    foreach (string file in fileNames)
            {
                if (file == this._fileName)
                {
                    //MessageBox.Show("Bookmarks gefunden!");
 	 	
                    // hole bookmarks aus dem ISF
 	 	
                    try
                    {
                        IsolatedStorageFileStream myStream =
                            new IsolatedStorageFileStream(this._fileName,
                                FileMode.Open, isoStore);
                        StreamReader reader = new StreamReader(myStream);
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            bookmarks.Add(line);
                        reader.Close();
 	 	            }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                        throw e;
                    }
 	 	
                    return bookmarks;
 	 	
                }
 	 	
            }
        }
        // ende Konstruktor

        public void addBookmark(String bookmark)
        {
            try
            {
                IsolatedStorageFileStream myStream = new IsolatedStorageFileStream(this._fileName,
                    FileMode.Append,
                    isoStore);
                StreamWriter writer = new StreamWriter(myStream);
 	 	
                writer.WriteLine(bookmark);
 	 	
                writer.Close();
 	 	
            }
 	 	    catch (Exception ex)
 	        {
 	 	        //MessageBox.Show(ex.Message);
                throw ex;
 	 	    }
        }
    }        
}
