using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public IntVariable score;
    public GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = $"score: {score.Value}";
    }

    public void ResetScore() {
        score.SetValue(0);
    }

    public void IncrementScore() {
        score.ApplyChange(1);
    }
}
