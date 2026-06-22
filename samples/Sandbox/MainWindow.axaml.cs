using Avalonia.Controls;
using Avalonia.Input;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object? sender, Avalonia.Input.FocusChangedEventArgs e)
        {
            if (TopLevel.GetTopLevel(sender as Control)?.FocusManager.FindNextElement(NavigationDirection.Next, new() { FocusedElement = sender as Control }) is { } focusTarget)
                focusTarget.Focus();
            // equivalently TopLevel.GetTopLevel(sender as Control)?.FocusManager.TryMoveFocus(NavigationDirection.Next, new() { FocusedElement = sender as Control });
        }
    }
}
