// ReSharper disable once CheckNamespace

using Avalonia.Animation.Animators;

namespace Avalonia.Media;

public interface IColorMatrixFilterEffect : IEffect
{
    ColorMatrix Matrix { get; }
}

public class ImmutableColorMatrixFilterEffect : IColorMatrixFilterEffect, IImmutableEffect
{
    static ImmutableColorMatrixFilterEffect()
    {
        EffectAnimator.EnsureRegistered();
    }

    public ImmutableColorMatrixFilterEffect(ColorMatrix matrix)
    {
        Matrix = matrix;
    }

    public ColorMatrix Matrix { get; }

    public bool Equals(IEffect? other) =>
        other is IColorMatrixFilterEffect colorFilter && colorFilter.Matrix == Matrix;
}
