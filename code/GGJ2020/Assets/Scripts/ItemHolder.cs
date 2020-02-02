using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class ItemHolder : MonoBehaviour {
    [SerializeField]
    protected float _ObjectMoveSpeed;

    [SerializeField] protected float _MaxMovementSpeed = 5.0f;
    
    float currentMouseYPos;
    float lastMouseYPos;
    float currentMousePossition;

    int currentPos;

    bool IsHolding;
    GameObject tempObject;
    
    public  static StaticObjectBehaviour selectedObject;

    public LayerMask hitLayers;
    public LayerMask wallMask;

    Vector3 FirstClick;

    Vector2 currentMousePos;
    Vector2 lastMousePos;
    Collider[] colliders = new Collider[]{null, null, null, null};

    // Start is called before the first frame update
    void Start()
    {
        currentMouseYPos = Input.mousePosition.y;
        lastMouseYPos = currentMouseYPos;
        currentPos = 0;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.Mode != GameManager.GameMode.GAME) return;
        
        ChooseObject();
        if (selectedObject != null) {
            PhysicMoveObject();
        }
        
        currentMouseYPos = Input.mousePosition.y;
        lastMousePos = Input.mousePosition;
    }

    void ChooseObject()
    {
        if (!IsHolding)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers)) {
                if (hit.rigidbody != null && hit.collider.gameObject.layer==9)
                {
                    tempObject = hit.collider.gameObject;

                    if (Input.GetMouseButtonDown(0)) {
                        FirstClick = Input.mousePosition;
                        selectedObject = tempObject.GetComponent<StaticObjectBehaviour>();
                        IsHolding = true;
                        Cursor.visible = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (selectedObject != null) {
                selectedObject.Body.angularVelocity = Vector2.zero;
            }

            selectedObject = null;
            IsHolding = false;
            Cursor.visible = true;
        }
    }

    void PhysicMoveObject() {
        var mouseDeltaPos = (Vector2)Input.mousePosition - lastMousePos;
        var collider = selectedObject.GetComponent<Collider>();
        
        mouseDeltaPos = mouseDeltaPos * _ObjectMoveSpeed;

        mouseDeltaPos = mouseDeltaPos.normalized * Mathf.Min(
            _MaxMovementSpeed, 
            mouseDeltaPos.magnitude); 

        currentMousePossition = Input.mousePosition.y;
        
        //adjust movement for camera 
        var movVector = new Vector2(mouseDeltaPos.x, mouseDeltaPos.y);
        movVector = movVector.Rotate(-Camera.main.transform.rotation.eulerAngles.y);

        if (movVector.magnitude <= 0.0f){
            return;
        }
        
        var movDif = new Vector3(movVector.x, 0, movVector.y);
        var newPosition = selectedObject.Body.transform.position + movDif;
        var bounds = collider.bounds;
        var extends = new Vector3(
            bounds.extents.x, 
            bounds.extents.y * 0.5f, 
            bounds.extents.z);

        var size = Physics.OverlapBoxNonAlloc(
            newPosition, 
            extends, 
            colliders, 
            selectedObject.Body.gameObject.transform.rotation);

        if (size > 0){
            for (var i = 0; i < size; i++){
                var collision = colliders[i];
                
                // ignore self
                if (collision.gameObject == selectedObject.Body.gameObject){
                    continue;
                }

                return;   
            }
        }

        selectedObject
            .Body
            .transform
            .position = newPosition;

        selectedObject.WasMoved();
    }
}
