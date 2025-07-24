using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region SingleTon

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion 

    public float currentScore = 0f;

    public bool isPlaying = false;

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime; 
        }
    }

    public void GameOver()
    {
        currentScore = 0;
    }
    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }


}
