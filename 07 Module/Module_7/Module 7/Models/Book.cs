using Module_7.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module_7.Models
{
    public class Book : IEntity
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
        public int PublishYear { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
        public string ISBN { get; set; }
    }
}
