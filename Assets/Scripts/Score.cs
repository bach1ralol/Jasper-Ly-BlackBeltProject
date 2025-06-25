using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
public class Score : MonoBehaviour
{


    public int score;
    private float timeElapsed = 0f;
    private bool timerIsRunning = true;
    public DestroyStart destroyStart;
    public TMP_Text text;

    private int highScore = 0;
    public Text highScoreText;
    private bool beatHighScore;

    private void Update()
    {
        if (timerIsRunning && destroyStart == null)
        {
            timeElapsed += Time.deltaTime;
            score =  (int)timeElapsed;
            text.text = ((int)timeElapsed).ToString();
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}

