
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour {
    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
