using System.IO;

namespace PixelloTools.Logging
{
    public interface Ilogger
    {
        TextWriter GetLoggerTextWriter();
        void LogIt(object log);
    }
}
