using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

<<<<<<< HEAD
{
    [SerializeField] Transform Center;
    [SerializeField] Transform LookPos;
=======
    [SerializeField]
    protected float _RotationSpeed;
    
    [SerializeField] 
    Transform Center;
>>>>>>> d9192ea777d3f021ae69af98d11b2de1fb8fa62a
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
<<<<<<< HEAD
        LookIntoWalls();
=======

        lastMouseXPos = Input.mousePosition.x;
>>>>>>> d9192ea777d3f021ae69af98d11b2de1fb8fa62a
    }


    void CameraRotation()
    {
        currentMouseXPos = Input.mousePosition.x;

        if (Input.GetMouseButton(2)) {
            var delta = currentMouseXPos - lastMouseXPos;
            
            transform.RotateAround(Center.position, Vector3.up, delta * _RotationSpeed);
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

