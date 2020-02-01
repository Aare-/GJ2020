using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class ExtensionVector2 {
    public static Vector2 Rotate(this Vector2 v, float degrees) {
        var radians = degrees * Mathf.Deg2Rad;
        var sin = Mathf.Sin(radians);
        var cos = Mathf.Cos(radians);

        var tx = v.x;
        var ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static Vector2 Mul(this Vector2 v, float val) {        
        return new Vector2(v.x * val, v.y * val);
    }
}
