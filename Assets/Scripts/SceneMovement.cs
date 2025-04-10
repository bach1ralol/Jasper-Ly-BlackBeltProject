using UnityEngine;

public class SceneMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject player;
    void Update()
    {
        transform.position = new Vector3 (player.transform.position.x+8f , transform.position.y,transform.position.z);
    }
}
