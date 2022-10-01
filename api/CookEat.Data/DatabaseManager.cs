using Microsoft.Extensions.FileProviders;
using System.Text.RegularExpressions;

namespace CookEat.Data
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IFileProvider _fileProvider;

        private Regex _sqlFileRegex = new(@"^\d{4}(_[a-zA-Z]+)+\.sql$", RegexOptions.Compiled);

        public DatabaseManager(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }

        public int ExpectedSchemaVersion => GetScripts().Last().Version;

        private IEnumerable<(int Version, IFileInfo FileInfo)> GetScripts()
        {
            static int ParseVersion(string name) => int.Parse(name.Substring(0, name.IndexOf("_")));

            return _fileProvider.GetDirectoryContents("")
                .Where(file => _sqlFileRegex.IsMatch(file.Name))
                .Select(file => (Version: ParseVersion(file.Name), FileInfo: file))
                .OrderBy(x => x.Version);
        }
    }
}