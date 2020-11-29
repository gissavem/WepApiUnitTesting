using System.Text.RegularExpressions;

namespace RestAPI.Options
{
    public class LogFinderOptions
    {
        public const string SectionName = "LogFinderOptions";
        public string? AbsoluteBaseDirectoryPath { get; set; }
        public string FileNamePrefix { get; } = "log_";
        public string? FullPath => AbsoluteBaseDirectoryPath +"\\"+ FileNamePrefix;
        public Regex FileIdRegex = new Regex("(log_)(?'id'.+)");
    }
}