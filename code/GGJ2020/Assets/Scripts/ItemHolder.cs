using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    bool IsHolding;
    GameObject tempObject;
    GameObject selectedObject;
    public LayerMask hitLayers;

    Vector3 FirstClick;

    float currentMouseYPos;
    float lastMouseYPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChooseObject();
        if (selectedObject != null)
        {
            Debug.Log(selectedObject.name);
            MoveObject();
        }
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
                        currentMouseYPos = FirstClick.y;
                        lastMouseYPos = currentMouseYPos;
                        selectedObject = tempObject;
                        IsHolding = true;
                        Cursor.visible = false;
                    }
                }
            }
           

        }
        currentMouseYPos = Input.mousePosition.y;
        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
            IsHolding = false;
            Cursor.visible = true;
        }
    }

    void MoveObject()
    {
        //selectedObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x, selectedObject.transform.position.y, Input.mousePosition.z));
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            if (selectedObject.tag=="Static")
            {
                selectedObject.transform.position = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);

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
