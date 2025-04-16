using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float CrouchForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;
    private bool isGrounded = false;
    private bool isJumping = false;
    public bool isGameStart=false;
    [SerializeField] private bool canDoubleJump = false; // Variable to track if double jump is available

    public Transform platformCheckPoint;
    public float sideDistance;
    private void Update()
    {
        RaycastHit2D sideHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, sideDistance, groundLayer);
        if (sideHit)
        {
            Debug.Log("Hit");
            Vector2 topOrigin = new Vector2(sideHit.point.x, sideHit.point.y + 0.8f);
            Debug.DrawRay(topOrigin, Vector2.down * 1f, Color.green, 1f);  // visualize it
            RaycastHit2D downHit = Physics2D.Raycast(topOrigin, Vector2.down, 1f, groundLayer);
            if (downHit)
            {
                Debug.Log("Move player up");
                transform.position = new Vector3(transform.position.x, downHit.point.y+0.5f, transform.position.z);
            }
        }
        if (isGameStart)
        {
            rb.linearVelocity = new Vector2( 10, rb.linearVelocityY);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
        Debug.DrawRay(feetPos.position, -Vector2.up*0.1f, Color.red);
        #region Jump

        if (isGrounded)
        {
            canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce * 0.8f, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

        #endregion

        #region Crouch

        if (Input.GetButton("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
            rb.AddForce(-rb.transform.up * CrouchForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }

        #endregion
    }
}
