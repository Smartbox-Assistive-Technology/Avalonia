// ReSharper disable CheckNamespace
namespace Avalonia.Media;

public sealed class ColorMatrixEffect : Effect, IColorMatrixEffect, IMutableEffect
{
    public static readonly StyledProperty<ColorMatrix> MatrixProperty =
        AvaloniaProperty.Register<ColorMatrixEffect, ColorMatrix>(nameof(Matrix), ColorMatrix.Identity);

    public ColorMatrix Matrix
    {
        get => GetValue(MatrixProperty);
        set => SetValue(MatrixProperty, value);
    }

    public IImmutableEffect ToImmutable() => new ImmutableColorMatrixEffect(Matrix);
}
