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
using System.ComponentModel;

namespace PartSearch.Filewrapper
{

    public class Favorit : INotifyPropertyChanged

    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        String _fileName;

        //Konstruktor
        public Favorit(string fileName)
        {
            this._fileName = fileName;
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
                }
            }
            return bookmarks;
        }

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

                onPropertyChanged(this, "list");
 	 	
            }
 	 	    catch (Exception ex)
 	        {
 	 	        //MessageBox.Show(ex.Message);
                throw ex;
 	 	    }
        }

        public void removeBookmark(String bookmark)
        {
            try
            {
                IsolatedStorageFile tempStore = IsolatedStorageFile.GetUserStoreForApplication();

                IsolatedStorageFileStream myTempStream = new IsolatedStorageFileStream("tempFile",
                    FileMode.Create,
                    tempStore);
                StreamWriter writer = new StreamWriter(myTempStream);

                //FIXME was passiert, wenn _fileName nicht existiert?
                IsolatedStorageFileStream myOldStream = new IsolatedStorageFileStream(this._fileName,
                    FileMode.Open,
                    isoStore);
                StreamReader reader = new StreamReader(myOldStream);

                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != bookmark)
                    {
                        writer.WriteLine(line);
                    }
                }

                writer.Close();
                reader.Close();

                isoStore.DeleteFile(this._fileName);
                isoStore.MoveFile( "tempFile" , this._fileName);

                onPropertyChanged(this, "list");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        public bool doesExist(string bookmark)
        {
            try
            {
                //FIXME was passiert, wenn _fileName nicht existiert?
                IsolatedStorageFileStream myOldStream = new IsolatedStorageFileStream(this._fileName,
                    FileMode.Open,
                    isoStore);
                StreamReader reader = new StreamReader(myOldStream);

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == bookmark)
                    {
                        return true;
                    }
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }        
}
