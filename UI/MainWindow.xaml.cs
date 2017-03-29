using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using UI.Model;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var request = WebRequest.Create("http://localhost/settings/getsetting?searchField=" + searchField.Text);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                string json = sr.ReadToEnd();
                List<Setting> settings = JsonConvert.DeserializeObject<List<Setting>>(json);
                searchResultList.ItemsSource = settings;

                using (var db = new SettingContext())
                {
                    var all = db.Settings;
                    db.Settings.RemoveRange(all);
                    db.SaveChanges();
                    db.Settings.AddRange(settings);
                    db.SaveChanges();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new SettingContext())
            {
                db.Database.EnsureCreated();

                List<Setting> settings = new List<Setting>();

                foreach (var d in db.Settings)
                {
                    settings.Add(d);
                }

                searchResultList.ItemsSource = settings;
            }
        }
    }
}
