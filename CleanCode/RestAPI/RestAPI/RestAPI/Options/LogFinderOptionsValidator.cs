using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace RestAPI.Options
{
    public class LogFinderOptionsValidator : IValidateOptions<LogFinderOptions>
    {
        public ValidateOptionsResult Validate(string name, LogFinderOptions? options)
        {
            var failures = new List<string>();

            if (string.IsNullOrWhiteSpace(options?.AbsoluteBaseDirectoryPath))
            {
                failures.Add($"Property '{nameof(options.AbsoluteBaseDirectoryPath)}' cannot be null");
            }
            return failures.Count > 0 ? ValidateOptionsResult.Fail(failures) : ValidateOptionsResult.Success;
        }
    }
}