using BookLib.Category_enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models
{
    public class Book : AbstractItem
    {
        public BookCategory Category { get; set; }

        public Book(long isbn, string name, string writer, string publisher, double price, DateTime publishDate, DateTime printDate, int edition, int copyNumber, BookCategory category) : base(isbn, name, writer, publisher, price, publishDate, printDate, edition, copyNumber)
        {
            Category = category;
        }
    }
}
