
using System;
using TinyMessenger;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameMode {
        NONE,
        ATTRACT,
        GAME,
        VICTORY
    }
    
    public static GameManager Instance;

    [SerializeField]
    protected CanvasGroup _AttractUI;
    
    [SerializeField] 
    protected CanvasGroup _GameUI;

    private GameMode _Mode = GameMode.NONE;

    public GameMode Mode {
        get => _Mode;
        set {
            _Mode = value;

            switch (_Mode){
                case GameMode.ATTRACT:
                    _AttractUI.gameObject.SetActive(true);
                    _GameUI.gameObject.SetActive(false);
                    break;
                
                case GameMode.GAME:
                    _AttractUI.gameObject.SetActive(false);
                    _GameUI.gameObject.SetActive(true);
                    break;
            }
        }
    }
    
    protected void Awake() {
        Instance = this;
    }

    protected void Start() {
        Mode = GameMode.ATTRACT;
    }

    public void StartGame() {
        Mode = GameMode.GAME;
        
        TinyMessengerHub
            .Instance
            .Publish(Msg.StartGame.Get());
        
        TinyMessengerHub
            .Instance
            .Publish(Msg.PlaySound.Get(SoundController.Sounds.BULB_BREAK));
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
