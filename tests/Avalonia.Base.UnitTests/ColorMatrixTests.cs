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
            0.986390407166744f,
            0.171503156548044f,
            0.914770196640457f,
            0.230380819695517f,
            0.790893072071516f,
            0.806931555773493f,
            0.0548596619135876f,
            0.956560745233267f,
            0.549687739262744f,
            0.769671546455141f,
            0.471482241583647f,
            0.749515691444674f,
            0.435306597536333f,
            0.0521188978405717f,
            0.9299150765764f,
            0.826413582705208f,
            0.788064658174381f,
            0.510206538986789f,
            0.17554109896579f,
            0.946103635180051f);

        var product = ColorMatrix.Identity * randomMatrix * ColorMatrix.Identity;

        Assert.Equal(randomMatrix, product);
    }

    [Fact]
    public void Product_Works()
    {
        // compare the product of two random 4x5 matrices with two 4x4 matrices when the 5th is all zeros
        var randomMatrix1 = new ColorMatrix(
            0.060647766536198f,
            0.588409519221642f,
            0.135071330814095f,
            0.0562377495956851f,
            0f,
            0.901844336859647f,
            0.320180624426627f,
            0.101326007383806f,
            0.618109004031799f,
            0f,
            0.935419940859977f,
            0.827118835064861f,
            0.66317863421874f,
            0.591467698419267f,
            0f,
            0.279269650265923f,
            0.995434851521653f,
            0.383912656277481f,
            0.534187272844814f,
            0f);

        var randomMatrix1_4x4 = new Matrix4x4(
            0.060647766536198f,
            0.588409519221642f,
            0.135071330814095f,
            0.0562377495956851f,
            0.901844336859647f,
            0.320180624426627f,
            0.101326007383806f,
            0.618109004031799f,
            0.935419940859977f,
            0.827118835064861f,
            0.66317863421874f,
            0.591467698419267f,
            0.279269650265923f,
            0.995434851521653f,
            0.383912656277481f,
            0.534187272844814f);
        
        var randomMatrix2 = new ColorMatrix(
            0.941320343174627f,
            0.00187479755682352f,
            0.749582550725337f,
            0.852300611369515f,
            0f,
            0.907076014954926f,
            0.20961494595847f,
            0.408693625039418f,
            0.930326214365314f,
            0f,
            0.219455221619273f,
            0.792072654770094f,
            0.53014862771279f,
            0.175842054968099f,
            0f,
            0.153303915770858f,
            0.0727266684029799f,
            0.543889123296744f,
            0.6402579097859f,
            0f);

        var randomMatrix2_4x4 = new Matrix4x4(
            0.941320343174627f,
            0.00187479755682352f,
            0.749582550725337f,
            0.852300611369515f,
            0.907076014954926f,
            0.20961494595847f,
            0.408693625039418f,
            0.930326214365314f,
            0.219455221619273f,
            0.792072654770094f,
            0.53014862771279f,
            0.175842054968099f,
            0.153303915770858f,
            0.0727266684029799f,
            0.543889123296744f,
            0.6402579097859f);

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
