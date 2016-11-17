using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IMDBWPF.Application
{
    public class FilmViewModel
    {
        private string _filmId;
        private string _search;
        private FilmsModel _model;

        private ObservableCollection<Film> _myFilms = new ObservableCollection<Film>();
        public ObservableCollection<Film> CurrentFilms
        {
            get { return this._myFilms; }
            
        }

        public ICommand FindCommand
        {
            get { return new DelegateCommand(FindExecute, FuncToEvaluate); }
        }

        public ICommand SearchCommand
        {
            get { return new DelegateCommand(SearchExecute, FuncToEvaluate); }
        }

        public string FilmIDField
        {
            get { return _filmId;  }
            set { _filmId = value; }
        }

        public string SearchField
        {
            get { return _search; }
            set { _search = value; }
        }

        public FilmViewModel()
        {
            _model = new FilmsModel();
        }

        public void FindExecute(object context)
        {
            if (FilmIDField != "")
            {
                Film _film = _model.Control("title", FilmIDField);
                if (_film != null)
                {
                    CurrentFilms.Add(_film);
                }
            }
        }

        public void SearchExecute(object context)
        {
            if (SearchField != "")
            {
                Film _film = _model.Search(SearchField);
                if (_film != null)
                {
                    CurrentFilms.Add(_film);
                }
            }
        }

        public bool FuncToEvaluate(object context)
        {
            return true;
        }
    }
}
