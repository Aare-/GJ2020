using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class ExtensionVector3 {
    public static Vector3 Z(float value) {
        return new Vector3(0, 0, value);
    }    
}