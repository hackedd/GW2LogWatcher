using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GW2LogWatcher
{
    public class RaidarEncounter
    {
        public string Account { get; set; }
        public int Archetype { get; set; }
        public string Area { get; set; }
        public string Character { get; set; }
        public float Duration { get; set; }
        public int Elite { get; set; }
        public int Id { get; set; }
        public int Profession { get; set; }
        public int StartedAt { get; set; }
        public bool Success { get; set; }
        public int UploadedAt { get; set; }
        public string UrlId { get; set; }

        public string Url => RaidarSession.BaseUrl + "encounter/" + UrlId;
    }

    public class RaidarNotification
    {
        public RaidarEncounter Encounter { get; set; }
        public int EncounterId { get; set; }
        public string EncounterUrlId { get; set; }
        public string Filename { get; set; }
        public string Type { get; set; }
        public int UploadId { get; set; }
        public string UploadedBy { get; set; }
        public string Error { get; set; }

        public string Url => RaidarSession.BaseUrl + "encounter/" + EncounterUrlId;
    }

    public class RaidarPollResponse
    {
        public int LastId { get; set; }
        public List<RaidarNotification> Notifications { get; set; }
    }

    public class RaidarSession
    {
        internal const string BaseUrl = "https://www.gw2raidar.com/";
        private static readonly Regex CsrfTokenRegex = new Regex("name='csrfmiddlewaretoken' value='([^']+)'");

        private readonly CookieContainer cookieContainer;
        private string csrfToken;

        private static RaidarSession _instance;
        public static RaidarSession Instance => _instance ?? (_instance = new RaidarSession());

        private static readonly JsonSerializerSettings SnakeCaseSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public RaidarSession()
        {
            cookieContainer = new CookieContainer();
            GetCsrfToken();
        }

        public bool IsLoggedIn { get; private set; }

        protected void GetCsrfToken()
        {
            var request = WebRequest.CreateHttp(BaseUrl);
            request.CookieContainer = cookieContainer;
            var streamReader = new StreamReader(request.GetResponseStream());
            var html = streamReader.ReadToEnd();

            csrfToken = CsrfTokenRegex.Search(html);
            Debug.Assert(csrfToken != null, "csrfToken != null");
        }

        public void Login(string username, string password)
        {
            var request = WebRequest.CreateHttp(BaseUrl + "login.json");
            var mp = new MultipartRequest();

            mp.AddField("username", username);
            mp.AddField("password", password);

            request.CookieContainer = cookieContainer;
            request.Method = "POST";
            request.ContentType = mp.ContentType;
            request.Referer = BaseUrl;
            request.Headers.Add("X-CSRFToken: " + csrfToken);

            var requestStream = request.GetRequestStream();
            mp.WriteRequest(requestStream);

            var streamReader = new StreamReader(request.GetResponseStream());
            var json = streamReader.ReadToEnd();

            dynamic responseData = JObject.Parse(json);

            if (responseData.error != null)
                throw new WebException("Unable to log in: " + responseData.error);

            csrfToken = responseData.csrftoken;
            Debug.Assert(csrfToken != null, "csrfToken != null");

            IsLoggedIn = true;
        }

        public int Upload(string filename)
        {
            Debug.Assert(IsLoggedIn, "IsLoggedIn");

            var request = WebRequest.CreateHttp(BaseUrl + "upload.json");
            var mp = new MultipartRequest();

            request.CookieContainer = cookieContainer;
            request.Method = "POST";
            request.ContentType = mp.ContentType;
            request.Referer = BaseUrl;
            request.Headers.Add("X-CSRFToken: " + csrfToken);

            using (var stream = Helpers.TryOpenRead(filename))
            {
                mp.AddFile(stream, filename, Path.GetFileName(filename));

                var requestStream = request.GetRequestStream();
                mp.WriteRequest(requestStream);
            }

            var streamReader = new StreamReader(request.GetResponseStream());
            var json = streamReader.ReadToEnd();

            dynamic responseData = JObject.Parse(json);

            if (responseData.error != null)
                throw new WebException("Unable to upload file: " + responseData.error);

            int? uploadId = responseData.upload_id;
            return uploadId.Value;
        }

        public RaidarPollResponse Poll()
        {
            Debug.Assert(IsLoggedIn, "IsLoggedIn");

            var request = WebRequest.CreateHttp(BaseUrl + "poll.json");
            request.CookieContainer = cookieContainer;
            request.Method = "POST";
            request.Referer = BaseUrl;
            request.Headers.Add("X-CSRFToken: " + csrfToken);

            var streamReader = new StreamReader(request.GetResponseStream());
            var json = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<RaidarPollResponse>(json, SnakeCaseSettings);
        }

        public RaidarNotification UploadAndWait(string filename)
        {
            return UploadAndWait(filename, TimeSpan.FromMinutes(10));
        }

        public RaidarNotification UploadAndWait(string filename, TimeSpan timeout)
        {
            var deadline = DateTime.Now + timeout;
            var uploadId = Upload(filename);

            while (DateTime.Now < deadline)
            {
                var response = Poll();

                var notification = response.Notifications.Find(n => n.UploadId == uploadId);
                if (notification != null)
                    return notification;

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }

            return null;
        }
    }
}