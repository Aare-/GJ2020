
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MouseMovable : MonoBehaviour {

    public float MovementCoefficient;
    
    private bool _Moving = false;

    private Vector3 lastMousePos;

    private Rigidbody body;

    protected void Awake() {
        body = GetComponent<Rigidbody>();
    }

    protected void Update() {

        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.transform == this.transform) {
                _Moving = true;
                lastMousePos = Input.mousePosition;
            }
        } else if(!Input.GetMouseButton(0)) {
            _Moving = false;
        }

        if (_Moving) {
            var moveDelta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            var targetVelocity = (moveDelta.normalized * (moveDelta.magnitude * MovementCoefficient));
            body.velocity = new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.z);
        }
        else {
            body.velocity = Vector3.one * 0.0f;
        }
        
    }
}
