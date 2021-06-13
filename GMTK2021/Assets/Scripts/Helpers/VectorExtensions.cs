using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    static class VectorExtensions
    {
        public static Vector2 WithX(this Vector2 incoming, float x)
        {
            return new Vector2(x, incoming.y);
        }

        public static Vector2 WithY(this Vector2 incoming, float y)
        {
            return new Vector2(incoming.x, y);
        }

        public static Vector2 WithFloorY(this Vector2 incoming, float minY)
        {
            return new Vector2(incoming.x, Mathf.Max(minY, incoming.y));
        }

        public static Vector2 ApplyX(this Vector2 incoming, float x)
        {
            return new Vector2(incoming.x + x, incoming.y);
        }

        public static Vector2 ApplyY(this Vector2 incoming, float y)
        {
            return new Vector2(incoming.x, incoming.y + y);
        }

        public static Vector2 ReverseX(this Vector2 incoming)
        {
            return new Vector2(-incoming.x, incoming.y);
        }

        public static Vector3 WithX(this Vector3 incoming, float x)
        {
            return new Vector3(x, incoming.y, incoming.z);
        }

        public static Vector3 WithY(this Vector3 incoming, float y)
        {
            return new Vector3(incoming.x, y, incoming.z);
        }

        public static Vector3 ApplyX(this Vector3 incoming, float x)
        {
            return new Vector3(incoming.x + x, incoming.y, incoming.z);
        }

        public static Vector3 ApplyY(this Vector3 incoming, float y)
        {
            return new Vector3(incoming.x, incoming.y + y, incoming.z);
        }

        public static Vector3 ReverseX(this Vector3 incoming)
        {
            return new Vector3(-incoming.x, incoming.y, incoming.z);
        }

        public static Vector3 MultiplyZ(this Vector3 incoming, float value)
        {
            return new Vector3(-incoming.x, incoming.y, incoming.z * value);
        }
    }
}
