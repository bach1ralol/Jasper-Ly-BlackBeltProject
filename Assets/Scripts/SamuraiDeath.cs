using UnityEngine;
using UnityEngine.SceneManagement;

public class SamuraiDeath : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().isAttacking)
            {
                collision.gameObject.GetComponent<PlayerMovement>().isAttacking = false;
                Destroy(gameObject);
            }
            else {
                Debug.Log("You Lose");
                SceneManager.LoadScene("GameOver");
                FindFirstObjectByType<AudioManager>().Play("SamuraiSlash");
            }
        }
    }
}
