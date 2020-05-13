using System;
using System.Collections.Generic;
using System.Text;
using Module_7.Interfaces;

namespace Module_7.Parsers
{
    public class BookParser : IParserElement
    {
        public string ElementName => "Book";


        IEnumerable<IEntity> IParserElement.ReadElement()
        {
            throw new NotImplementedException();
        }
    }
}
