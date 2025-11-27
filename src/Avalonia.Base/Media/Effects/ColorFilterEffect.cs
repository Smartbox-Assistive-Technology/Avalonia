// ReSharper disable CheckNamespace
namespace Avalonia.Media;

public sealed class ColorMatrixFilterEffect : Effect, IColorMatrixFilterEffect, IMutableEffect
{
    public static readonly StyledProperty<ColorMatrix> MatrixProperty =
        AvaloniaProperty.Register<ColorMatrixFilterEffect, ColorMatrix>(nameof(Matrix), ColorMatrix.Identity);

    public ColorMatrix Matrix
    {
        get => GetValue(MatrixProperty);
        set => SetValue(MatrixProperty, value);
    }

    public IImmutableEffect ToImmutable() => new ImmutableColorMatrixFilterEffect(Matrix);
}
