using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PositionHoldManager))]
public class RoomManager : MonoBehaviour {

    [Range(-0.5f, 0.5f)]
    public float Rotation;

    private Rigidbody _Body;

    private bool _ShouldStopRotation = false;

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

        if (!_ShouldStopRotation) {
            _Body.transform.Rotate(Vector3.up, Rotation);
        }
    }

    public void BlockRotation() {
        _HoldManager.ResetPositions();
    }
}
