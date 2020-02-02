using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class ItemHolder : MonoBehaviour {
    [SerializeField]
    protected float _ObjectMoveSpeed;

    float currentMouseYPos;
    float lastMouseYPos;
    float currentMousePossition;

    int currentPos;

    bool IsHolding;
    GameObject tempObject;
    GameObject selectedObject;

    public LayerMask hitLayers;
    public LayerMask wallMask;

    Vector3 FirstClick;

    Vector2 currentMousePos;
    Vector2 lastMousePos;

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
            //MoveObject();
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

                    if (Input.GetMouseButtonDown(0))
                    {
                        FirstClick = Input.mousePosition;
                        selectedObject = tempObject;
                        IsHolding = true;
                        Cursor.visible = false;

                        selectedObject.GetComponent<StaticObjectBehaviour>().WasMoved();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (selectedObject != null) {
                var body = selectedObject.GetComponent<Rigidbody>();
                body.angularVelocity = Vector2.zero;
            }

            selectedObject = null;
            IsHolding = false;
            Cursor.visible = true;
        }
    }

    void PhysicMoveObject() {
        var mouseDeltaPos = (Vector2)Input.mousePosition - lastMousePos;
        var body = selectedObject.GetComponent<Rigidbody>();
        var collider = selectedObject.GetComponent<Collider>();
        
        mouseDeltaPos = mouseDeltaPos * _ObjectMoveSpeed;

        currentMousePossition = Input.mousePosition.y;

        //adjust movement for camera 
        var movVector = new Vector2(mouseDeltaPos.x, mouseDeltaPos.y);
        movVector = movVector.Rotate(-Camera.main.transform.rotation.eulerAngles.y);
        
        var movDif = new Vector3(movVector.x, 0, movVector.y);
        var newPosition = body.transform.position + movDif;
        var extends = collider.bounds.extents;//new Vector3(collider.bounds.extents.x, 0.1f, collider.bounds.extents.z);
        
        Debug.Log("EXT: "+extends);
        
        // check if new position would collide, if so dont move there
        if (!Physics.BoxCast(
                newPosition,
                extends,
                movDif.normalized,
                body.gameObject.transform.rotation,
                movDif.magnitude,
                wallMask)) {
            Debug.Log("BOX CAST PASSED");
            body.transform.position = newPosition;
        }
        else{
            Debug.Log("BLOCK!");
        }

    }
}
