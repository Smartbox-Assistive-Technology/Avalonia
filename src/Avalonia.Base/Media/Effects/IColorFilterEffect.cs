// ReSharper disable once CheckNamespace

using Avalonia.Animation.Animators;

namespace Avalonia.Media;

public interface IColorFilterEffect : IEffect
{
    ColorMatrix Matrix { get; }
}

public class ImmutableColorFilterEffect : IColorFilterEffect, IImmutableEffect
{
    static ImmutableColorFilterEffect()
    {
        EffectAnimator.EnsureRegistered();
    }

    public ImmutableColorFilterEffect(ColorMatrix matrix)
    {
        Matrix = matrix;
    }

    public ColorMatrix Matrix { get; }

    public bool Equals(IEffect? other) =>
        other is IColorFilterEffect colorFilter && colorFilter.Matrix == Matrix;
}
