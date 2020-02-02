
using System;
using TinyMessenger;
using UnityEngine;
using UnityEngine.UI;

public class NotifyOfBlockedRotation : MonoBehaviour {

    [SerializeField]
    protected GameObject _ScrewPrompt;

    [SerializeField] protected Image _FillImage;
    
    private PositionHoldManager _HoldManager;

    protected void Start() {
        _HoldManager = FindObjectOfType<PositionHoldManager>();
    }

    protected void OnEnable() {
        TinyTokenManager
            .Instance
            .Register(this, (Msg.RotationProgress m) => { _FillImage.fillAmount = m.Progress; });
    }

    protected void OnDisable() {
        TinyTokenManager
            .Instance
            .UnregisterAll(this);
    }

    protected void Update() {
        _ScrewPrompt.gameObject.SetActive(_HoldManager.CanRotate);
    }
}
