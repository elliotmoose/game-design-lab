using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public delegate void GameEvent();
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }
    
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TMPro.TextMeshProUGUI gameOverText;

    public static event GameEvent OnPlayerKilled;

    public AudioMixer audioMixer;
    public UnityEvent onGameReset;


    static bool firstGame = true;
    bool gameOver = false;

    public bool IsGameOver() {
        return gameOver;
    }
    // Start is called before the first frame update
    
    void Awake() {
        if(_instance != null) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        // DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {        
        if(firstGame) {
            firstGame = false;
            // ResetGame();
            onGameReset.Invoke();
        }
    //     // DontDestroyOnLoad(this.gameObject);
    //     if(restartMode) {
    //         StartGame();
    //     }
    //     restartMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void StartGame() {
    //     startScreen.SetActive(false);
    //     //reset score
    //     onGameStart.Invoke();
    // }

    public void ResetGame() {
        gameOver = false;
        gameOverScreen.SetActive(false);
        startScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
        onGameReset.Invoke();
        // GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        // StartGame();
    }

    public void GameOver() {
        gameOver = true;
        gameOverScreen.SetActive(true);
        gameOverText.text = "GAME OVER\nYOU LOSTED"; 
        if(OnPlayerKilled != null) OnPlayerKilled();
    }

    public void Win() {
        gameOver = true;
        gameOverScreen.SetActive(true);    
        gameOverText.text = "YOU WIN"; 
    }
}
