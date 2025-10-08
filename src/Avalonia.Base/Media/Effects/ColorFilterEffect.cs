using System.Numerics;
// ReSharper disable CheckNamespace
namespace Avalonia.Media;

public sealed class ColorFilterEffect : Effect, IColorFilterEffect, IMutableEffect
{
    public static readonly StyledProperty<ColorMatrix> MatrixProperty =
        AvaloniaProperty.Register<ColorFilterEffect, ColorMatrix>(nameof(Matrix), ColorMatrix.Identity);

    public ColorMatrix Matrix
    {
        get => GetValue(MatrixProperty);
        set => SetValue(MatrixProperty, value);
    }

    public IImmutableEffect ToImmutable() => new ImmutableColorFilterEffect(Matrix);
}
