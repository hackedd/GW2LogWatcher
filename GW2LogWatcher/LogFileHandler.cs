namespace GW2LogWatcher
{
    public class LogFileHandlerResults
    {
        public bool Success { get; set; }
        public string Output { get; set; }
    }

    public abstract class LogFileHandler
    {
        public abstract LogFileHandlerResults Handle(string filename);
    }
}
