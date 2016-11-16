using System.Windows;
using IMDBWPF.Application;

namespace IMDBWPF.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class IMDB : Window
    {
        public IMDB(FilmViewModel _vm)
        {
            InitializeComponent();

            base.DataContext = _vm;
        }


        /*
        private void buttonGet_Click(object sender, RoutedEventArgs e)
        {
            if (filmID.Text != "")
            {
                Control("title", filmID.Text);
            }
        }
        */
        /*
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
        */

        /*
*/
            /*

        private void ShowFilm(Film film)
        {
            table.Clear();
            
            table.Rows.Add("Title", film.Title);
            table.Rows.Add("Rating", film.Rating);
            table.Rows.Add("Description", film.Description);
            table.Rows.Add("Creator", film.Director);

            if (film.Poster != null)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(film.Poster);
                bi.EndInit();
                image.Source = bi;
            }
            dataGrid.DataContext = table.DefaultView;
        }*/
    }
}
