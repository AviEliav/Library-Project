using BookLib;
using BookLib.Category_enums;
using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_Project.Pages
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        Manager manager = Manager.Instance;


        public AddItem()
        {
            InitializeComponent();

            string[] itemType = new[] { "Book", "Journal" };

            ComboBoxType.ItemsSource = itemType;
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
            if (isbn < 999999999999 || isbn > 10000000000000  )
            {
                MessageBox.Show("Please enter the right amount of Isbn digits (13) ");
                return;
            }

            // Checking that you haven't entered existing Isbn
            for (int i = 0; i < manager.items.Count; i++)
            {
                if (isbn == manager.items[i].Isbn)
                {
                    MessageBox.Show("You entered an existing Isbn");
                    return;
                }
            }
            
         
           
            // Check if the item type you choose in the CombotBox is Book, check if the fileds are not null, and save the item
            if (ComboBoxType.Text == "Book" && !( TextBoxIsbn.Text == "" ||  TextBoxName.Text == "" || TextBoxWriter.Text == "" || TextBoxPublisher.Text == "" || TextBoxPrice.ToString() == "" || PublishDate.SelectedDate.Equals(null) || PrintDate.SelectedDate.Equals(null) || TextBoxEdition.ToString() == "" || TextBoxCopyNumber.ToString() == "" || ComboBoxGenre.SelectedItem == null))
            {         
                BookCategory genre = (BookCategory)ComboBoxGenre.SelectedItem;
                Book newBook = new Book(isbn, TextBoxName.Text, TextBoxWriter.Text, TextBoxPublisher.Text, price, PublishDate.SelectedDate.Value, PrintDate.SelectedDate.Value, edition, copy, genre);
                manager.AddItem(newBook);
                manager.UpdateDiscountAfterEdit(newBook);

                MessageBox.Show("The Item has been saved successfully");
                this.Close();
            }

           // Check if the item type you choose in the ComboBox is Jornal, check if the fileds are not null, and save the item
            else if (ComboBoxType.Text == "Journal" && !(TextBoxIsbn.Text == "" || TextBoxName.Text == "" || TextBoxWriter.Text == "" || TextBoxPublisher.Text == "" || TextBoxPrice.ToString() == "" || PublishDate.SelectedDate.Equals(null) || PrintDate.SelectedDate.Equals(null) || TextBoxEdition.ToString() == "" || TextBoxCopyNumber.ToString() == "" || ComboBoxGenre.SelectedItem == null))
            {
                JournalCategory genre = (JournalCategory)ComboBoxGenre.SelectedItem;
                Journal newJournal = new Journal(isbn, TextBoxName.Text, TextBoxWriter.Text, TextBoxPublisher.Text, price, PublishDate.SelectedDate.Value, PrintDate.SelectedDate.Value, edition, copy, genre);
                manager.AddItem(newJournal);
                manager.UpdateDiscountAfterEdit(newJournal);

                MessageBox.Show("The Item has been saved successfully");
                this.Close();
            }

            else
            {
                MessageBox.Show("Please fill all the fileds");
            }
             
        }


        // Return to the home page
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // Check if the item type you choose is book or jornal and base on your selection give you the unique categorys for each item
        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                      
                if (ComboBoxType.SelectedItem.ToString() == "Book")
                    ComboBoxGenre.ItemsSource = new BookCategory[] { BookCategory.Adventure, BookCategory.Fantasy, BookCategory.Horror, BookCategory.Romance, BookCategory.Motivational };

                else 
                    ComboBoxGenre.ItemsSource = new JournalCategory[] { JournalCategory.Science, JournalCategory.Technology, JournalCategory.History, JournalCategory.Psychology, JournalCategory.Fashion, JournalCategory.Nature };
             
        }


        // Validate that you can only enter numbers in the TextBox
        private void TextBoxIsbn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }


        // Validate that you can only enter numbers and one dot in the TextBox for having decimal price
        private void TextBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
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


        // Validate that you can only enter numbers in the TextBox
        private void TextBoxEdition_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }


        // validate that you can only enter numbers to the TextBox
        private void TextBoxCopyNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

    }
}
