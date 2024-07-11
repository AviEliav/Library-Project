using BookLib;
using BookLib.Category_enums;
using BookLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace UnitTestLibraryProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_AddBook_ReturnsNotNull()
        {

            //arrange
            Manager manager = Manager.Instance;
            Book book = new Book(9783161484100, "Harry Poter", "J.K Rowling", "Bloomsbury", 100, new DateTime(1997, 6, 27), new DateTime(1999, 2, 25), 1, 1234, BookCategory.Fantasy);

            //act
            manager.AddItem(book);

            //assert
            Assert.IsNotNull(manager.items.FirstOrDefault(x => x.Name == book.Name));

        }


        [TestMethod]
        public void Test_EditBook_ReturnsTrue()
        {

            Manager manager = Manager.Instance;
            Book book = new Book(9783161484100, "Harry Poter", "J.K Rowling", "Bloomsbury", 100, new DateTime(1997, 6, 27), new DateTime(1999, 2, 25), 1, 1234, BookCategory.Fantasy);

            manager.AddItem(book);
            book.Name = "1";
            manager.EditItem(book);

            Assert.IsTrue(manager.items.FirstOrDefault(x => x.ID == book.ID).Name == "1");
        }


        [TestMethod]
        public void Test_DeleteItem_ReturnsTrue()
        {

            Manager manager = Manager.Instance;
            AbstractItem item = manager.GetAll()[0];

            manager.DeleteItem(item);

             Assert.IsFalse(manager.items.Contains(item));
        }


    }

}
