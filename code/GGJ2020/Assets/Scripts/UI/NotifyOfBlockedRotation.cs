
using System;
using UnityEngine;

public class NotifyOfBlockedRotation : MonoBehaviour {
    [SerializeField]
    protected GameObject _Info;
    
    [SerializeField]
    protected GameObject _ScrewPrompt;

    private PositionHoldManager _HoldManager;

    protected void Start() {
        _HoldManager = FindObjectOfType<PositionHoldManager>();
    }

    protected void Update() {
        _Info.gameObject.SetActive(!_HoldManager.CanRotate);
        _ScrewPrompt.gameObject.SetActive(_HoldManager.CanRotate);
    }
}
