using System;
using UnityEngine;

namespace Data
{
    public static class ColorTypeExtension
    {
        public static Color GetColor(this ColorType color)
        {
            switch (color)
            {
                case ColorType.Blue:
                    return new Color(64/255f, 104/255f, 183/255f);
                case ColorType.Green:
                    return new Color(14 / 255f, 192 / 255f, 0);
                case ColorType.Red:
                    return new Color(207/255f,0,0);
                case ColorType.White:
                    return new Color(1, 1, 1);
                case ColorType.Black:
                    return new Color(0,0, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }        
        }

    }
}