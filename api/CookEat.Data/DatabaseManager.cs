using Microsoft.Extensions.FileProviders;

namespace CookEat.Data
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IFileProvider _fileProvider;

        public DatabaseManager(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
    }
}