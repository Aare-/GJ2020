using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StaticObjectBehaviour : MonoBehaviour {
    private RoomManager _RoomManager;

    private Rigidbody _Body;

    private Collider _Collider;
    
    private bool _ResetVelocityAndPositon;

    private Vector2 _LastXZPosition;

    private Quaternion _LastRotation;

    private Collider _OtherColliderStopIgnoring;

    private int _KeepPositionStill = 0;
    
    // Start is called before the first frame update
    void Start() {
        _RoomManager  = FindObjectOfType<RoomManager>();
        _Body = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        if (_OtherColliderStopIgnoring != null) {
            Physics.IgnoreCollision(_OtherColliderStopIgnoring, _Collider, false);
            _OtherColliderStopIgnoring = null;
            
        }

        if (_KeepPositionStill > 0) {
            _KeepPositionStill--;
            
            transform.position = new Vector3(
                _LastXZPosition.x, 
                transform.position.y,
                _LastXZPosition.y);
            _Body.velocity = new Vector3(0, 0.0f, 0);
            _Body.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision coll) {
        
        if (coll.gameObject.CompareTag("Object")) {
            var xyImpulse = new Vector2(coll.impulse.x, coll.impulse.z);

            Debug.Log("Collision " + coll.impulse);
            
            // we block only collisions that influence the xy position
            if (Mathf.Abs(coll.impulse.y) == 0) {
                _ResetVelocityAndPositon = true;
                _RoomManager.BlockRotation();
                
                Debug.Log("BLOCKER Collided BLOCKING "+coll.impulse, this);

                var trans = transform;
            
                trans.position = new Vector3(
                    _LastXZPosition.x, 
                    trans.position.y,
                    _LastXZPosition.y);
                trans.rotation = _LastRotation;
                _Body.velocity = new Vector3(0, 0.0f, 0);
                _Body.angularVelocity = Vector3.zero;

                _KeepPositionStill = 0;

                Physics.IgnoreCollision(coll.collider, this._Collider, true);
                _OtherColliderStopIgnoring = coll.collider;
            }
            else {
                Debug.Log("Collided, non-blocking "+coll.gameObject.tag, coll.gameObject);   
            }
        }
    }

    protected void LateUpdate() {
        if (_ResetVelocityAndPositon) {
            Debug.Log("Reset static obj");
            _ResetVelocityAndPositon = false;

            //_Body.velocity = new Vector3(0, _Body.velocity.y, 0);
            //_Body.angularVelocity = Vector3.zero;
            
            
        }
        else {
            var pos = transform.position;
            _LastXZPosition = new Vector2(pos.x, pos.z);
            _LastRotation = transform.rotation;
        }
    }
}
