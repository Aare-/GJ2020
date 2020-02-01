﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField] Transform Center;
    [SerializeField] Transform LookPos;
        
    [SerializeField]
    protected float _RotationSpeed;

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
        
        lastMouseXPos = Input.mousePosition.x;
    }


    void CameraRotation()
    {
        currentMouseXPos = Input.mousePosition.x;

        if (Input.GetMouseButton(2)) {
            var delta = currentMouseXPos - lastMouseXPos;
            
            Debug.Log("Delta: "+delta);
            
            transform.RotateAround(Center.position, Vector3.up, delta * _RotationSpeed);
        }
    }

    void LookIntoWalls() {
        if (tempGameObject == null) return;
        
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

