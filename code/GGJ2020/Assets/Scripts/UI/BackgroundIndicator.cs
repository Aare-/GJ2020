
using System;
using TinyMessenger;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundIndicator : MonoBehaviour {
    [SerializeField] protected Color _LightOnColour;
    
    [SerializeField] protected Color _LightOffColour;

    [SerializeField] protected Image _BackgroundImage;
    
    protected void OnEnable() {
        _BackgroundImage.color = _LightOnColour;
        
        TinyTokenManager
            .Instance
            .Register(this, (Msg.StartGame m) => { _BackgroundImage.color = _LightOffColour; });
    }

    protected void OnDisable() {
        TinyTokenManager
            .Instance
            .UnregisterAll(this);
    }
}
