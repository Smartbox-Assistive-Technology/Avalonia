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
        private readonly float _m11;
        private readonly float _m12;
        private readonly float _m13;
        private readonly float _m14;
        private readonly float _m15;
        private readonly float _m21;
        private readonly float _m22;
        private readonly float _m23;
        private readonly float _m24;
        private readonly float _m25;
        private readonly float _m31;
        private readonly float _m32;
        private readonly float _m33;
        private readonly float _m34;
        private readonly float _m35;
        private readonly float _m41;
        private readonly float _m42;
        private readonly float _m43;
        private readonly float _m44;
        private readonly float _m45;

        public ColorMatrix(float m11, float m12, float m13, float m14, float m15, float m21, float m22, float m23, float m24, float m25, float m31, float m32, float m33, float m34, float m35, float m41, float m42, float m43, float m44, float m45)
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
            1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f, 0.0f);

        /// <summary>
        /// Returns the canonical grey-scale matrix.
        /// </summary>
        public static ColorMatrix Greyscale { get; } = new ColorMatrix(
            0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
            0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
            0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
            0.00f, 0.0f,  0.0f,  1.0f, 0.0f);

        /// <summary>
        /// Returns a color-inversion matrix.
        /// </summary>
        /// <remarks>
        /// Inverts the Red, Green, and Blue components without changing the Alpha component.
        /// </remarks>
        public static ColorMatrix Inversion { get; } = new ColorMatrix(
            -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
            0.0f,  -1.0f, 0.0f,  0.0f,  1.0f,
            0.0f,  0.0f,  -1.0f, 0.0f,  1.0f,
            0.0f,  0.0f,  0.0f,  1.0f,  0.0f);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// The first element of the first row (Red from Red component).
        /// </summary>
        public float M11 => _m11;

        /// <summary>
        /// The second element of the first row (Red from Green component).
        /// </summary>
        public float M12 => _m12;
        
        /// <summary>
        /// The third element of the first row (Red from Blue component).
        /// </summary>
        public float M13 => _m13;

        /// <summary>
        /// The fourth element of the first row (Red from Alpha component).
        /// </summary>
        public float M14 => _m14;

        /// <summary>
        /// The fifth element of the first row (Red constant component).
        /// </summary>
        public float M15 => _m15;

        /// <summary>
        /// The first element of the second row (Green from Red component).
        /// </summary>
        public float M21 => _m21;

        /// <summary>
        /// The second element of the second row (Green from Green component).
        /// </summary>
        public float M22 => _m22;
        
        /// <summary>
        /// The third element of the second row (Green from Blue component).
        /// </summary>
        public float M23 => _m23;

        /// <summary>
        /// The fourth element of the second row (Green from Alpha component).
        /// </summary>
        public float M24 => _m24;

        /// <summary>
        /// The fifth element of the second row (Green constant component).
        /// </summary>
        public float M25 => _m25;

        /// <summary>
        /// The first element of the third row (Blue from Red component).
        /// </summary>
        public float M31 => _m31;

        /// <summary>
        /// The second element of the third row (Blue from Green component).
        /// </summary>
        public float M32 => _m32;
        
        /// <summary>
        /// The third element of the third row (Blue from Blue component).
        /// </summary>
        public float M33 => _m33;

        /// <summary>
        /// The fourth element of the third row (Blue from Alpha component).
        /// </summary>
        public float M34 => _m34;

        /// <summary>
        /// The fifth element of the third row (Blue constant component).
        /// </summary>
        public float M35 => _m35;

        /// <summary>
        /// The first element of the fourth row (Alpha from Red component).
        /// </summary>
        public float M41 => _m41;

        /// <summary>
        /// The second element of the fourth row (Alpha from Green component).
        /// </summary>
        public float M42 => _m42;
        
        /// <summary>
        /// The third element of the fourth row (Alpha from Blue component).
        /// </summary>
        public float M43 => _m43;

        /// <summary>
        /// The fourth element of the fourth row (Alpha from Alpha component).
        /// </summary>
        public float M44 => _m44;

        /// <summary>
        /// The fifth element of the fourth row (Alpha constant component).
        /// </summary>
        public float M45 => _m45;

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
        /// <param name="s">Twenty comma-delimited float values that describe the new <see cref="ColorMatrix"/></param>
        /// <returns>The <see cref="ColorMatrix"/>.</returns>
        public static ColorMatrix Parse(string s)
        {
            using (var tokenizer = new SpanStringTokenizer(s, CultureInfo.InvariantCulture, exceptionMessage: "Invalid ColorMatrix."))
            {
                var v1 = tokenizer.ReadFloat();
                var v2 = tokenizer.ReadFloat();
                var v3 = tokenizer.ReadFloat();
                var v4 = tokenizer.ReadFloat();
                var v5 = tokenizer.ReadFloat();
                var v6 = tokenizer.ReadFloat();
                var v7 = tokenizer.ReadFloat();
                var v8 = tokenizer.ReadFloat();
                var v9 = tokenizer.ReadFloat();
                var v10 = tokenizer.ReadFloat();
                var v11 = tokenizer.ReadFloat();
                var v12 = tokenizer.ReadFloat();
                var v13 = tokenizer.ReadFloat();
                var v14 = tokenizer.ReadFloat();
                var v15 = tokenizer.ReadFloat();
                var v16 = tokenizer.ReadFloat();
                var v17 = tokenizer.ReadFloat();
                var v18 = tokenizer.ReadFloat();
                var v19 = tokenizer.ReadFloat();
                var v20 = tokenizer.ReadFloat();

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
                _m11,
                _m12,
                _m13,
                _m14,
                _m15,
                _m21,
                _m22,
                _m23,
                _m24,
                _m25,
                _m31,
                _m32,
                _m33,
                _m34,
                _m35,
                _m41,
                _m42,
                _m43,
                _m44,
                _m45,
            ];
        }
    }
}
