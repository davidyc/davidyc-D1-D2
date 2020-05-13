﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Module_7.Interfaces
{
    public interface IParserElement
    {
        string ElementName { get; }
        IEnumerable<IEntity> ReadElement();
    }
}