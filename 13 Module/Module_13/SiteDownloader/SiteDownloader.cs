using HtmlAgilityPack;
using SiteDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
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
        private readonly IContentSaver _contentSaver;
        private readonly ISet<Uri> _alreadyVisited = new HashSet<Uri>();


        public SiteContentDownloader(IContentSaver contentSaver, int maxDeepLevel = 0)
        {
            if (maxDeepLevel < 0)
            {
                new ArgumentException("Bad argument. Deep kevel can not be less zero");
            }
            _maxDeepLevel = maxDeepLevel;
            _contentSaver = contentSaver;
        }

        public void LoadFromURL(string URL)
        {
            _alreadyVisited.Clear();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URL);
                GetPageByURL(httpClient, httpClient.BaseAddress, 0);
            }
        }

        private void GetPageByURL(HttpClient httpClient, Uri uri, int level)
        {
           // Console.WriteLine($"Level {level}");
            if (IsMaxDeepLevel(level) || _alreadyVisited.Contains(uri))
            {
                return;
            }
            _alreadyVisited.Add(uri);
            var header = GetHeaderWithSuccessStatusCode(httpClient, uri);
            if (header == null)
            {
                return;
            }
            if(header.Content.Headers.ContentType != null && header.Content.Headers.ContentType.MediaType == htmlDocumentMediaType)
            {
                SaveHtmlPage(httpClient, uri, level + 1);
            }
            else
            {
                SaveContentFile(httpClient, uri);
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
            var x = response.Content.ReadAsStreamAsync().Result;
            _contentSaver.SaveFile(uri, response.Content.ReadAsStreamAsync().Result);
        }

        private void SaveHtmlPage(HttpClient httpClient, Uri uri, int level)
        {        
            
            var response = httpClient.GetAsync(uri).Result;
            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);        


           _contentSaver.SaveHtmlDocument(uri, CreateFileName(document), CreateStreamHTMLPage(document));

            var attributesWithLinks = document.DocumentNode.Descendants().SelectMany(d => d.Attributes.Where(IsAttributeWithLink));
            foreach (var attributesWithLink in attributesWithLinks)
            {
                GetPageByURL(httpClient, new Uri(httpClient.BaseAddress, attributesWithLink.Value), level + 1);
            }
        }
        private string CreateFileName(HtmlDocument document)
        {
            return document.DocumentNode.Descendants("title").FirstOrDefault()?.InnerText + ".html";
        }

        private Stream CreateStreamHTMLPage(HtmlDocument document)
        {
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
        private bool IsAttributeWithLink(HtmlAttribute attribute)
        {
            return attribute.Name == "src" || attribute.Name == "href";
        }





    }
}
