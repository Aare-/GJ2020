
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WhatCollided : MonoBehaviour {

    [SerializeField] protected CanvasGroup _SpeechBubble;

    [SerializeField] protected Image _Icon;
    
    protected void OnEnable() {
        _SpeechBubble.alpha = 0.0f;
        
        TinyTokenManager
            .Instance
            .Register(this, (Msg.ObjectCollided m) => {
                NotifyAboutCollidingObject(m.CollidingObject);
            });
    }

    protected void OnDisable() {
        TinyTokenManager
            .Instance
            .UnregisterAll(this);
    }

    protected void Update() {
        transform.LookAt(Camera.main.transform);
    }

    private void NotifyAboutCollidingObject(StaticObjectBehaviour obj) {
        _Icon.sprite = obj.Icon;

        DOTween.Kill(_SpeechBubble, false);
        
        _SpeechBubble.alpha = 1.0f;

        _SpeechBubble
            .DOFade(0.0f, 0.5f)
            .SetDelay(3.0f);
    }
}
