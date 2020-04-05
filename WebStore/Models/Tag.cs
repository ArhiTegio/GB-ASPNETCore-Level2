using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Tag
    {
        public string[] Tags { get; set; }
        public string Vots { get; set; }

        public Tag() { }
        public Tag(string[] tags, string vots)
        {
            Tags = tags;
            Vots = vots;
        }
    }
}
