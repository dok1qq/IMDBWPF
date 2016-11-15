using System;
using System.Windows.Input;

namespace IMDBWPF.Application
{
    class CommandSearch: ICommand
    {
        private FilmViewModel _vm;

        public CommandSearch(FilmViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
             _vm.SearchExecute(_vm.SearchField);
        }

        public event EventHandler CanExecuteChanged  = delegate { };
    }
}
