using BookLib.Category_enums;
using BookLib.Models;
using System;
using System.Collections.Generic;

namespace BookLib
{
    public class Manager
    {
        private static Manager instance;

        private Manager()
        {
            UpdateDiscount();
        }


        public List<AbstractItem> items = new List<AbstractItem>
        {
         new Book(9783161484100,"Harry Poter", "J.K Rowling", "Bloomsbury", 100, new DateTime(1997, 6, 27), new DateTime(1999, 2, 25), 1, 1234, BookCategory.Fantasy),
         new Book(9780976773665, "Can't Hurt Me", "David Goggins", "Lioncrest", 150, new DateTime(2018, 12, 3), new DateTime(2020, 7, 7), 1, 12345, BookCategory.Motivational),
         new Book(9781566199094,"Rich dad poor dad", " Robert T. Kiyosaki", "Plata", 100, new DateTime(1997, 4, 11), new DateTime(1998, 6, 11), 1, 123456, BookCategory.Horror),


         new Journal(9781402894626,"Vogue", "Ana Wintor", "Conde Nast", 30, new DateTime(2023, 8, 6), new DateTime(2023, 8, 6), 2, 1234567, JournalCategory.Fashion),
         new Journal(9789295055025,"National Geographic", "William Albert Allard", "Penguin", 40, new DateTime(2023, 8, 7), new DateTime(2023, 8, 7), 1, 1234578, JournalCategory.Nature),
         new Journal(9781485198697, "WIRED", "Rob Reddick", "Conde Nast", 60, new DateTime(2022, 7, 16), new DateTime(2022, 7, 16), 1, 123456, JournalCategory.Technology),
        };


        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Manager();
                }
                return instance;
            }

            set { instance = value; }
        }


        //Return the AbstractItem List
        public List<AbstractItem> GetAll()
        {
            return items;
        }


        //Adding item
        public void AddItem(AbstractItem item)
        {
            items.Add(item);
        }


        //Deleteing item
        public void DeleteItem(AbstractItem item)
        {
            items.Remove(item);
        }


        //Editing item
        public void EditItem(AbstractItem item)
        {
            foreach (AbstractItem key in items)
            {
                if (key.ID == item.ID)
                {
                    key.Isbn = item.Isbn;
                    key.Name = item.Name;
                    key.Writer = item.Writer;
                    key.Publisher = item.Publisher;
                    key.Price = item.Price;
                    key.PublishDate = item.PublishDate;
                    key.PrintDate = item.PrintDate;
                    key.Edition = item.Edition;
                    key.CopyNumber = item.CopyNumber;


                    if (key is Book book && item is Book Bookitem)
                    {
                        book.Category = Bookitem.Category;
                    }

                    else if (key is Journal journal && item is Journal Journalitem)
                    {
                        journal.Category = Journalitem.Category;
                    }
                }
            }
        }



        // Calculating and showing the discount the itme gets
        public void UpdateDiscount()
        {
            foreach (AbstractItem item in items)
            {
                UpdateDiscountAfterEdit(item);              
            }

        }



        // Calculating and showing the discount the itme gets after edit
        public void UpdateDiscountAfterEdit(AbstractItem UpdatedItem)
        {
            foreach (AbstractItem item in items)
            {
                if (UpdatedItem.ID == item.ID)
                {
                    double discount = 0;

                    if (item is Book)
                    {
                        Book book = (Book)item;
                        if (book.Category == BookCategory.Fantasy)
                        {
                            discount = 10 > discount ? 10 : discount;
                        }
                    }

                    if (item.Writer == "J.K Rowling")
                    {
                        discount = 20 > discount ? 20 : discount;
                    }

                    if (item.PublishDate.Year < 2017)
                    {
                        discount = 5 > discount ? 5 : discount;
                    }

                    if (item.Publisher == "Conde Nast")
                    {
                        discount = 10 > discount ? 10 : discount;
                    }
                    item.Discount = discount;
                }



            }
        }

    }

}
