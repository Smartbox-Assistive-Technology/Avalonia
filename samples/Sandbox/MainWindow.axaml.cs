using Avalonia.Controls;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SmallCanvas_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            SmallCanvas.Children.Move(0, SmallCanvas.Children.Count - 1);
        }
    }
}
