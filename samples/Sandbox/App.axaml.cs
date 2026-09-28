using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Sandbox
{
    public class App : Application
    {
        private WpfBits.App? _wpfApp;

        public override void Initialize()
        {
            _wpfApp = new(Callback);
            AvaloniaXamlLoader.Load(this);
        }

        private void Callback()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mw = desktop.MainWindow = new MainWindow();
                mw.Show();
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();
        }
    }
}
