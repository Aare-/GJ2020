using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;


[RequireComponent(typeof(Rigidbody))]
public class StaticObjectBehaviour : MonoBehaviour {
    private RoomManager _RoomManager;
    
    private PositionHoldManager _PHolderManager;

    private Rigidbody _Body;

    private Collider _Collider;

    private Collider _OtherColliderStopIgnoring;

    [SerializeField] Transform[] nodes;

    private bool _HasToBeMoved = false;
    public bool XAxis = false;
    public bool YAxis = false;
    public bool ZAxis = false;

    public Rigidbody Body => _Body;

    public bool HasToBeMoved => _HasToBeMoved;

    // Start is called before the first frame update
    void Start() {
        _RoomManager = FindObjectOfType<RoomManager>();
        _PHolderManager = FindObjectOfType<PositionHoldManager>();
        _Body = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
        
        _PHolderManager.RegisterStaticObject(this);
        
    }

    // Update is called once per frame
    void Update() {
        if (_OtherColliderStopIgnoring != null) {
            Physics.IgnoreCollision(_OtherColliderStopIgnoring, _Collider, false);
            _OtherColliderStopIgnoring = null;
        }
    }

    private void OnCollisionEnter(Collision coll) {
        
        if (coll.gameObject.CompareTag("Object")) {
            var xyImpulse = new Vector2(coll.impulse.x, coll.impulse.z);

            Debug.Log("Collision " + coll.impulse);
            
            // we block only collisions that influence the xy position
            if (Math.Abs(Mathf.Abs(coll.impulse.y)) < 1.0f) {
                _HasToBeMoved = true;
                
                _RoomManager.BlockRotation();

                //Physics.IgnoreCollision(coll.collider, this._Collider, true);
                //_OtherColliderStopIgnoring = coll.collider;
            }
            else {
                Debug.Log("Collided, non-blocking "+coll.gameObject.tag, coll.gameObject);   
            }
        }
    }

    public void WasMoved() {
        _HasToBeMoved = false;
    }

    public void Move(int moveIndex)
    {
        transform.DOMove(nodes[moveIndex].position, 2)
  .SetEase(Ease.OutQuint);
    }
}
