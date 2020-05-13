using Module_7.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module_7.Parsers
{
    public class PatentParser : IParserElement
    {
        public string ElementName => "Patent";

        IEnumerable<IEntity> IParserElement.ReadElement()
        {
            throw new NotImplementedException();
        }
    }
}
