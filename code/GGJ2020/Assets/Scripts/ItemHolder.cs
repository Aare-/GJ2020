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
    void Update()
    {
        ChooseObject();
        if (selectedObject != null)
        {
            Debug.Log(selectedObject.name);
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

            if (Physics.Raycast(ray, out hit))
            {
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

        if (Input.GetMouseButtonUp(0))
        {
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

        mouseDeltaPos = mouseDeltaPos * _ObjectMoveSpeed;

        currentMousePossition = Input.mousePosition.y;

        var movVector = new Vector2(mouseDeltaPos.x, mouseDeltaPos.y);
        movVector = movVector.Rotate(-Camera.main.transform.rotation.eulerAngles.y);
        
        var body = selectedObject.GetComponent<Rigidbody>();

        if (selectedObject.GetComponent<StaticObjectBehaviour>().YAxis)
        {
            body.transform.position += new Vector3(0, movVector.x, 0);
        }
        else if (selectedObject.GetComponent<StaticObjectBehaviour>().XAxis)
        {
            body.transform.position += new Vector3(movVector.x, 0, movVector.y);
        }
        else if(currentMousePossition-lastMouseYPos<0)
        {
            lastMouseYPos = currentMousePossition;
            currentPos++;
            selectedObject.GetComponent<StaticObjectBehaviour>().Move(currentPos);

        }
        else if (currentMousePossition - lastMouseYPos > 0)
        {
            lastMouseYPos = currentMousePossition;
            currentPos--;
            selectedObject.GetComponent<StaticObjectBehaviour>().Move(currentPos);

        }
        //body.transform.position += new Vector3(movVector.x, 0, movVector.y);
    }
    
    void MoveObject() {
        //selectedObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x, selectedObject.transform.position.y, Input.mousePosition.z));
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            if (selectedObject.tag=="Static")
            {
                //selectedObject.transform.position = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                if (selectedObject.GetComponent<StaticObjectBehaviour>().XAxis)
                {
                    Debug.Log("On X movement");
                    selectedObject.transform.position = new Vector3(hit.point.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
                }
                else if (selectedObject.GetComponent<StaticObjectBehaviour>().YAxis)
                {
                    Debug.Log("On Y movement");

                    selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, hit.point.y, selectedObject.transform.position.z);
                }
                else if (selectedObject.GetComponent<StaticObjectBehaviour>().ZAxis)
                {
                    Debug.Log("On Z movement");

                    selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, hit.point.z);
                }

                
            }

            
            if (selectedObject.tag=="Trigger" && Mathf.Abs(currentMouseYPos - lastMouseYPos)>0.1f )
            {
                Debug.Log("trigger object");
                

                if (currentMouseYPos-lastMouseYPos>0)
                {
                    selectedObject.transform.RotateAround(selectedObject.transform.parent.GetChild(0).position, Vector3.up, -5);
                }
                else if (currentMouseYPos-lastMouseYPos<0)
                {
                    selectedObject.transform.RotateAround(selectedObject.transform.parent.GetChild(0).position, Vector3.up, 5);
                }
                lastMouseYPos = currentMouseYPos;
            }
            
        }
    }
    
}
