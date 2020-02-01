﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    [SerializeField] Transform Center;
    [SerializeField] Vector3 offset;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(new Vector3(0, 0, .5f));
        }
        else if (Input.GetKey(KeyCode.S))
        {
           // transform.Translate(new Vector3(0, 0, -.5f));
        }
        else if (Input.GetKey(KeyCode.A))
        {
           // transform.Translate(new Vector3(-.5f, 0, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
           // transform.Translate(new Vector3(.5f, 0, 0));
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            // transform.Rotate(new Vector3(0, -5, 0));
            transform.RotateAround(Center.position, Vector3.up,-5);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(Center.position, Vector3.up, 5);

        }
        transform.LookAt(Vector3.zero);
    }
}
