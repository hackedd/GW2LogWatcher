using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GW2LogWatcher
{
    struct MultipartFile
    {
        public string FileName { get; set; }
        public string FieldName { get; set; }
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
    }

    struct MultipartField
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }

    class MultipartRequest
    {
        private readonly List<MultipartFile> files;
        private readonly List<MultipartField> fields;

        public MultipartRequest()
        {
            Boundary = "------FormBoundary" + DateTime.Now.Ticks.ToString("x");
            files = new List<MultipartFile>();
            fields = new List<MultipartField>();
        }

        public string Boundary { get; set; }

        public string ContentType => "multipart/form-data; boundary=" + Boundary;

        public void AddFile(string fileName, string fieldName, string contentType = "application/octet-stream")
        {
            files.Add(new MultipartFile
            {
                FileName = fileName,
                FieldName = fieldName,
                ContentType = contentType
            });
        }

        public void AddFile(Stream stream, string fileName, string fieldName,
            string contentType = "application/octet-stream")
        {
            files.Add(new MultipartFile
            {
                FileName = fileName,
                FieldName = fieldName,
                ContentType = contentType,
                Stream = stream
            });
        }

        public void AddField(string fieldName, string value)
        {
            fields.Add(new MultipartField
            {
                FieldName = fieldName,
                Value = value
            });
        }

        public void WriteRequest(Stream requestStream)
        {
            var crlf = new byte[] {0x0D, 0x0A};
            
            foreach (var file in files)
            {
                var sb = new StringBuilder();
                sb.Append("--");
                sb.Append(Boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append(file.FieldName);
                sb.Append("\"; filename=\"");
                sb.Append(Path.GetFileName(file.FileName));
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append(file.ContentType);
                sb.Append("\r\n");
                sb.Append("\r\n");

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                requestStream.Write(bytes, 0, bytes.Length);

                if (file.Stream != null)
                {
                    file.Stream.CopyTo(requestStream);
                }
                else
                {
                    using (var stream = File.OpenRead(file.FileName))
                        stream.CopyTo(requestStream);
                }

                requestStream.Write(crlf, 0, 2);
            }

            foreach (var field in fields)
            {
                var sb = new StringBuilder();
                sb.Append("--");
                sb.Append(Boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append(field.FieldName);
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: application/octet-stream\r\n");
                sb.Append("\r\n");
                sb.Append(field.Value);
                sb.Append("\r\n");

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                requestStream.Write(bytes, 0, bytes.Length);
            }

            var closingBytes = Encoding.ASCII.GetBytes("--" + Boundary + "--\r\n");
            requestStream.Write(closingBytes, 0, closingBytes.Length);
        }

    }
}
