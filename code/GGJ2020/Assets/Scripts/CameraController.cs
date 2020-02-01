using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    [SerializeField] Transform Center;
    [SerializeField] Transform LookPos;
    // [SerializeField] Vector3 offset;
    Vector3 dir;
    float currentMouseXPos;
    float lastMouseXPos;

    GameObject tempGameObject;



    // Update is called once per frame
    void Update()
    {
        dir = LookPos.position - transform.position;
        transform.LookAt(Vector3.zero);

        CameraRotation();
        LookIntoWalls();
    }


    void CameraRotation()
    {
        currentMouseXPos = Input.mousePosition.x;
        if (Input.GetMouseButtonDown(2))
        {
           // currentMouseXPos = Input.mousePosition.x;
            lastMouseXPos = currentMouseXPos;
            Debug.Log("rotate");
        }

       else if (Input.GetMouseButton(2))
        {
            
            if (Mathf.Abs(currentMouseXPos - lastMouseXPos) > 0.1f)
            {
                Debug.Log("rotating");
                if (currentMouseXPos - lastMouseXPos > 0)
                {
                    transform.RotateAround(Center.position, Vector3.up, -5);
                    Debug.Log("rotating");
                }
                else if (currentMouseXPos - lastMouseXPos < 0)
                {
                    transform.RotateAround(Center.position, Vector3.up, 5);
                }
                lastMouseXPos = currentMouseXPos;
            }

        }

    }

    void LookIntoWalls()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                tempGameObject = hit.collider.gameObject;

            }
            else
            {
                tempGameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
           
    }
}

