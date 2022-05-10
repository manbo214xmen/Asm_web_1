using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asm_web_1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string info { get; set; }
        public int bookquantity { get; set; }
        public int price { get; set; }
        public int cataid { get; set; }
        public string author { get; set; }
        public string imgfile { get; set; }

    }
}
