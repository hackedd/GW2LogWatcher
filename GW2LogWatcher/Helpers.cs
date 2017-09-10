using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace GW2LogWatcher
{
    static class Helpers
    {
        public static string Search(this Regex regex, string subject)
        {
            var match = regex.Match(subject);
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }

        public static Stream GetResponseStream(this HttpWebRequest request)
        {
            try
            {
                var response = request.GetResponse();
                return response.GetResponseStream();
            }
            catch (WebException e)
            {
                Debug.WriteLine("WebException for {0}: {1}", request.RequestUri, e);
                Debug.WriteLine("Status: {0}", e.Status);

                foreach (var header in e.Response.Headers.AllKeys)
                    Debug.WriteLine("{0}: {1}", header, e.Response.Headers[header]);

                using (var sr = new StreamReader(e.Response.GetResponseStream()))
                    Debug.WriteLine(sr.ReadToEnd());

                throw;
            }
        }

        public static Stream TryOpenRead(string filename, int maxAttempts = 10)
        {
            for (var attempt = 0; attempt < maxAttempts; attempt += 1)
            {
                try
                {
                    return File.OpenRead(filename);
                }
                catch (IOException e)
                {
                    Debug.WriteLine("Unable to open {0} (attempt {1}/{2}): {3}", filename, attempt, maxAttempts, e);
                    Thread.Sleep(500);
                }
            }

            return null;
        }
    }
}
