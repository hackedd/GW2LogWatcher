using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GW2LogWatcher
{
    public class Log
    {
        public LinkLabel Label { get; }
        public int MaxLines { get; }

        private readonly List<object[]> lines;

        public Log(LinkLabel label, int maxLines)
        {
            Label = label;
            MaxLines = maxLines;
            lines = new List<object[]>();
        }

        public void Add(params object[] values)
        {
            if (lines.Count >= MaxLines)
                lines.RemoveAt(0);

            lines.Add(values);
            Format();
        }

        private void Format()
        {
            if (Label.InvokeRequired)
            {
                Label.Invoke(new Action(Format));
                return;
            }

            var sb = new StringBuilder();
            var links = new List<LinkLabel.Link>();

            foreach (var values in lines)
            {
                foreach (var value in values)
                {
                    var link = value as LogLink;
                    if (link != null)
                    {
                        links.Add(new LinkLabel.Link(sb.Length, link.Label.Length, link.Address));
                        sb.Append(link.Label);
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }

                sb.AppendLine();
            }

            Label.Links.Clear();

            Label.Text = sb.ToString();

            foreach (var link in links)
                Label.Links.Add(link);
        }
    }
}
