using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace GW2LogWatcher
{
    public class DpsReportHandler : LogFileHandler
    {
        private const string UploadUrl = "https://dps.report/uploadContent.php";
        private const string FileField = "file";

        private static readonly Regex LinkRegex = new Regex("<a href=\"([^\"]+)\"");

        public override LogFileHandlerResults Handle(string filename)
        {
            var request = WebRequest.CreateHttp(UploadUrl);
            var mp = new MultipartRequest();

            request.Method = "POST";
            request.ContentType = mp.ContentType;

            using (var stream = Helpers.TryOpenRead(filename))
            {
                mp.AddFile(stream, filename, FileField);

                var requestStream = request.GetRequestStream();
                mp.WriteRequest(requestStream);
            }

            var streamReader = new StreamReader(request.GetResponseStream());
            var html = streamReader.ReadToEnd();
            var link = LinkRegex.Search(html);

            return new LogFileHandlerResults
            {
                Success = link != null,
                Output = link ?? html
            };
        }
    }
}
