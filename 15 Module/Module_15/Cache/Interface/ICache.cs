﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Interface
{
    public interface ICache<T>
    {
        T Get(string key);
        void Set(string key, T value, DateTimeOffset expirationDate);
    }
}
