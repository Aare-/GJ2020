using System;
using UnityEngine;
using TinyMessenger;


[RequireComponent(typeof(Rigidbody))]
public class StaticObjectBehaviour : MonoBehaviour {

    [Header("Config")] 
    public Sprite Icon;

    [Header("Movement Path")]
    public MovPath Path;

    [Range(0, 1.0f)]
    public float MovePercent;
    
    private RoomManager _RoomManager;
    
    private PositionHoldManager _PHolderManager;

    private Rigidbody _Body;

    private Collider _Collider;

    private Collider _OtherColliderStopIgnoring;

    private bool _HasToBeMoved = false;

    private Collider[] Colliders = new Collider[]{null, null, null, null};
    
    public Rigidbody Body => _Body;

    private float _OldPercent;

    public bool HasToBeMoved => _HasToBeMoved;

    // Start is called before the first frame update
    void Start() {
        _RoomManager = FindObjectOfType<RoomManager>();
        _PHolderManager = FindObjectOfType<PositionHoldManager>();
        _Body = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
        
        _PHolderManager.RegisterStaticObject(this);

        if (Path != null) {
            transform.position = Path.Start.transform.position;
        }
        
        HidePath();
    }

    // Update is called once per frame
    void Update() {
        if (_OtherColliderStopIgnoring != null) {
            Physics.IgnoreCollision(_OtherColliderStopIgnoring, _Collider, false);
            _OtherColliderStopIgnoring = null;
        }

        if (Path != null){
            var percDistance = _OldPercent - MovePercent;
            var t = 0.0f;
            var reachedPos = Body.transform.position;

            if (Mathf.Abs(percDistance) > 0.0f){
                bool madeMove = false;

                while (t < 1.0f) {

                    //0.05f / Math.Abs(percDistance);
                    t = Mathf.Clamp01(t + 0.1f / Math.Abs(percDistance));
                    var iterStep = Mathf.Lerp(_OldPercent, MovePercent, t);

                    var newPosition =
                        Vector3.Lerp(Path.Start.position, Path.End.position, iterStep);

                    if (CanMoveToNewPosition(newPosition)){
                        reachedPos = newPosition;
                        madeMove = true;
                    }
                    else{
                        break;
                    }
                }

                if (madeMove) {
                    Body
                        .transform
                        .position = reachedPos;

                    WasMoved();   
                }
            }
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

    public void ShowPath() {
        if (Path == null) return;
        
        Path.gameObject.SetActive(true);
    }

    public void HidePath() {
        if (Path == null) return;
        
        Path.gameObject.SetActive(false);
    }

    public bool CanMoveToNewPosition(Vector3 newPos) {
        var collider = GetComponent<Collider>();
        var bounds = collider.bounds;
        var extends = new Vector3(
            bounds.extents.x, 
            bounds.extents.y * 0.5f, 
            bounds.extents.z);
        
        var size = Physics.OverlapBoxNonAlloc(
            newPos, 
            extends, 
            Colliders, 
            Body.gameObject.transform.rotation);

        if (size > 0){
            for (var i = 0; i < size; i++){
                var collision = Colliders[i];
                
                // ignore self
                if (collision.gameObject == Body.gameObject){
                    continue;
                }

                return false;   
            }
        }

        return true;
    }
}
