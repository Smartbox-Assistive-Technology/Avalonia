// ReSharper disable once CheckNamespace

using Avalonia.Animation.Animators;

namespace Avalonia.Media;

public interface IColorMatrixEffect : IEffect
{
    ColorMatrix Matrix { get; }
}

public class ImmutableColorMatrixEffect : IColorMatrixEffect, IImmutableEffect
{
    static ImmutableColorMatrixEffect()
    {
        EffectAnimator.EnsureRegistered();
    }

    public ImmutableColorMatrixEffect(ColorMatrix matrix)
    {
        Matrix = matrix;
    }

    public ColorMatrix Matrix { get; }

    public bool Equals(IEffect? other) =>
        other is IColorMatrixEffect colorFilter && colorFilter.Matrix == Matrix;
}
