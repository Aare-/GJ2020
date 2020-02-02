
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AttractLoopInterface : MonoBehaviour {
    public void OnStartGameClicked() {
        GameManager.Instance.StartGame();
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            OnStartGameClicked();
        }
    }
}
