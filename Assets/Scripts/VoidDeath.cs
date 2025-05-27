using UnityEngine;
using UnityEngine.SceneManagement;
public class VoidDeath : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindFirstObjectByType<AudioManager>().Stop("ThemeSong");
            FindFirstObjectByType<AudioManager>().Play("FallingInTheVoid");
            SceneManager.LoadScene("GameOver");
        }
    }
}
