using System;

namespace Pong {
    public class Vector2D {
        public int X { get; }
        public int Y { get; }

        public Vector2D(int x, int y) {
            X = x;
            Y = y;
        }

        public int Distance(Vector2D other) {
            throw new NotImplementedException();
        }

        public float AngleTo(Vector2D other) {
            throw new NotImplementedException();
        }


        // Comparison and operators

        protected bool Equals(Vector2D other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector2D) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X * 397) ^ Y;
            }
        }

        public static Vector2D operator +(Vector2D x, Vector2D y) {
            throw new NotImplementedException();
        }

        public static Vector2D operator -(Vector2D x, Vector2D y) {
            throw new NotImplementedException();
        }

        public static Vector2D operator *(Vector2D x, Vector2D y) {
            throw new NotImplementedException();
        }

        public static Vector2D operator /(Vector2D x, Vector2D y) {
            throw new NotImplementedException();
        }
    }
}
