using System.Numerics;
using Avalonia.Media;
using Avalonia.Utilities;
using Xunit;

namespace Avalonia.Visuals.UnitTests;

/// <summary>
///  These tests use the "official" Matrix4x4 from the System.Numerics namespace, to validate
///  that Avalonia's own implementation of a 4x5 Matrix works correctly.
/// </summary>
public class ColorMatrixTests
{
    [Fact]
    public void Greyscale_Transformed_Color_Should_Return_Correct_Value()
    {
        var color = new Color(255, 200, 100, 20);
        var expected = new Color(255, 115, 115, 115);

        var transformedColor = ColorMatrix.Greyscale.Transform(color);

        Assert.Equal(expected, transformedColor);
    }

    [Fact]
    public void Inversion_Transformed_Color_Should_Return_Correct_Value()
    {
        var color = new Color(255, 200, 100, 20);
        var expected = new Color(255, 55, 155, 235);

        var transformedColor = ColorMatrix.Inversion.Transform(color);

        Assert.Equal(expected, transformedColor);
    }

    [Fact]
    public void ColorMatrix_Product_With_Identity_Preserves_Matrix()
    {
        var randomMatrix = new ColorMatrix(
            0.986390407166744,
            0.171503156548044,
            0.914770196640457,
            0.230380819695517,
            0.790893072071516,
            0.806931555773493,
            0.0548596619135876,
            0.956560745233267,
            0.549687739262744,
            0.769671546455141,
            0.471482241583647,
            0.749515691444674,
            0.435306597536333,
            0.0521188978405717,
            0.9299150765764,
            0.826413582705208,
            0.788064658174381,
            0.510206538986789,
            0.17554109896579,
            0.946103635180051);

        var product = ColorMatrix.Identity * randomMatrix * ColorMatrix.Identity;

        Assert.Equal(randomMatrix, product);
    }

    [Fact]
    public void Product_Works()
    {
        // compare the product of two random 4x5 matrices with two 4x4 matrices when the 5th is all zeros
        var randomMatrix1 = new ColorMatrix(
            0.060647766536198,
            0.588409519221642,
            0.135071330814095,
            0.0562377495956851,
            0,
            0.901844336859647,
            0.320180624426627,
            0.101326007383806,
            0.618109004031799,
            0,
            0.935419940859977,
            0.827118835064861,
            0.66317863421874,
            0.591467698419267,
            0,
            0.279269650265923,
            0.995434851521653,
            0.383912656277481,
            0.534187272844814,
            0);

        var randomMatrix1_4x4 = new Matrix4x4(
            (float)0.060647766536198,
            (float)0.588409519221642,
            (float)0.135071330814095,
            (float)0.0562377495956851,
            (float)0.901844336859647,
            (float)0.320180624426627,
            (float)0.101326007383806,
            (float)0.618109004031799,
            (float)0.935419940859977,
            (float)0.827118835064861,
            (float)0.66317863421874,
            (float)0.591467698419267,
            (float)0.279269650265923,
            (float)0.995434851521653,
            (float)0.383912656277481,
            (float)0.534187272844814);
        
        var randomMatrix2 = new ColorMatrix(
            0.941320343174627,
            0.00187479755682352,
            0.749582550725337,
            0.852300611369515,
            0,
            0.907076014954926,
            0.20961494595847,
            0.408693625039418,
            0.930326214365314,
            0,
            0.219455221619273,
            0.792072654770094,
            0.53014862771279,
            0.175842054968099,
            0,
            0.153303915770858,
            0.0727266684029799,
            0.543889123296744,
            0.6402579097859,
            0);

        var randomMatrix2_4x4 = new Matrix4x4(
            (float)0.941320343174627,
            (float)0.00187479755682352,
            (float)0.749582550725337,
            (float)0.852300611369515,
            (float)0.907076014954926,
            (float)0.20961494595847,
            (float)0.408693625039418,
            (float)0.930326214365314,
            (float)0.219455221619273,
            (float)0.792072654770094,
            (float)0.53014862771279,
            (float)0.175842054968099,
            (float)0.153303915770858,
            (float)0.0727266684029799,
            (float)0.543889123296744,
            (float)0.6402579097859);

        var product = randomMatrix1 * randomMatrix2;
        var product_4x4 = randomMatrix1_4x4 * randomMatrix2_4x4;

        Assert.True(MathUtilities.AreClose((float)product.M11, product_4x4.M11));
        Assert.True(MathUtilities.AreClose((float)product.M12, product_4x4.M12));
        Assert.True(MathUtilities.AreClose((float)product.M13, product_4x4.M13));
        Assert.True(MathUtilities.AreClose((float)product.M14, product_4x4.M14));
        Assert.True(MathUtilities.AreClose((float)product.M21, product_4x4.M21));
        Assert.True(MathUtilities.AreClose((float)product.M22, product_4x4.M22));
        Assert.True(MathUtilities.AreClose((float)product.M23, product_4x4.M23));
        Assert.True(MathUtilities.AreClose((float)product.M24, product_4x4.M24));
        Assert.True(MathUtilities.AreClose((float)product.M31, product_4x4.M31));
        Assert.True(MathUtilities.AreClose((float)product.M32, product_4x4.M32));
        Assert.True(MathUtilities.AreClose((float)product.M33, product_4x4.M33));
        Assert.True(MathUtilities.AreClose((float)product.M34, product_4x4.M34));
        Assert.True(MathUtilities.AreClose((float)product.M41, product_4x4.M41));
        Assert.True(MathUtilities.AreClose((float)product.M42, product_4x4.M42));
        Assert.True(MathUtilities.AreClose((float)product.M43, product_4x4.M43));
        Assert.True(MathUtilities.AreClose((float)product.M44, product_4x4.M44));
    }
    
    [Fact]
    public void ColorMatrix_Product_Preserves_Constant_Components()
    {
        // product should be the same as it would with a 5x5 matrix where the M55 position is 1
        // inversion matrix has non-zero values in the constant column
        var matrix1 = ColorMatrix.Inversion;
        var matrix2 = ColorMatrix.Greyscale;

        var product = matrix1 * matrix2;

        Assert.Equal(matrix1.M15, product.M15);
        Assert.Equal(matrix1.M25, product.M25);
        Assert.Equal(matrix1.M35, product.M35);
        Assert.Equal(matrix1.M45, product.M45);
    }
}
