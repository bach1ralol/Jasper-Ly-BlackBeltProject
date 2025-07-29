using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;
    public PlayerMovement gameStarts;

    [SerializeField]
    private Renderer bgRenderer;

    void Update()
    {
        if (gameStarts.isGameStart == true)
        {
            bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
    }
}
