using System;
using System.Collections.Generic;
using System.Data;
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
using LiteDB;
using HtmlAgilityPack;

namespace IMDBWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string title = "//*[@itemprop='name']";
        private string rating = "//*[@itemprop='ratingValue']";
        private string description = "//*[@itemprop='description']";
        private string creator = "//*[@itemprop='creator']/a/span";
        private string fImage = "//*[@class='poster']/a";
        private string nImage = "//*[@class='image']/a";
        private DataTable table;

        public MainWindow()
        {
            table = new DataTable("Film info");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Property", typeof(string));

            InitializeComponent();
        }

        private void buttonGet_Click(object sender, RoutedEventArgs e)
        {
            if (filmID.Text != "")
            {
                Control("title", filmID.Text);
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            HtmlWeb web = new HtmlWeb();

            HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.imdb.com/find?ref_=nv_sr_fn&q=" + search.Text + "&s=all");
            if (doc != null)
            {
                HtmlNode cell = doc.DocumentNode.SelectSingleNode("//*[@class='findList']").SelectSingleNode("tr").SelectSingleNode("th|td");
                string text = cell.InnerHtml;
                string[] substrings = text.Split('/');

                Control(substrings[1], substrings[2]);
            }
        }

        private void Control(string property, string id)
        {
            Film film = CheckFilmInDB(id);

            if (film != null)
            {
                ShowFilm(film);
            }
            else
            {
                film = GetInfoOfFilm(property, id);
                if (film != null)
                {
                    AddFilmInCollection(film);
                    ShowFilm(film);
                }
            }
        }

        private Film GetInfoOfFilm(string property, string _id)
        {
            HtmlWeb webId = new HtmlWeb();

            HtmlAgilityPack.HtmlDocument doc = webId.Load("http://www.imdb.com/" + property + "/" + _id);

            if (doc != null)
            {
                string recieveTitle = "";
                string recieveRating = "";
                string recieveDescription = "";
                string recieveCreator = "";
                string resultUrlImage = "";

                HtmlNode recTitle = doc.DocumentNode.SelectSingleNode(title);
                if (recTitle != null)
                {
                    recieveTitle = recTitle.InnerText;
                }

                HtmlNode recRating = doc.DocumentNode.SelectSingleNode(rating);
                if (recRating != null)
                {
                    recieveRating = recRating.InnerText;
                }

                HtmlNode recDescription = doc.DocumentNode.SelectSingleNode(description);
                if (recDescription != null)
                {
                    recieveDescription = recDescription.InnerText;
                }

                HtmlNode recCreator = doc.DocumentNode.SelectSingleNode(creator);
                if (recCreator != null)
                {
                    recieveCreator = recCreator.InnerHtml;
                }


                if (property.Equals("title"))
                {
                    string sImg = doc.DocumentNode.SelectSingleNode(fImage).InnerHtml;
                    string[] substrings = sImg.Split('"');
                    resultUrlImage = substrings[5];
                }

                if (property.Equals("name"))
                {
                    string sImg = doc.DocumentNode.SelectSingleNode(nImage).InnerHtml;
                    string[] substrings = sImg.Split('"');
                    resultUrlImage = substrings[11];
                }

                return new Film()
                {
                    Id = _id,
                    Title = recieveTitle,
                    Rating = recieveRating,
                    Director = recieveCreator,
                    Description = recieveDescription,
                    Poster = resultUrlImage
                };
            }
            else
            {
                return null;
            }
        }

        private void ShowFilm(Film film)
        {
            table.Clear();
            //image.Hide();

            table.Rows.Add("Title", film.Title);
            table.Rows.Add("Rating", film.Rating);
            table.Rows.Add("Description", film.Description);
            table.Rows.Add("Creator", film.Director);

            if (film.Poster != null)
            {
                //filmImage.Load(film.Poster);
                //filmImage.Show();
            }

            dataGrid.DataContext = table;
        }

        private void AddFilmInCollection(Film film)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Film>("films");
                col.Insert(film);
            }
        }

        private Film CheckFilmInDB(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Film>("films");
                return col.FindOne(film => film.Id == id);
            }
        }
    }

    public class Film
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Poster { get; set; }
    }
}
