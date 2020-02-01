using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControllPoint : MonoBehaviour
{
    [SerializeField] int tag;

    WallManager wallManager;
    // Start is called before the first frame update
    private void Start()
    {
        wallManager = FindObjectOfType<WallManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        wallManager.currentState = tag;
    }
}
