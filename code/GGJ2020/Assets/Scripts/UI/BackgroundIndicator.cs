
using System;
using TinyMessenger;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundIndicator : MonoBehaviour {
    public static BackgroundIndicator Instance;
    
    [SerializeField] protected Color _LightOnColour;
    
    [SerializeField] protected Color _LightOffColour;

    [SerializeField] protected Image _BackgroundImage;

    protected void Awake() {
        Instance = this;
    }

    protected void OnEnable() {
        _BackgroundImage.color = _LightOnColour;
        
        TinyTokenManager
            .Instance
            .Register(this, (Msg.StartGame m) => { TurnLightOff(); });
    }

    protected void OnDisable() {
        TinyTokenManager
            .Instance
            .UnregisterAll(this);
    }

    public void TurnLightOff() {
        _BackgroundImage.color = _LightOffColour;
    }

    public void TurnLightOn() {
        _BackgroundImage.color = _LightOnColour;
    }
}
