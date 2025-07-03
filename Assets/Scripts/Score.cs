using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    private float timeElapsed = 0f;
    private bool timerIsRunning = true;

    public DestroyStart destroyStart;
    public TMP_Text text;
    public TMP_Text highScoreText;

    private int highScore = 0;
    private bool beatHighScore = false;

    private void Start()
    {
        // Load and display saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void Update()
    {
        
        if (timerIsRunning && destroyStart == null)
        {
            timeElapsed += Time.deltaTime;
            score = (int)timeElapsed;
            text.text = score.ToString();

            
        if (score > highScore)
        {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
                highScoreText.text = "High Score: " + highScore.ToString();
        }
        }
    }
}
