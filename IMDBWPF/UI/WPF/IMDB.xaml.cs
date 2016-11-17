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
    }
}
