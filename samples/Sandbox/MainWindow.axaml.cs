using Avalonia.Controls;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RendererDiagnostics.DebugOverlays = Avalonia.Rendering.RendererDebugOverlays.Fps | Avalonia.Rendering.RendererDebugOverlays.RenderTimeGraph;
            InitializeComponent();
        }
    }
}
