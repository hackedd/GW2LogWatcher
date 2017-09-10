namespace GW2LogWatcher
{
    public class RaidarHandler : LogFileHandler
    {
        public override LogFileHandlerResults Handle(string filename)
        {
            var session = RaidarSession.Instance;

            if (!session.IsLoggedIn)
                return new LogFileHandlerResults {Success = false, Output = "Not logged in"};

            var notification = session.UploadAndWait(filename);
            if (notification == null)
                return new LogFileHandlerResults {Success = false, Output = "Timeout"};

            if (notification.Type != "upload")
                return new LogFileHandlerResults {Success = false, Output = notification.Error};

            return new LogFileHandlerResults {Success = true, Output = notification.Url};
        }
    }
}
