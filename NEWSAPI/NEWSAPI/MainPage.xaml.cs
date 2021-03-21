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

using System.Collections.ObjectModel;
using NEWSAPI.Models;

namespace NEWSAPI
{
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Article> Articles;
        public MainPage()
        {
            this.InitializeComponent();
            Articles = new ObservableCollection<Article>();
            GetAR("tesla", "2021-02-"+System.DateTime.Now.DayOfWeek, "publishedAt", "20f0bc79a63947818867de418e5f4ab3");
        }

        private async void GetAR(string q, string from, string SortBy, string ApiKey)
        {
            //var url = string.Format("https://newsapi.org/v2/everything?q={0}&from={1}&sortBy={2}&apiKey={3}", q, from, SortBy, ApiKey);
            var url = string.Format("http://newsapi.org/v2/everything?q={0}&from={1}&sortBy={2}&apiKey={3}", q, from, SortBy, ApiKey);
            var news = await New.GetNew(url) as RootObject;
            news.articles.ForEach(n => {
                Articles.Add(n);
            });
        }

        private void View_ItemClick(object sender, ItemClickEventArgs e)
        {
            Article ar = e.ClickedItem as Article;
            Frame.Navigate(typeof(ArticleView), ar);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                NewsItemGrid.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "Home";
            }
            else if (Bookmark.IsSelected)
            {
                NewsItemGrid.Visibility = Visibility.Collapsed;
                TitleTextBlock.Text = "Bookmark";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }
    }
}
