using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asm_web_1.Models
{
    public class orders
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public string bookName { get; set; }
        public int userid { get; set; }
        public int quantity { get; set; }
        public DateTime orderdate { get; set; }

    }
}
