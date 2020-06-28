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
        private readonly ILogger _logger;
        private readonly ISet<Uri> _alreadyVisited = new HashSet<Uri>();
        private string _domain;
        private bool _onlyBaseDomain;

        public List<String> FileExetention { get; set; }

        public SiteContentDownloader(IContentSaver contentSaver, ILogger logger, int maxDeepLevel = 0, 
            bool onlyBaseDomain = false)
        {
            if (maxDeepLevel < 0)
            {
                new ArgumentException("Bad argument. Deep kevel can not be less zero");
            }
            _maxDeepLevel = maxDeepLevel;
            _contentSaver = contentSaver;
            _logger = logger;
            _onlyBaseDomain = onlyBaseDomain;
            FileExetention = new List<string>();
        }

        public void LoadFromURL(string URL)
        {
            _alreadyVisited.Clear();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URL);
                _domain = httpClient.BaseAddress.Host;                
                GetPageByURL(httpClient, httpClient.BaseAddress, 0);
            }
        }

        private void GetPageByURL(HttpClient httpClient, Uri uri, int level)
        {
            if (IsMaxDeepLevel(level) || _alreadyVisited.Contains(uri) || NeedGetAllDomain(uri))
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
                if (NeedSaveFile(uri.AbsoluteUri))
                    SaveContentFile(httpClient, uri);
                else
                    _logger.MakeLog($"File {uri.AbsoluteUri} extension is not suitable");
            }
        }

        private bool NeedSaveFile(string path)
        {
            var extension = Path.GetExtension(path);
            return FileExetention.Contains(extension) || FileExetention.Count == 0;
        }

        private bool IsMaxDeepLevel(int level)
        {
            return level > _maxDeepLevel;
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
            _logger.MakeLog($"Save file {uri.AbsoluteUri}");
            _contentSaver.SaveFile(uri, response.Content.ReadAsStreamAsync().Result);
        }

        private void SaveHtmlPage(HttpClient httpClient, Uri uri, int level)
        {                    
            var response = httpClient.GetAsync(uri).Result;
            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);
            _logger.MakeLog($"Save page {uri.AbsoluteUri}");
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

        private void SetDomain(string domain)
        {
            if (_onlyBaseDomain)
                _domain = String.Empty;
            else
                _domain = domain;
        }

        private bool NeedGetAllDomain(Uri uri)
        {
            return _domain != uri.Host && _onlyBaseDomain;        
        }
    }
}
