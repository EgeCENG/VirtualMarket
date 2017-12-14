using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TreeViewModel
    {
        public List<ProductWithLevel> productWithLevels { get; set; }
        public List<int> CountOfElementEachLevel { get; set; }
        public int ElementCount { get; set; }
        public int Depth { get; set; }
    }
}