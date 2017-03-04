using System;
using SFML.Window;

namespace Pong {
    public class PVector2F {
        public float X;
        public float Y;
        public double Length {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }
        }

        public PVector2F(float x, float y) {
            X = x;
            Y = y;
        }

        public PVector2F Normalise() {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Length == 0) return new PVector2F(0, 0);
            return this / (float) Length;
        }

        public static PVector2F operator -(PVector2F v) {
            return new PVector2F(-v.X, -v.Y);
        }

        public static PVector2F operator -(PVector2F v1, PVector2F v2) {
            return new PVector2F(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static PVector2F operator +(PVector2F v1, PVector2F v2) {
            return new PVector2F(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static PVector2F operator *(PVector2F v, float x) {
            return new PVector2F(v.X * x, v.Y * x);
        }

        public static PVector2F operator *(float x, PVector2F v) {
            return new PVector2F(v.X * x, v.Y * x);
        }

        public static PVector2F operator /(PVector2F v, float x) {
            return new PVector2F(v.X / x, v.Y / x);
        }

        public static implicit operator Vector2f(PVector2F vector) {
            return new Vector2f(vector.X, vector.Y);
        }

        public override string ToString() {
            return "[PVector2f] X(" + X + ") Y(" + Y + ")";
        }
    }
}
