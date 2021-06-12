using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class ColorExtensions
    {
        public static Color WithTransparency(this Color color, float transparency)
        {
            return new Color(color.r, color.g, color.b, transparency);
        }
    }
}
