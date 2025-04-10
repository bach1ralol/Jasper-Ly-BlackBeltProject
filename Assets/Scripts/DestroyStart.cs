using UnityEngine;

public class DestroyStart : MonoBehaviour
{
    public PlayerMovement playerMovement;
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.isGameStart = true;
            Destroy(gameObject);
        }
    }
}
