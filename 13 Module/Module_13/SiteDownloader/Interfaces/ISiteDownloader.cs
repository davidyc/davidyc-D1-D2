using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteDownloader.Interfaces
{
    public interface ISiteContentDownloader
    {
        void LoadFromURL(string URL);       
     }
}
