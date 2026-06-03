using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Media;
using Xunit;

namespace Avalonia.Skia.RenderTests
{
    public class StreamGeometryTests : TestBase
    {
        public StreamGeometryTests()
            : base(@"Media\StreamGeometry")
        {
        }
         
        [Fact]
        public async Task PreciseEllipticArc_Produces_Valid_Arcs_In_All_Directions()
        {
            var grid = new Avalonia.Controls.Primitives.UniformGrid() { Columns = 2, Rows = 4, Width = 320, Height = 400 };
            foreach (var sweepDirection in new[] { SweepDirection.Clockwise, SweepDirection.CounterClockwise })
                foreach (var isLargeArc in new[] { false, true })
                    foreach (var isPrecise in new[] { false, true })
                    {
                        Point Pt(double x, double y) => new Point(x, y);
                        Size Sz(double w, double h) => new Size(w, h);
                        var streamGeometry = new StreamGeometry();
                        using (var context = streamGeometry.Open())
                        {
                            context.BeginFigure(Pt(20, 20), true);

                            if(isPrecise)
                                context.PreciseArcTo(Pt(40, 40), Sz(20, 20), 0, isLargeArc, sweepDirection);
                            else
                                context.ArcTo(Pt(40, 40), Sz(20, 20), 0, isLargeArc, sweepDirection);
                            context.LineTo(Pt(40, 20));
                            context.LineTo(Pt(20, 20));
                            context.EndFigure(true);
                        }
                        var pathShape = new Avalonia.Controls.Shapes.Path();
                        pathShape.Data = streamGeometry;
                        pathShape.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
                        pathShape.Fill = new SolidColorBrush(Colors.Gold);
                        pathShape.StrokeThickness = 2;
                        pathShape.Margin = new Thickness(20);
                        grid.Children.Add(pathShape);
                    }
            await RenderToFile(grid);
        }

        [Fact]
        public void Can_Clone_StreamGeometry_With_Transform()
        {
            var streamGeometry = StreamGeometry.Parse("M10,190 l190,-190 M0,0M200,200");
            streamGeometry.Transform = new TranslateTransform(50, 150);

            var cloned = streamGeometry.Clone();

            Assert.Equal(streamGeometry.Transform.Value, cloned.Transform?.Value);
        }

        [Fact]
        public void Can_Open_StreamGeometry_With_Transform()
        {
            var streamGeometry = StreamGeometry.Parse("M10,190 l190,-190 M0,0M200,200");
            streamGeometry.Transform = new TranslateTransform(50, 150);

            using (var context = streamGeometry.Open())
            {
                context.BeginFigure(new(0, 0), true);
                context.LineTo(new (100, 100));
                context.EndFigure(true);
            }
        }
    }
}
