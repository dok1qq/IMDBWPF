using System;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace IMDBWPF.Application
{
    class FilmViewModel
    {
        private string _filmId;
        private string _search;
        private FilmsModel _model;

        private ICommand FindCommand
        {
            get { return new DelegateCommand(FindExecute, FuncToEvaluate); }
        }

        private readonly ICommand SearchCommand;

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
            //FindCommand = new CommandFind(this);
            //SearchCommand = new CommandSearch(this);
            DataTable table = new DataTable("Film info");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Property", typeof(string));

            _model = new FilmsModel(table);
        }

        public void FindExecute(object context)
        {
            _model.Control("title", FilmIDField);
        }

        public bool FuncToEvaluate(object context)
        {
            return true;
        }

        public void SearchExecute(string searchText)
        {
            //...
        }
    }
}
