using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainGame");
        FindFirstObjectByType<AudioManager>().Play("ThemeSong");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
        FindFirstObjectByType<AudioManager>().Play("ShopMusic");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        FindFirstObjectByType<AudioManager>().Play("CreditsMusic");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
            FindFirstObjectByType<AudioManager>().Stop("ThemeSong");
        }
    }
}

