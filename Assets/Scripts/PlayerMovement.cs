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
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject playerImage;

    public float rotationSpeed = 100f;
    public bool isGameStart = false;
    private bool canDoubleJump = false;

    public Transform platformCheckPoint;
    public float sideDistance;

    public PhysicsMaterial2D normalM;
    public PhysicsMaterial2D attackM;

    [SerializeField] private float stepHeight = 0.5f;
    [SerializeField] private float skinWidth = 0.02f;

    private bool isGrounded = false;
    private Collider2D _col;
    public bool isAttacking = false;

    public GameObject arrow;
    public Spawner spawner;

    void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // Step-up logic
        float direction = Mathf.Sign(rb.linearVelocity.x);
        Vector2 size = _col.bounds.size;
        Vector2 originLow = (Vector2)transform.position + Vector2.up * skinWidth;
        Vector2 originHigh = originLow + Vector2.up * stepHeight;

        RaycastHit2D hitLow = Physics2D.BoxCast(originLow, size, 0f, Vector2.right * direction,
            Mathf.Abs(rb.linearVelocity.x * Time.fixedDeltaTime), groundLayer);
        RaycastHit2D hitHigh = Physics2D.BoxCast(originHigh, size, 0f, Vector2.right * direction,
            Mathf.Abs(rb.linearVelocity.x * Time.fixedDeltaTime), groundLayer);

        if (hitLow && !hitHigh)
            transform.Translate(Vector2.up * stepHeight, Space.World);
    }

    void Update()
    {
        // Constant forward motion
        if (isGameStart)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }

        // Side-edge correction
        RaycastHit2D sideHit = Physics2D.Raycast(transform.position,
            Vector2.right * transform.localScale.x, sideDistance, groundLayer);
        if (sideHit)
        {
            Vector2 topOrigin = sideHit.point + Vector2.up * 0.8f;
            RaycastHit2D downHit = Physics2D.Raycast(topOrigin, Vector2.down, 1f, groundLayer);
            if (downHit)
                transform.position = new Vector3(transform.position.x,
                    downHit.point.y + 0.5f, transform.position.z);
        }

        // Jump Logic
        if (isGrounded)
        {
            canDoubleJump = true;
            isAttacking = false;
            _col.sharedMaterial = normalM;

            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                FindFirstObjectByType<AudioManager>().Play("Jump");
            }
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.8f);
            canDoubleJump = false;
            FindFirstObjectByType<AudioManager>().Play("Jump");
        }

        // Crouch in Air
        if (Input.GetButton("Crouch") && !isGrounded && !isAttacking)
        {
            rb.AddForce(-Vector2.up * crouchForce, ForceMode2D.Impulse);
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }

        // Attack Spin
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            isAttacking = true;
            _col.sharedMaterial = attackM;
        }

        if (isAttacking)
        {
            playerImage.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            playerImage.transform.rotation = Quaternion.identity;
        }
    }
}
