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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Practice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchContact : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public SearchContact()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "database.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
        }
        public class Contact
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            [Unique]
            public string Phone { get; set; }
        }
        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            string noti = "";
            int c = 0;
            string id = "";
            string name1 = "";
            string phone1 = "";
            string name = textBox.Text;
            string phone = textBox1.Text;
            var query = conn.Table<Contact>();
            foreach (var message in query)
            {
                if(name == message.Name)
                {
                    id = id + " " + message.Id;
                    name1 = name1 + " " + message.Name;
                    phone1 = phone1 + " " + message.Phone;
                    c = 1;
                    break;
                }
                else
                {
                    noti = "Contact not found.";
                }
            }
            if(c == 0)
            {
                textBlock3.Text = noti;
            }
            else
            {
                textBlock3.Text = "ID: " + id + "\nName: " + name1 + "\nPhone: " + phone1 + "\n";
                textBox1.Text = phone1;
            }
            
        }
    }
}
