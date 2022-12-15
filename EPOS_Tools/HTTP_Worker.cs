using System.Net;
using System.Text;

namespace EPOS_Tools
{
    internal class HTTP_Worker
    {
        public static string Get(string uri) => Get(new Uri(uri), null);
        public static string Get(Uri uri) => Get(uri, null);
        public static string Get(string uri, CookieContainer cookieContainer) => Get(new Uri(uri), cookieContainer);

        public static string Get(Uri uri, CookieContainer cookieContainer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.CookieContainer = cookieContainer;
            return getResponce(request);
        }

        public static string Post(string uri) => Post(new Uri(uri), null, "");
        public static string Post(Uri uri) => Post(uri, null, "");

        public static string Post(string uri, CookieContainer cookieContainer)
            => Post(new Uri(uri), cookieContainer, "");

        public static string Post(Uri uri, CookieContainer cookieContainer)
            => Post(uri, cookieContainer, "");

        public static string Post(string uri, CookieContainer cookieContainer, string postData)
            => Post(new Uri(uri), cookieContainer, new ASCIIEncoding().GetBytes(postData));

        public static string Post(string uri, CookieContainer cookieContainer, byte[] postData)
            => Post(new Uri(uri), cookieContainer, postData);

        public static string Post(Uri uri, CookieContainer cookieContainer, string postData)
            => Post(uri, cookieContainer, new ASCIIEncoding().GetBytes(postData));

        public static string Post(Uri uri, CookieContainer cookieContainer, byte[] postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.CookieContainer = cookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(postData, 0, postData.Length);
            stream.Close();
            return getResponce(request);
        }

        private static string getResponce(HttpWebRequest request)
        {
            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();
            using (Stream stream = responce.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                return reader.ReadToEnd();
        }

    }
}
