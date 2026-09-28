using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WpfBits;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Action? Callback { get; set; }

    public MainWindow()
    {
        Console.WriteLine("MainWindow");
        InitializeComponent();
        Console.WriteLine("MainWindow2");

        this.ShowActivated = false;
        this.Loaded += OnLoaded;
        this.Closing += (s, e) =>
        {
            e.Cancel = true;
            // Activate 'fixes' the issue (assuming you cancel the close) (NOTE: this doesn't seem to do anything in the pre-logged-in-startup-state)
            this.Activate();
            // Hide doesn't seem to make it better or worse
            this.Hide();
            Callback?.Invoke();
        };
        this.Closed += (s, e) => System.Console.WriteLine("Closed");
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // set NOACTIVATE on the window to induce the problem
        var hWnd = new WindowInteropHelper(this).Handle;
        var extendedStyles = NativeMethods.GetExtendedStyle(hWnd);
        extendedStyles |= 0x0800_0000; // WS_EX_NOACTIVATE
        NativeMethods.SetExtendedStyle(hWnd, extendedStyles);
    }
}

public static class NativeMethods
{
    private const int GWL_EXSTYLE = -20;

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    private static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    private static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
    {
        if (Environment.Is64BitProcess)
            return SetWindowLong64(hWnd, nIndex, dwNewLong);
        return SetWindowLong32(hWnd, nIndex, dwNewLong);
    }
    public static IntPtr GetWindowLong(IntPtr hWnd, int nIndex)
    {
        if (Environment.Is64BitProcess)
            return GetWindowLong64(hWnd, nIndex);
        return GetWindowLong32(hWnd, nIndex);
    }
    public static void SetExtendedStyle(IntPtr hWnd, IntPtr style, [CallerMemberName] string? caller = null)
    {
        if (SetWindowLong(hWnd, GWL_EXSTYLE, style) == IntPtr.Zero)
            System.Console.WriteLine($"Failed to SetWindowLong from {caller}, win32Error:{Marshal.GetLastWin32Error()}");
    }
    public static IntPtr GetExtendedStyle(IntPtr hWnd, [CallerMemberName] string? caller = null)
    {
        var style = GetWindowLong(hWnd, GWL_EXSTYLE);
        if (style == IntPtr.Zero)
            System.Console.WriteLine($"Failed to GetWindowLong from {caller}, win32Error:{Marshal.GetLastWin32Error()}");
        return style;
    }
}