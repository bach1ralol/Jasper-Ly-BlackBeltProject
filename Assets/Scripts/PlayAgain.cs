using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("MainGame");
        FindFirstObjectByType<AudioManager>().Play("ThemeSong");
    }

}

