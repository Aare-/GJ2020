using System;
using TinyMessenger;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PositionHoldManager))]
public class RoomManager : MonoBehaviour {

    [SerializeField] protected float _InitialRotation = 0.0f;
    
    [SerializeField]
    protected float _RotationSpeed;
    
    [Range(0.0f, 0.5f)]
    public float Rotation;

    private Rigidbody _Body;

    private PositionHoldManager _HoldManager;

    private float _AlreadyRotated = 0.0f;

    public Rigidbody Body => _Body;

    public float AlreadyRotated => _AlreadyRotated;

    // Start is called before the first frame update
    void Start() {
        _HoldManager = GetComponent<PositionHoldManager>();
        _Body = GetComponent<Rigidbody>();
        
        _Body.transform.rotation = Quaternion.Euler(0, _InitialRotation, 0);
        
        TinyMessengerHub
            .Instance
            .Publish(Msg.RotationProgress.Get(0.0f));
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.zero;

        switch (GameManager.Instance.Mode){
            case GameManager.GameMode.GAME:
                
                Rotation = Input.GetKey(KeyCode.Space) ? 
                    _RotationSpeed : 
                    0.0f;

                if (_HoldManager.CanRotate && ItemHolder.selectedObject == null){
                    _Body.transform.Rotate(Vector3.up, Rotation);
                }

                var rot = _Body.transform.localEulerAngles.y;
                while (rot < 0)
                    rot += 360;
                rot %= 360;

                _AlreadyRotated = rot;
                
                TinyMessengerHub
                    .Instance
                    .Publish(Msg.RotationProgress.Get(rot / 360.0f));  

                break;
        }
    }

    public void BlockRotation() {
        _HoldManager.ResetPositions();

        TinyMessengerHub
            .Instance
            .Publish(Msg.PlaySound.Get(SoundController.Sounds.COLLIDER_HIT));
    }
}
