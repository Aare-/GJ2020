using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] public int currentState;
    [SerializeField] GameObject[] walls;
    
    void Update()
    {
        if (currentState == 0)
        {
            walls[1].SetActive(true);
            walls[3].SetActive(true);
            walls[0].SetActive(false);
        }
        else if (currentState == 1)
        {
            walls[0].SetActive(false);
            walls[1].SetActive(false);
        }
        else if (currentState == 2)
        {
            walls[0].SetActive(true);
            walls[2].SetActive(true);
            walls[1].SetActive(false);
        }
        else if (currentState == 3)
        {
            walls[1].SetActive(false);
            walls[2].SetActive(false);
        }
        else if (currentState == 4)
        {
            walls[1].SetActive(true);
            walls[3].SetActive(true);
            walls[2].SetActive(false);
        }
        else if (currentState == 5)
        {
            walls[2].SetActive(false);
            walls[3].SetActive(false);
        }
        else if (currentState == 6)
        {
            walls[0].SetActive(true);
            walls[2].SetActive(true);
            walls[3].SetActive(false);
        }
        else if (currentState == 7)
        {
            walls[0].SetActive(false);
            walls[3].SetActive(false);
        }

    }
}
