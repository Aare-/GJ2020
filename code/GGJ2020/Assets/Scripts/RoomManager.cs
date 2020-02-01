using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RoomManager : MonoBehaviour {

    public float AVelocity;
    
    public float RotateSpeed;
    
    public bool IsRotatable;

    private Quaternion _LastRotation; 

    private Rigidbody _Body;

    private bool _ShouldStopRotation = false;

    // Start is called before the first frame update
    void Start() {
        _Body = GetComponent<Rigidbody>();

        IsRotatable = true;
    }

    // Update is called once per frame
    void Update() {

        transform.position = Vector3.zero;

        if (!_ShouldStopRotation) {
            _Body.transform.Rotate(Vector3.up, AVelocity);
        }
    }

    public void BlockRotation() {
        //_ShouldStopRotation = true;
        _Body.angularVelocity = Vector3.zero;
        AVelocity = 0.0f;
        transform.rotation = _LastRotation;
    }

    private void LateUpdate() {
        _LastRotation = transform.rotation;
    }
}
