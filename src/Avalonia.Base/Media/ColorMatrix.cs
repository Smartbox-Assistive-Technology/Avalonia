using System;
using System.Globalization;
using Avalonia.Utilities;

namespace Avalonia.Media
{
    /// <summary>
    /// A 4x5 matrix.
    /// </summary>
    /// <remarks>ColorMatrix layout:
    ///                 | 1st col (R) | 2nd col (G) | 3rd col (B) | 4th col (A) | 5th col (1) |
    /// 1st row (Red)   | R -> R      | G -> R      | B -> R      | A -> R      | 1 -> R      |
    /// 2nd row (Green) | R -> G      | G -> G      | B -> G      | A -> G      | 1 -> G      |
    /// 3rd row (Blue)  | R -> B      | G -> B      | B -> B      | A -> B      | 1 -> B      |
    /// 4th row (Alpha) | R -> A      | G -> A      | B -> A      | A -> A      | 1 -> A      |
    /// </remarks>
#if !BUILDTASK
    public
#endif
    readonly struct ColorMatrix : IEquatable<ColorMatrix>
    {
        private readonly double _m11;
        private readonly double _m12;
        private readonly double _m13;
        private readonly double _m14;
        private readonly double _m15;
        private readonly double _m21;
        private readonly double _m22;
        private readonly double _m23;
        private readonly double _m24;
        private readonly double _m25;
        private readonly double _m31;
        private readonly double _m32;
        private readonly double _m33;
        private readonly double _m34;
        private readonly double _m35;
        private readonly double _m41;
        private readonly double _m42;
        private readonly double _m43;
        private readonly double _m44;
        private readonly double _m45;

        public ColorMatrix(double m11, double m12, double m13, double m14, double m15, double m21, double m22, double m23, double m24, double m25, double m31, double m32, double m33, double m34, double m35, double m41, double m42, double m43, double m44, double m45)
        {
            _m11 = m11;
            _m12 = m12;
            _m13 = m13;
            _m14 = m14;
            _m15 = m15;
            _m21 = m21;
            _m22 = m22;
            _m23 = m23;
            _m24 = m24;
            _m25 = m25;
            _m31 = m31;
            _m32 = m32;
            _m33 = m33;
            _m34 = m34;
            _m35 = m35;
            _m41 = m41;
            _m42 = m42;
            _m43 = m43;
            _m44 = m44;
            _m45 = m45;
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static ColorMatrix Identity { get; } = new ColorMatrix(
            1.0, 0.0, 0.0, 0.0, 0.0,
            0.0, 1.0, 0.0, 0.0, 0.0,
            0.0, 0.0, 1.0, 0.0, 0.0,
            0.0, 0.0, 0.0, 1.0, 0.0);

        /// <summary>
        /// Returns the canonical grey-scale matrix.
        /// </summary>
        public static ColorMatrix Greyscale { get; } = new ColorMatrix(
            0.21, 0.72, 0.07, 0.0, 0.0,
            0.21, 0.72, 0.07, 0.0, 0.0,
            0.21, 0.72, 0.07, 0.0, 0.0,
            0.00, 0.0 , 0.0 , 1.0, 0.0);

        /// <summary>
        /// Returns a color-inversion matrix.
        /// </summary>
        /// <remarks>
        /// Inverts the Red, Green, and Blue components without changing the Alpha component.
        /// </remarks>
        public static ColorMatrix Inversion { get; } = new ColorMatrix(
            -1.0, 0.0,  0.0,  0.0,  1.0,
            0.0,  -1.0, 0.0,  0.0,  1.0,
            0.0,  0.0,  -1.0, 0.0,  1.0,
            0.0,  0.0,  0.0,  1.0,  0.0);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// The first element of the first row (Red from Red component).
        /// </summary>
        public double M11 => _m11;

        /// <summary>
        /// The second element of the first row (Red from Green component).
        /// </summary>
        public double M12 => _m12;
        
        /// <summary>
        /// The third element of the first row (Red from Blue component).
        /// </summary>
        public double M13 => _m13;

        /// <summary>
        /// The fourth element of the first row (Red from Alpha component).
        /// </summary>
        public double M14 => _m14;

        /// <summary>
        /// The fifth element of the first row (Red constant component).
        /// </summary>
        public double M15 => _m15;

        /// <summary>
        /// The first element of the second row (Green from Red component).
        /// </summary>
        public double M21 => _m21;

        /// <summary>
        /// The second element of the second row (Green from Green component).
        /// </summary>
        public double M22 => _m22;
        
        /// <summary>
        /// The third element of the second row (Green from Blue component).
        /// </summary>
        public double M23 => _m23;

        /// <summary>
        /// The fourth element of the second row (Green from Alpha component).
        /// </summary>
        public double M24 => _m24;

        /// <summary>
        /// The fifth element of the second row (Green constant component).
        /// </summary>
        public double M25 => _m25;

        /// <summary>
        /// The first element of the third row (Blue from Red component).
        /// </summary>
        public double M31 => _m31;

        /// <summary>
        /// The second element of the third row (Blue from Green component).
        /// </summary>
        public double M32 => _m32;
        
        /// <summary>
        /// The third element of the third row (Blue from Blue component).
        /// </summary>
        public double M33 => _m33;

        /// <summary>
        /// The fourth element of the third row (Blue from Alpha component).
        /// </summary>
        public double M34 => _m34;

        /// <summary>
        /// The fifth element of the third row (Blue constant component).
        /// </summary>
        public double M35 => _m35;

        /// <summary>
        /// The first element of the fourth row (Alpha from Red component).
        /// </summary>
        public double M41 => _m41;

        /// <summary>
        /// The second element of the fourth row (Alpha from Green component).
        /// </summary>
        public double M42 => _m42;
        
        /// <summary>
        /// The third element of the fourth row (Alpha from Blue component).
        /// </summary>
        public double M43 => _m43;

        /// <summary>
        /// The fourth element of the fourth row (Alpha from Alpha component).
        /// </summary>
        public double M44 => _m44;

        /// <summary>
        /// The fifth element of the fourth row (Alpha constant component).
        /// </summary>
        public double M45 => _m45;

        /// <summary>
        /// Multiplies two matrices together and returns the resulting matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The product matrix.</returns>
        public static ColorMatrix operator *(ColorMatrix value1, ColorMatrix value2)
        {
            return new ColorMatrix(
                value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41 + value1.M15 * 0,
                value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42 + value1.M15 * 0,
                value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43 + value1.M15 * 0,
                value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44 + value1.M15 * 0,
                value1.M11 * value2.M15 + value1.M12 * value2.M25 + value1.M13 * value2.M35 + value1.M14 * value2.M45 + value1.M15 * 1,
                value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41 + value1.M25 * 0,
                value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42 + value1.M25 * 0,
                value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43 + value1.M25 * 0,
                value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44 + value1.M25 * 0,
                value1.M21 * value2.M15 + value1.M22 * value2.M25 + value1.M23 * value2.M35 + value1.M24 * value2.M45 + value1.M25 * 1,
                value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41 + value1.M35 * 0,
                value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42 + value1.M35 * 0,
                value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43 + value1.M35 * 0,
                value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44 + value1.M35 * 0,
                value1.M31 * value2.M15 + value1.M32 * value2.M25 + value1.M33 * value2.M35 + value1.M34 * value2.M45 + value1.M35 * 1,
                value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41 + value1.M45 * 0,
                value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42 + value1.M45 * 0,
                value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43 + value1.M45 * 0,
                value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44 + value1.M45 * 0,
                value1.M41 * value2.M15 + value1.M42 * value2.M25 + value1.M43 * value2.M35 + value1.M44 * value2.M45 + value1.M45 * 1);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are equal; False otherwise.</returns>
        public static bool operator ==(ColorMatrix value1, ColorMatrix value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are not equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are not equal; False if they are equal.</returns>
        public static bool operator !=(ColorMatrix value1, ColorMatrix value2)
        {
            return !value1.Equals(value2);
        }

        /// <summary>
        /// Creates a translation matrix from the given X and Y components.
        /// </summary>
        /// <param name="xPosition">The X position.</param>
        /// <param name="yPosition">The Y position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix CreateTranslation(double xPosition, double yPosition)
        {
            return new Matrix(1.0, 0.0, 0.0, 1.0, xPosition, yPosition);
        }

        /// <summary>
        /// Appends another matrix as post-multiplication operation.
        /// Equivalent to this * value;
        /// </summary>
        /// <param name="value">A matrix.</param>
        /// <returns>Post-multiplied matrix.</returns>
        public ColorMatrix Append(ColorMatrix value)
        {
            return this * value;
        }

        /// <summary>
        /// Prepends another matrix as pre-multiplication operation.
        /// Equivalent to value * this;
        /// </summary>
        /// <param name="value">A matrix.</param>
        /// <returns>Pre-multiplied matrix.</returns>
        public ColorMatrix Prepend(ColorMatrix value)
        {
            return value * this;
        }

        /// <summary>
        ///  Transforms the color with the matrix
        /// </summary>
        /// <param name="c">The color to be transformed</param>
        /// <returns>The transformed color</returns>
        public Color Transform(Color c)
        {
            return new Color(
                (byte)MathUtilities.Clamp(_m41 * c.R + _m42 * c.G + _m43 * c.B + _m44 * c.A + 255 * _m45, 0, 255),
                (byte)MathUtilities.Clamp(_m11 * c.R + _m12 * c.G + _m13 * c.B + _m14 * c.A + 255 * _m15, 0, 255),
                (byte)MathUtilities.Clamp(_m21 * c.R + _m22 * c.G + _m23 * c.B + _m24 * c.A + 255 * _m25, 0, 255),
                (byte)MathUtilities.Clamp(_m31 * c.R + _m32 * c.G + _m33 * c.B + _m34 * c.A + 255 * _m35, 0, 255));
        }

        /// <summary>
        /// Returns a boolean indicating whether the matrix is equal to the other given matrix.
        /// </summary>
        /// <param name="other">The other matrix to test equality against.</param>
        /// <returns>True if this matrix is equal to other; False otherwise.</returns>
        public bool Equals(ColorMatrix other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return _m11 == other.M11 &&
                   _m12 == other.M12 &&
                   _m13 == other.M13 &&
                   _m14 == other.M14 &&
                   _m15 == other.M15 &&
                   _m21 == other.M21 &&
                   _m22 == other.M22 &&
                   _m23 == other.M23 &&
                   _m24 == other.M24 &&
                   _m25 == other.M25 &&
                   _m31 == other.M31 &&
                   _m32 == other.M32 &&
                   _m33 == other.M33 &&
                   _m34 == other.M34 &&
                   _m35 == other.M35 &&
                   _m41 == other.M41 &&
                   _m42 == other.M42 &&
                   _m43 == other.M43 &&
                   _m44 == other.M44 &&
                   _m45 == other.M45;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Matrix other && Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return (_m11, _m12, _m13, _m14, _m15, _m21, _m22, _m23, _m24, _m25, _m31, _m32, _m33, _m34, _m35, _m41, _m42, _m43, _m44, _m45).GetHashCode();
        }

        /// <summary>
        /// Parses a <see cref="Matrix"/> string.
        /// </summary>
        /// <param name="s">Twenty comma-delimited double values that describe the new <see cref="ColorMatrix"/></param>
        /// <returns>The <see cref="ColorMatrix"/>.</returns>
        public static ColorMatrix Parse(string s)
        {
            using (var tokenizer = new SpanStringTokenizer(s, CultureInfo.InvariantCulture, exceptionMessage: "Invalid Matrix."))
            {
                var v1 = tokenizer.ReadDouble();
                var v2 = tokenizer.ReadDouble();
                var v3 = tokenizer.ReadDouble();
                var v4 = tokenizer.ReadDouble();
                var v5 = tokenizer.ReadDouble();
                var v6 = tokenizer.ReadDouble();
                var v7 = tokenizer.ReadDouble();
                var v8 = tokenizer.ReadDouble();
                var v9 = tokenizer.ReadDouble();
                var v10 = tokenizer.ReadDouble();
                var v11 = tokenizer.ReadDouble();
                var v12 = tokenizer.ReadDouble();
                var v13 = tokenizer.ReadDouble();
                var v14 = tokenizer.ReadDouble();
                var v15 = tokenizer.ReadDouble();
                var v16 = tokenizer.ReadDouble();
                var v17 = tokenizer.ReadDouble();
                var v18 = tokenizer.ReadDouble();
                var v19 = tokenizer.ReadDouble();
                var v20 = tokenizer.ReadDouble();

                return new ColorMatrix(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20);
            }
        }

        /// <summary>
        /// Prepares a row-major array of floats from the <see cref="ColorMatrix"/> that can be applied to supplied to SKColorFilter.CreateColorMatrix.
        /// </summary>
        /// <returns>The array.</returns>
        internal float[] ToRowMajorFloatArray()
        {
            return [
                (float)_m11,
                (float)_m12,
                (float)_m13,
                (float)_m14,
                (float)_m15,
                (float)_m21,
                (float)_m22,
                (float)_m23,
                (float)_m24,
                (float)_m25,
                (float)_m31,
                (float)_m32,
                (float)_m33,
                (float)_m34,
                (float)_m35,
                (float)_m41,
                (float)_m42,
                (float)_m43,
                (float)_m44,
                (float)_m45,
            ];
        }
    }
}
