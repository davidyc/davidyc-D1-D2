using SiteDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDownLoaderClient
{  
    public class ContentSaver : IContentSaver
    {
        private readonly string _rootFoderPath;
        private const int maxLenFileName = 200;

      
        public ContentSaver(string startPath)
        {           
            _rootFoderPath = startPath;
            CreateRootFolder();
        }
        public void SaveFile(Uri uri, Stream fileStream)
        {         
            string filePath = CreateFolderPath(uri);
            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);

            if (Directory.Exists(filePath))
            {
                filePath = Path.Combine(filePath, Guid.NewGuid().ToString());
            }

            SaveToFile(fileStream, filePath);
            fileStream.Close();
        }

        public void SaveHtmlDocument(Uri uri, string name, Stream documentStream)
        {
            string saveFolderPath = CreateFolderPath(uri);
            Directory.CreateDirectory(saveFolderPath);
            name = RemoveInvalidSymbols(name);
            string filePath = Path.Combine(saveFolderPath, name);
            if (filePath.Length > maxLenFileName)
            {
                filePath = filePath.Substring(0, maxLenFileName) + ".htlm";
            }

            SaveToFile(documentStream, filePath);
            documentStream.Close();
        }

        private void SaveToFile(Stream stream, string fileFullPath)
        {
            var createdFileStream = File.Create(fileFullPath);
            stream.CopyTo(createdFileStream);
            createdFileStream.Close();
        }

        private string CreateFolderPath(Uri uri)
        {
            return Path.Combine(_rootFoderPath, uri.Host) + uri.LocalPath.Replace("/", @"\");
        }

        private void CreateRootFolder()
        {
            if (!Directory.Exists(_rootFoderPath))
                Directory.CreateDirectory(_rootFoderPath);
        }

        private string RemoveInvalidSymbols(string filename)
        {
            var invalidSymbols = Path.GetInvalidFileNameChars();
            return new string(filename.Where(c => !invalidSymbols.Contains(c)).ToArray());
        }
    }

}
