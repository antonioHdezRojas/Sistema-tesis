using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STI.Helpers
{
    public class PagedData<T> where T : class
    {
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }        
        public int NumberOfPages { get; set; }        
    }
}