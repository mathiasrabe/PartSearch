using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text;
using System.Threading;

// viel übernommen von: http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetresponse.aspx

namespace PartSearch
{
    public class RequestState
    {
        // This class stores the State of the request.
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;
        public RequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = null;
            streamResponse = null;
        }
    }

    public class SearchEngine
    {
        protected string URI;
        protected string htmlText;

        public static ManualResetEvent allDone = new ManualResetEvent(false);
        const int BUFFER_SIZE = 1024;
        const int DefaultTimeout = 2 * 60 * 1000; // 2 minutes timeout

        // Abort the request if the timer fires.
        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public SearchEngine(string URI)
        {
            this.URI = URI;
        }

        public void GetWebText(string searchTerm)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(this.URI + searchTerm);

                // Create an instance of the RequestState and assign the previous myHttpWebRequest
                RequestState myRequestState = new RequestState();
                myRequestState.request = myRequest;


                // Start the asynchronous request.
                IAsyncResult result =
                  (IAsyncResult)myRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

                // this line implements the timeout, if there is a timeout, the callback fires and the request becomes aborted
                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), myRequest, DefaultTimeout, true); //FIXME

                // The response came in the allowed time. The work processing will happen in the 
                // callback function.
                allDone.WaitOne();

                // Release the HttpWebResponse resource.
                myRequestState.response.Close();
            }
            catch (Exception e)
            {
                //FIXME exceptions?!
                MessageBox.Show("Error: " + e);
            }
        }

        private static void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                // State of request is asynchronous.
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest myHttpWebRequest = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

                // Read the response into a Stream object.
                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                // Begin the Reading of the contents of the HTML page and print it to the console.
                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                return;
            }
            catch (WebException e)
            {
                MessageBox.Show("Error: " + e);
                //Console.WriteLine("\nRespCallback Exception raised!");
                //Console.WriteLine("\nMessage:{0}", e.Message);
                //Console.WriteLine("\nStatus:{0}", e.Status);
            }
            allDone.Set();
        }
        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            try
            {

                RequestState myRequestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                // Read the HTML page and then print it to the console.
                if (read > 0)
                {
                    myRequestState.requestData.Append(Encoding.UTF8.GetString(myRequestState.BufferRead, 0, read)); //stat UTF8 war vorher ASCII
                    IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                    return;
                }
                else
                {
                    //Console.WriteLine("\nThe contents of the Html page are : ");
                    if (myRequestState.requestData.Length > 1)
                    {
                        string stringContent;
                        stringContent = myRequestState.requestData.ToString();
                        //Console.WriteLine(stringContent);
                    }
                    //Console.WriteLine("Press any key to continue..........");
                    //Console.ReadLine();

                    responseStream.Close();
                }

            }
            catch (WebException e)
            {
                MessageBox.Show("Error: " + e);
                //Console.WriteLine("\nReadCallBack Exception raised!");
                //Console.WriteLine("\nMessage:{0}", e.Message);
                //Console.WriteLine("\nStatus:{0}", e.Status);
            }
            allDone.Set();

        }
    }
}
