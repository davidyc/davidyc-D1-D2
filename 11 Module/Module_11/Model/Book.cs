﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11.Model
{
    public class Book
    {
        [BsonId]
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public string[] Genre { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"Name:{Name} Author:{Author} Count: {Count} Year:{Year}";
        }

    }
}
