using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace MovieGeek.a1.Helpers
{
    internal class IsolatedStorageHelper
    {

        private const String PathFormat = "HtmlDumps\\{0}.htm";

        internal static async Task WriteAsync(String path, String content)
        {
            var local = IsolatedStorageFile.GetUserStoreForApplication();
            var filePath = String.Format(PathFormat, path);
            if (local.FileExists(filePath))
            {
                using (var isoStoreFile = new IsolatedStorageFileStream(filePath, FileMode.Truncate, FileAccess.Write, local))
                {
                    using (var isoStreamWriter = new StreamWriter(isoStoreFile))
                    {
                        await isoStreamWriter.WriteAsync(content);
                    }
                }
            }
            else
            {
                using (var isoStoreFile = new IsolatedStorageFileStream(filePath, FileMode.Create, FileAccess.Write, local))
                {
                    using (var isoStreamWriter = new StreamWriter(isoStoreFile))
                    {
                        await isoStreamWriter.WriteAsync(content);
                    }
                }
            }
        }

        internal static async Task<String> ReadAsync(String path)
        {
            var local = IsolatedStorageFile.GetUserStoreForApplication();
            var filePath = String.Format(PathFormat, path);
            using (var isoStoreFile = new IsolatedStorageFileStream(filePath, FileMode.Open, FileAccess.Read, local))
            {
                using (var streamReader = new StreamReader(isoStoreFile))
                {
                    var content = await streamReader.ReadToEndAsync();
                    return content;
                }
            }
        }

        internal static Boolean FileExists(String path)
        {
            var local = IsolatedStorageFile.GetUserStoreForApplication();
            var filePath = String.Format(PathFormat, path);
            var s = local.FileExists(filePath);
            return s;
        }
    }

    internal class IsoDir
    {
        internal const String OpeningThisWeek = "opening this week";
        internal const String InTheaters = "in theaters";
    }
}