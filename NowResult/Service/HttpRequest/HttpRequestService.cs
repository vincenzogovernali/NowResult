using System;
using System.IO;
using System.Net;

namespace NowResult.Service.HttpRequest
{
    class HttpRequestService
    {
        public String call (string url)
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = Properties.Settings.Default.UserAgent;
            request.Timeout = Properties.Settings.Default.Timeout;
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            {
                html = reader.ReadToEnd();
            }
            return html;
        }
    }
}
