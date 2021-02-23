using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSTAPP.BAL
{
    public class ReturnModel<T> where T : class
    {
        public T datam { get; set; }
        public IEnumerable<T> data { get; set; }
        public string code { get; set; }
        public string error { get; set; }
    }
}