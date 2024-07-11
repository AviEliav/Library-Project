using BookLib;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookLib;
using BookLib.Models;

namespace Library_Project.Pages
{
    /// <summary>
    /// Interaction logic for ItemList.xaml
    /// </summary>
    public partial class ItemList : Window
    {
        Manager manager = Manager.Instance;
        public ItemList()
        {
            InitializeComponent();
            ItemsLV.ItemsSource = manager.GetAll();
        }

        // Delete item 
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            AbstractItem item = (AbstractItem)ItemsLV.SelectedItem;
            manager.DeleteItem(item);
            ItemsLV.ItemsSource = null;
            ItemsLV.ItemsSource = manager.GetAll();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
