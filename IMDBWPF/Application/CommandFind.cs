

using System;
using System.Windows.Input;

namespace IMDBWPF.Application
{
    class CommandFind: ICommand
    {
        private FilmViewModel _vm;

        public CommandFind(FilmViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            _vm.FindExecute(_vm.FilmIDField);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
