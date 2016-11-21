using System;
using System.Windows;
using IMDBWPF;
using IMDBWPF.Application;
using IMDBWPF.UI.WPF;
using SimpleInjector;

static class Program
{
    [STAThread]
    static void Main()
    {
        var container = Bootstrap();

        // Any additional other configuration, e.g. of your desired MVVM toolkit.

        RunApplication(container);
    }

    private static Container Bootstrap()
    {
        // Create the container as usual.
        var container = new Container();

        // Register your types, for instance:
        //container.Register<FilmsModel>(Lifestyle.Singleton);
        //container.Register<FilmViewModel>();

        // Register your windows and view models:
        container.Register<IMDB>();
        container.Register<FilmViewModel>();

        container.Verify();

        return container;
    }

    private static void RunApplication(Container container)
    {
        try
        {
            var app = new App();
            var mainWindow = container.GetInstance<IMDB>();
            app.Run(mainWindow);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}