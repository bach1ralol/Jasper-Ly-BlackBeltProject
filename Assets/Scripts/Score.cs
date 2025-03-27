using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private float timeElapsed = 0f;
    private bool timerIsRunning = true;
    public DestroyStart destroyStart;
    public TMP_Text text;

    private void Update()
    {
        if (timerIsRunning && destroyStart == null)
        {
            timeElapsed += Time.deltaTime;
            text.text = ((int)timeElapsed).ToString();
        }
    }
}

