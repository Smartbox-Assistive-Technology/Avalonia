using System;
using System.Windows;

namespace WpfBits;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private Action _callback { get; }

    public App(Action callback)
    {
        Console.WriteLine("app");
        _callback = callback;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mw = new MainWindow() { Callback = _callback };
        mw.Show();
    }
}

