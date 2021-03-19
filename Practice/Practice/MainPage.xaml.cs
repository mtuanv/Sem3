using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net.Attributes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Practice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public MainPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "database.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Contact>();
            var query = conn.Table<Contact>();
            string id = "";
            string name = "";
            string phone = "";
            foreach (var message in query)
            {
                id = id + " " + message.Id;
                name = name + " " + message.Name;
                phone = phone + " " + message.Phone;
            }
            textBlock1.Text = "ID: " + id + "\nName: " + name + "\nPhone: " + phone + "\n";
        }
        public class Contact
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            [Unique]
            public string Phone { get; set; }
        }

        private void Addct_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContact));
        }

        private void Searchct_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchContact));
        }
    }
}
