using System;
using System.Data;
using LiteDB;
using HtmlAgilityPack;

namespace IMDBWPF.Application
{
    class FilmsModel
    {
        private string title = "//*[@itemprop='name']";
        private string rating = "//*[@itemprop='ratingValue']";
        private string description = "//*[@itemprop='description']";
        private string creator = "//*[@itemprop='creator']/a/span";
        private string fImage = "//*[@class='poster']/a";
        private string nImage = "//*[@class='image']/a";
        private DataTable table;

        public FilmsModel(DataTable _table)
        {
            table = _table;
        }

        private void SetFilmInDB(Film film)
        {
            using (var db = new LiteDatabase(@"Films.db"))
            {
                var col = db.GetCollection<Film>("films");
                col.Insert(film);
            }
        }

        private Film GetFilmFromDB(string id)
        {
            using (var db = new LiteDatabase(@"Films.db"))
            {
                var col = db.GetCollection<Film>("films");
                return col.FindOne(film => film.Id == id);
            }
        }


        public Film Control(string property, string id)
        {
            Film film = GetFilmFromDB(id);

            if (film == null)
            {
                film = GetInfoOfFilm(property, id);
                if (film != null)
                {
                    SetFilmInDB(film);
                }
            }

            return film;
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
    }
}
