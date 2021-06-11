using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    static class Vector2Extensions
    {
        public static Vector2 WithX(this Vector2 incoming, float x)
        {
            return new Vector2(x, incoming.y);
        }

        public static Vector2 WithY(this Vector2 incoming, float y)
        {
            return new Vector2(incoming.x, y);
        }

        public static Vector2 ApplyX(this Vector2 incoming, float x)
        {
            return new Vector2(incoming.x + x, incoming.y);
        }

        public static Vector2 ApplyY(this Vector2 incoming, float y)
        {
            return new Vector2(incoming.x, incoming.y + y);
        }
    }
}
