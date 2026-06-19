using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.TextFormatting;

namespace Sandbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            var container = new StackPanel();

            var scale = LayoutHelper.GetLayoutScale(this);

            var typeFaces = new[]
            {
                // swap these around to see the madness
                new Typeface(new FontFamily("Segoe UI")),
                new Typeface(new FontFamily("Roboto")),
            };

            const string text = "dots  ●  ⬤  ●  dots";

            foreach (var tf in typeFaces)
            {
                var f = new TextLayout(text, tf, foreground: Brushes.Black);

                var grid = new Grid()
                {
                    RowDefinitions = [.. Enumerable.Repeat(GridLength.Auto, 2 + f.TextLines[0].TextRuns.Count).Select(l => new RowDefinition(l))],
                    ColumnDefinitions = [new(GridLength.Auto), new(GridLength.Auto), new(GridLength.Star)],
                    ColumnSpacing = 10,
                    RowSpacing = 5,
                };

                int r = 0;
                void addRow(params Control[] controls)
                {
                    int c = 0;
                    foreach (var control in controls)
                    {
                        control[Grid.RowProperty] = r;
                        control[Grid.ColumnProperty] = c++;
                        grid.Children.Add(control);
                    }
                    r++;
                }

                addRow(
                    new TextBlock() { Text = "SelectableTextBlock", },
                    new SelectableTextBlock()
                    {
                        Text = text,
                        FontFamily = tf.FontFamily,
                    }
                );

                foreach (var tr in f.TextLines[0].TextRuns)
                {
                    addRow(
                        new TextBlock() { Text = $"Text Run {r-1}", },
                        new SelectableTextBlock()
                        {
                            Text = tr.Text+"",
                            FontFamily = tr.Properties!.Typeface.FontFamily,
                        },
                        new SelectableTextBlock()
                        {
                            Text = tr.Properties!.Typeface.FontFamily+"",
                            FontFamily = tr.Properties!.Typeface.FontFamily,
                        }
                    );
                }

                // scale ** 2 here makes no sense... but whatever
                var wb = new RenderTargetBitmap(new((int)Math.Ceiling(f.Width * scale * scale), (int)Math.Ceiling(f.Height * scale * scale)), new(96 * scale, 96 * scale));
                using (var dc = wb.CreateDrawingContext())
                {
                    dc.DrawRectangle(Brushes.AliceBlue, null, new(0, 0, f.Width, f.Height));
                    f.Draw(dc, default);
                }

                addRow(
                    new TextBlock() { Text = "TextLayout.Draw", },
                    new Image()
                    {
                        Stretch = Stretch.None,
                        Source = wb,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    }
                );

                var gb = new GroupBox()
                {
                    Header = tf.FontFamily + "",
                    Content = grid,
                };

                container.Children.Add(gb);
            }

            this.Content = container;
        }
    }
}
