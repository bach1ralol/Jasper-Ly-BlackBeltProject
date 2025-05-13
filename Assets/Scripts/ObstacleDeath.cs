using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleDeath : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<PlayerMovement>().isAttacking){
                Debug.Log("You Lose");
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
