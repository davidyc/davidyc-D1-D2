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

      
        public ContentSaver(string startPath)
        {           
            _rootFoderPath = startPath;
            CreateRootFolder();
        }
        public void SaveFile(Uri uri, Stream fileStream)
        {
            // fix it            
            string filePath = CreateFolderPath(uri);
            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);
            if (Directory.Exists(filePath))
            {
                filePath = Path.Combine(filePath, Guid.NewGuid().ToString());
            }

            var createdFileStream = File.Create(filePath);
            fileStream.CopyTo(createdFileStream);
            createdFileStream.Close();
            fileStream.Close();

        }

        public void SaveHtmlDocument(Uri uri, string name, Stream documentStream)
        {
            string saveFolderPath = CreateFolderPath(uri);
            Directory.CreateDirectory(saveFolderPath);       
            string filePath = Path.Combine(saveFolderPath, name);

            var createdFileStream = File.Create(filePath);
            documentStream.CopyTo(createdFileStream);
            createdFileStream.Close();
            documentStream.Close();
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
    }

}
