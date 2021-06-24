using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public delegate void GameEvent();
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject scoreText;
    public TMPro.TextMeshProUGUI gameOverText;

    public static event GameEvent OnPlayerKilled;
    public static event GameEvent OnEnemyKilled;
    public static event GameEvent OnIncreaseScore;

    public AudioMixer audioMixer;


    static bool restartMode = true;
    public bool gameOver = true;
    // Start is called before the first frame update
    
    void Awake() {
        Instance = this;
    }

    void Start()
    {        
        // DontDestroyOnLoad(this.gameObject);
        if(restartMode) {
            StartGame();
        }
        restartMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = $"score: {score}";
    }

    public void StartGame() {
        Debug.Log($"started with {restartMode}");
        startScreen.SetActive(false);
        gameOver = false;
    }
    
    public void ResetGame() {
        gameOverScreen.SetActive(false);
        startScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void EnemyKilled() {
        if(GameManager.OnEnemyKilled!=null) GameManager.OnEnemyKilled.Invoke();
    }
    
    public void IncreaseScore() {
        score += 1;
        if(GameManager.OnIncreaseScore!=null) GameManager.OnIncreaseScore.Invoke();
    }

}
