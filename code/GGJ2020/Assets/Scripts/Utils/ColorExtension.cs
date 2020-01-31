using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ColorExtension {
    public static string ToHex(this Color color) {
        return "#" + ColorUtility.ToHtmlStringRGB(color);        
    }

    public static Color32 ChangeAlpha(this Color32 color, float newAlpha) {
        return new Color32(
            color.r, 
            color.g, 
            color.b, 
            (byte)Math.Floor(255 * newAlpha));
    }

    public static Color ChangeAlpha(this Color color, float newAlpha) {
        return new Color(
            color.r,
            color.g,
            color.b,
            newAlpha);
    }

    public static Color32 ChangeRGB(this Color32 color, Color32 newColor) {
        return new Color32(
            newColor.r,
            newColor.g,
            newColor.b,
            color.a);
    }

    public static Color ChangeRGB(this Color color, Color newRGB) {
        return new Color(
            newRGB.r,
            newRGB.g,
            newRGB.b,
            color.a);
    }
}