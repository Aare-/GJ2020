﻿using System;
using System.Collections.Generic;
using TinyMessenger;
using UnityEngine;

public class Msg {

    
    public class ObjectCollided : ITinyMessage {
        private static ObjectCollided _Instance;

        public StaticObjectBehaviour CollidingObject;

        #region Implementation
        public static ObjectCollided Get(StaticObjectBehaviour sObj) {
            if (_Instance == null)
                _Instance = new ObjectCollided();

            _Instance.CollidingObject = sObj;

            return _Instance;
        }

        public object Sender => null;

        #endregion        
    }
    
    public class RotationProgress : ITinyMessage {
        private static RotationProgress _Instance;

        public float Progress;

        #region Implementation
        public static RotationProgress Get(float newProgress) {
            if (_Instance == null)
                _Instance = new RotationProgress();

            _Instance.Progress = Mathf.Clamp01(newProgress);

            return _Instance;
        }

        public object Sender => null;

        #endregion        
    }
    
    public class StartGame : ITinyMessage {
        private static StartGame _Instance;

        #region Implementation
        public static StartGame Get() {
            if (_Instance == null)
                _Instance = new StartGame();

            return _Instance;
        }

        public object Sender => null;

        #endregion        
    }

}
