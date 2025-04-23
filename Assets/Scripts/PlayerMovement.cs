using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float crouchForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private bool isAttacking = false;
    public float rotationSpeed = 100f;
    public bool isGameStart = false;
    [SerializeField] private bool canDoubleJump = false;

    public Transform platformCheckPoint;
    public float sideDistance;

    public PhysicsMaterial2D normalM;
    public PhysicsMaterial2D attackM;

    [SerializeField] private float stepHeight = 0.5f;
    [SerializeField] private float skinWidth = 0.02f;

    private bool isGrounded = false;
    private bool wasGrounded = false;

    void FixedUpdate()
    {
        // Step-up logic
        float direction = Mathf.Sign(rb.linearVelocity.x);
        Vector2 size = GetComponent<Collider2D>().bounds.size;
        Vector2 originLow = (Vector2)transform.position + Vector2.up * skinWidth;
        Vector2 originHigh = originLow + Vector2.up * stepHeight;

        RaycastHit2D hitLow = Physics2D.BoxCast(originLow, size, 0f, Vector2.right * direction, Mathf.Abs(rb.linearVelocity.x * Time.fixedDeltaTime), groundLayer);
        RaycastHit2D hitHigh = Physics2D.BoxCast(originHigh, size, 0f, Vector2.right * direction, Mathf.Abs(rb.linearVelocity.x * Time.fixedDeltaTime), groundLayer);

        if (hitLow && !hitHigh)
        {
            transform.Translate(Vector2.up * stepHeight, Space.World);
        }

        // Landing check: zero vertical velocity on impact
        bool nowGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
        if (nowGrounded && !wasGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
        wasGrounded = nowGrounded;
    }

    void Update()
    {
        // Side-edge correction
        RaycastHit2D sideHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, sideDistance, groundLayer);
        if (sideHit)
        {
            Vector2 topOrigin = sideHit.point + Vector2.up * 0.8f;
            RaycastHit2D downHit = Physics2D.Raycast(topOrigin, Vector2.down, 1f, groundLayer);
            if (downHit)
            {
                transform.position = new Vector3(transform.position.x, downHit.point.y + 0.5f, transform.position.z);
            }
        }

        // Constant forward motion
        if (isGameStart)
        {
            rb.linearVelocity = new Vector2(10f, rb.linearVelocity.y);
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        #region Jump
        if (isGrounded)
        {
            GetComponent<Collider2D>().sharedMaterial = normalM;
            canDoubleJump = true;
            isAttacking = false;
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
        if (Input.GetButton("Crouch") && !isGrounded)
        {
            rb.AddForce(-Vector2.up * crouchForce, ForceMode2D.Impulse);
        }
        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }
        #endregion

        #region AttackingSpin
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            isAttacking = true;
        }
        if (isAttacking)
        {
            GetComponent<Collider2D>().sharedMaterial = attackM;
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        #endregion
    }
}
