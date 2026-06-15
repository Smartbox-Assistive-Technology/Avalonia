using Avalonia;
using Avalonia.Rendering.Composition;

namespace Sandbox
{
    public class Program
    {
        static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new Win32PlatformOptions() { RenderingMode = [Win32RenderingMode.AngleEgl] }) // software rendering is less sensitive to system configuration
                .With(new SkiaOptions() { MaxGpuResourceSizeBytes = 100_000_000 }) // lots of resources is necessary for adequate performance with large images if not using Software rendering
                .With(new CompositionOptions() { UseRegionDirtyRectClipping = true }) // makes more consistent behaviour when throwing the mouse around the screen
                .LogToTrace();
    }
}
