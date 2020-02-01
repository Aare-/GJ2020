using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PositionHoldManager))]
public class RoomManager : MonoBehaviour {

    [SerializeField]
    protected float _RotationSpeed;
    
    [Range(0.0f, 0.5f)]
    public float Rotation;

    private Rigidbody _Body;

    private PositionHoldManager _HoldManager;

    public Rigidbody Body => _Body;

    // Start is called before the first frame update
    void Start() {
        _HoldManager = GetComponent<PositionHoldManager>();
        _Body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.zero;
        
        if (Input.GetKey(KeyCode.Space)) {
            Rotation = _RotationSpeed;
        }
        else {
            Rotation = 0.0f;
        }
        
        if(_HoldManager.CanRotate)
            _Body.transform.Rotate(Vector3.up, Rotation);
    }

    public void BlockRotation() {
        _HoldManager.ResetPositions();
    }
}
