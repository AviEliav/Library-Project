using BookLib;
using BookLib.Models;
using Library_Project.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager manager = Manager.Instance;

        public MainWindow()
        {
            InitializeComponent();          
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem add = new AddItem();
            add.ShowDialog();
        }

        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            EditItem edit = new EditItem();
            edit.ShowDialog();
        }

        private void btnItemList_Click(object sender, RoutedEventArgs e)
        {
            ItemList item = new ItemList();
            item.ShowDialog();      
        }



      
    }
}
