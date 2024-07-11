using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models
{
    public abstract class AbstractItem 
    {
        protected AbstractItem(long isbn, string name, string writer, string publisher, double price, DateTime publishDate, DateTime printDate, int edition, int copyNumber)
        {
            ID = Guid.NewGuid();
            Isbn = isbn;
            Name = name;
            Writer = writer;
            Publisher = publisher;
            Price = price;
            PublishDate = publishDate;
            PrintDate = printDate;
            Edition = edition;
            CopyNumber = copyNumber;
        }

      

        public double FinalPrice
        {
            get { return Price - (Price * (Discount/100)); }
        }

        public double Discount { get; set; }

        public Guid ID { get; set; }

        public long Isbn { get; set; }

        public string Name{ get; set; }

        public string Writer { get; set; }

        public string Publisher { get; set; }

        public double Price { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime PrintDate { get; set; }

        public int Edition { get; set; }

        public int CopyNumber { get; set; }

        
    }
}
