using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectBehaviour : MonoBehaviour
{
    RoomManager roomManager;
    // Start is called before the first frame update
    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag=="Object")
        {
            Debug.Log("Collision");
            roomManager.IsRotatable = false;
        }
    }
}
