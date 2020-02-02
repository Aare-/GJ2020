﻿using UnityEngine;
using TinyMessenger;


[RequireComponent(typeof(Rigidbody))]
public class StaticObjectBehaviour : MonoBehaviour {

    [Header("Config")] 
    public Sprite Icon;

    [Header("Movement Path")] 
    public bool UsesPath;
    
    public Transform Path;

    public Vector3 MovementVector;

    public float MovePosition;
    
    private RoomManager _RoomManager;
    
    private PositionHoldManager _PHolderManager;

    private Rigidbody _Body;

    private Collider _Collider;

    private Collider _OtherColliderStopIgnoring;

    private bool _HasToBeMoved = false;

    public Rigidbody Body => _Body;

    public bool HasToBeMoved => _HasToBeMoved;

    // Start is called before the first frame update
    void Start() {
        _RoomManager = FindObjectOfType<RoomManager>();
        _PHolderManager = FindObjectOfType<PositionHoldManager>();
        _Body = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
        
        _PHolderManager.RegisterStaticObject(this);
        
        Path.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update() {
        if (_OtherColliderStopIgnoring != null) {
            Physics.IgnoreCollision(_OtherColliderStopIgnoring, _Collider, false);
            _OtherColliderStopIgnoring = null;
        }
    }

    private void OnCollisionEnter(Collision coll) {

        if(coll.gameObject.layer == 8 && Mathf.Abs(coll.relativeVelocity.y) == 0f) {
            if (ItemHolder.selectedObject != this){
                TinyMessengerHub
                    .Instance
                    .Publish(Msg.ObjectCollided.Get(this));
            }

            _HasToBeMoved = true;
            _RoomManager.BlockRotation();
        }
    }

    public void WasMoved() {
        _HasToBeMoved = false;
    }
}
