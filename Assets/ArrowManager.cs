using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( player.transform.position.x, transform.position.y, transform.position.z);
        if (player.transform.position.y>=7)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
