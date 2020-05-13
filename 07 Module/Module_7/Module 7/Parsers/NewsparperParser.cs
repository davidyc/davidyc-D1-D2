using System;
using System.Collections.Generic;
using System.Text;
using Module_7.Interfaces;

namespace Module_7.Parsers
{
    public class NewsparperParser : IParserElement
    {
        public string ElementName => "Newspaper";

        IEnumerable<IEntity> IParserElement.ReadElement()
        {
            throw new NotImplementedException();
        }
    }
}
