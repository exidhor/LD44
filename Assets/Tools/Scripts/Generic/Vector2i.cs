﻿using System;
using UnityEngine;

#pragma warning disable 219 // to hide the warning "x, y values never used [...]"

namespace Tools
{
    [Serializable]
    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2i operator +(Vector2i c1, Vector2i c2)
        {
            return new Vector2i(c1.x + c2.x, c1.y + c2.y);
        }

        public static Vector2i operator -(Vector2i c1, Vector2i c2)
        {
            return new Vector2i(c1.x - c2.x, c1.y - c2.y);
        }

        public static Vector2 operator *(Vector2i c, float f)
        {
            return new Vector2(c.x * f, c.y * f);
        }

        public static Vector2 operator *(float f, Vector2i c)
        {
            return new Vector2(c.x * f, c.y * f);
        }

        public static bool operator ==(Vector2i c1, Vector2i c2)
        {
            return c1.x == c2.x
                   && c1.y == c2.y;
        }

        public static bool operator !=(Vector2i c1, Vector2i c2)
        {
            return !(c1 == c2);
        }

        public static implicit operator Vector2(Vector2i c)
        {
            return new Vector2(c.x, c.y);
        }

        public static explicit operator Vector2i(Vector2 c)
        {
            return new Vector2i((int) c.x, (int) c.y);
        }

        public override string ToString()
        {
            return "Vector2i(" + x + ", " + y + ")";
        }

        public bool Equals(Vector2i other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Vector2i && Equals((Vector2i)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
    }
}

#pragma warning restore 219
