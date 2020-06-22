using SiteDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteDownloader
{
    public class SiteContentDownloader : ISiteContentDownloader
    {
        private readonly int _maxDeepLevel;
        private const string htmlDocumentMediaType = "text/html";
        private readonly IContentSaver contentSaver;

        public SiteContentDownloader(IContentSaver contentSaver, int maxDeepLevel = 0)
        {
            if(maxDeepLevel < 0)
            {
                new ArgumentException("Bad argument. Deep kevel can not be less zero");
            }
            _maxDeepLevel = maxDeepLevel;
        }

        public void LoadFromURL(string URL)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URL);
                GetPageByURL(httpClient, httpClient.BaseAddress, 0);
            }
        }

        private void GetPageByURL(HttpClient httpClient, Uri uri, int level)
        {
            if (IsMaxDeepLevel(level))
            {
                return;
            }
            var header = GetHeaderWithSuccessStatusCode(httpClient, uri);
            if (header == null)
            {
                return;
            }
            if(header.Content.Headers.ContentType.MediaType == htmlDocumentMediaType)
            {
                Console.WriteLine("DownLoadPage");
                GetPageByURL(httpClient, uri, level + 1);
            }
            else
            {
                Console.WriteLine("DownLoadFile");
            }
        }

        private bool IsMaxDeepLevel(int level)
        {
            return level > _maxDeepLevel ? true : false;
        }

        private HttpResponseMessage GetHeaderWithSuccessStatusCode(HttpClient httpClient, Uri uri)
        {
            HttpResponseMessage head = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri)).Result;
            if (!head.IsSuccessStatusCode)
            {
                return null;
            }
            return head;
        }

        private void SaveContentFile(HttpClient httpClient, Uri uri)
        {
            var response = httpClient.GetAsync(uri).Result;           
            contentSaver.SaveFile(uri, response.Content.ReadAsStreamAsync().Result);
        }




    }
}
