using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool IsRotatable;
    // Start is called before the first frame update
    void Start()
    {
        IsRotatable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRotatable)
        {
            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(new Vector3(0, -2, 0));
            }
            else if (Input.GetKey(KeyCode.T))
            {
                transform.Rotate(new Vector3(0, 2, 0));
            }
        }
         
    }
}
