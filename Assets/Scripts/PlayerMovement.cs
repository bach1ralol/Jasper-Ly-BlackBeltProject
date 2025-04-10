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

    private void Update()
    {

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Stop vertical velocity on collision
        }*/

        /*
        if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Stop vertical velocity on collision
        }*/
    }
}
