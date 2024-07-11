using BookLib;
using BookLib.Category_enums;
using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Library_Project.Pages
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {
        Manager manager = Manager.Instance;

       List<AbstractItem> itemsName = new List<AbstractItem>();

        public EditItem()
        {
            InitializeComponent();


            string[] itemType = new[] { "Book", "Journal" };

            ComboBoxType.ItemsSource = itemType;


            for (int i = 0; i < Manager.Instance.items.Count; i++)
            {
                itemsName.Add(Manager.Instance.items[i]);
            }

            ComboBoxItem.ItemsSource = itemsName;
        }


        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public AbstractItem selectedItem;


        // Uplodaing the item details to the fileds
        private void ComboBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = ComboBoxItem.SelectedItem as AbstractItem;
            if (selectedItem != null)
            {

                TextBoxName.Text = selectedItem.Name;
                TextBoxWriter.Text = selectedItem.Writer;
                TextBoxPublisher.Text = selectedItem.Publisher;

                long isbn = selectedItem.Isbn;
                TextBoxIsbn.Text = isbn.ToString();

                double price = selectedItem.Price;
                TextBoxPrice.Text = price.ToString();

                DateTime publish = selectedItem.PublishDate;
                PublishDate.Text = publish.ToString();

                DateTime print = selectedItem.PrintDate;
                PrintDate.Text = print.ToString();

                int edition = selectedItem.Edition;
                TextBoxEdition.Text = edition.ToString();
                int copy = selectedItem.CopyNumber;
                TextBoxCopyNumber.Text = copy.ToString();

                ComboBoxType.SelectedItem = selectedItem.GetType().Name;

                if (selectedItem.GetType().Name == "Book")
                {
                    Book book = (Book)selectedItem;
                    ComboBoxGenre.SelectedItem = book.Category;
                }

                else
                {
                    Journal journal = (Journal)selectedItem;
                    ComboBoxGenre.SelectedItem = journal.Category;
                }
            }
        }



        // Check if the item type you choose is book or jornal and base on your selection give you the unique categorys for each item
        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxType.SelectedItem.ToString() == "Book")
                ComboBoxGenre.ItemsSource = new BookCategory[] { BookCategory.Adventure, BookCategory.Fantasy, BookCategory.Horror, BookCategory.Romance, BookCategory.Motivational };

            else
                ComboBoxGenre.ItemsSource = new JournalCategory[] { JournalCategory.Science, JournalCategory.Technology, JournalCategory.History, JournalCategory.Psychology, JournalCategory.Fashion, JournalCategory.Nature };
        }



        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            long isbn;
            double price;
            int edition;
            int copy;


            // Check if the TextBox is not empty 
            if (TextBoxIsbn.Text != "" && TextBoxPrice.Text != "" && TextBoxEdition.Text != "" && TextBoxCopyNumber.Text != "")
            {
                isbn = long.Parse(TextBoxIsbn.Text);
                price = double.Parse(TextBoxPrice.Text);
                edition = int.Parse(TextBoxEdition.Text);
                copy = int.Parse(TextBoxCopyNumber.Text);
            }

            else
            {
                MessageBox.Show("Please fill all the fileds");
                return;
            }


            // Check if the Isbn is valid (have 13 digits)
            if (isbn < 999999999999 || isbn > 10000000000000)
            {
                MessageBox.Show("Please enter the right amount of Isbn digits (13) ");
                return;
            }



            // Checking that you haven't entered existing Isbn
            for (int i = 0; i < manager.items.Count; i++)
            {
                if (ComboBoxItem.SelectedItem is AbstractItem at)
                {
                    if (at.ID == manager.items[i].ID) continue;

                }
                if (isbn == manager.items[i].Isbn) 
                {
                    MessageBox.Show("You entered an existing Isbn");
                    return;
                }
            }



            // Save the details you edit for the book item
            if (ComboBoxType.SelectedItem.ToString() == "Book" && !(TextBoxIsbn.Text == "" || TextBoxName.Text == "" || TextBoxWriter.Text == "" || TextBoxPublisher.Text == "" || TextBoxPrice.ToString() == "" || PublishDate.SelectedDate.Equals(null) || PrintDate.SelectedDate.Equals(null) || TextBoxEdition.ToString() == "" || TextBoxCopyNumber.ToString() == "" || ComboBoxGenre.SelectedItem == null))
            {
                isbn = long.Parse(TextBoxIsbn.Text);
                double.TryParse(TextBoxPrice.Text, out price);
                DateTime publishDate = PublishDate.DisplayDate;
                DateTime printDate = PrintDate.DisplayDate;
                edition = int.Parse(TextBoxEdition.Text);
                copy = int.Parse(TextBoxCopyNumber.Text);
                BookCategory genre = (BookCategory)ComboBoxGenre.SelectedItem;


                Book editBook = new Book(isbn, TextBoxName.Text, TextBoxWriter.Text, TextBoxPublisher.Text, price, publishDate, printDate, edition, copy, genre);
                editBook.Isbn = isbn;
                editBook.ID = selectedItem.ID;
                manager.EditItem(editBook);
                manager.UpdateDiscountAfterEdit(editBook);

                MessageBox.Show("The edit was successfully saved");
                this.Close();
            }



            // Save the details you edit for the jornal item
            else if (ComboBoxType.SelectedItem.ToString() == "Journal" && !(TextBoxIsbn.Text == "" || TextBoxName.Text == "" || TextBoxWriter.Text == "" || TextBoxPublisher.Text == "" || TextBoxPrice.ToString() == "" || PublishDate.SelectedDate.Equals(null) || PrintDate.SelectedDate.Equals(null) || TextBoxEdition.ToString() == "" || TextBoxCopyNumber.ToString() == "" || ComboBoxGenre.SelectedItem == null))
            {
                isbn = long.Parse(TextBoxIsbn.Text);
                double.TryParse(TextBoxPrice.Text, out price);
                DateTime publishDate = PublishDate.DisplayDate;
                DateTime printDate = PrintDate.DisplayDate;
                edition = int.Parse(TextBoxEdition.Text);
                copy = int.Parse(TextBoxCopyNumber.Text);
                JournalCategory genre = (JournalCategory)ComboBoxGenre.SelectedItem;


                Journal editJournal = new Journal(isbn, TextBoxName.Text, TextBoxWriter.Text, TextBoxPublisher.Text, price, publishDate, printDate, edition, copy, genre);
                editJournal.Isbn = isbn;
                editJournal.ID = selectedItem.ID;
                manager.EditItem(editJournal);
                manager.UpdateDiscountAfterEdit(editJournal);
           
                MessageBox.Show("The edit was successfully saved");
                this.Close();
            }

            else
            {
                MessageBox.Show("Please fill all the fileds");
            }
        }




        // Validate that you can only enter numbers in the TextBox
        private void TextBoxIsbn_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }



        // Validate that you can only enter numbers and one dot in the TextBox for having decimal price
        private void TextBoxPrice_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                if (TextBoxPrice.Text.Contains("."))
                    e.Handled = true;
                return;

            }

            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }



        // validate that you can only enter numbers to the TextBox
        private void TextBoxEdition_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }



        // validate that you can only enter numbers to the TextBox
        private void TextBoxCopyNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

    }
}
