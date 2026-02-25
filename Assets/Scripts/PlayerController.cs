using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer; 

    private float pressStartTime;
    private bool isGrounded = true;
    private float lastX; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        lastX = transform.position.x;
    }

    private void OnEnable()
    {
        playerInput.actions["Jump"].started += OnPressStart;
        playerInput.actions["Jump"].canceled += OnPressEnd;
    }

    private void OnDisable()
    {
        playerInput.actions["Jump"].started -= OnPressStart;
        playerInput.actions["Jump"].canceled -= OnPressEnd;
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * settings.moveSpeed, 6f) - 3f;
        transform.position = new Vector3(x, transform.position.y, 0);

        HandleFlip(x);
    }

    private void HandleFlip(float currentX)
    {
        if (currentX > lastX + 0.001f)
        {
            spriteRenderer.flipX = false;
        }

        else if (currentX < lastX - 0.001f)
        {
            spriteRenderer.flipX = true;
        }
        lastX = currentX; 
    }

    private void OnPressStart(InputAction.CallbackContext context)
    {
        pressStartTime = (float)context.startTime;
    }

    private void OnPressEnd(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        float duration = (float)context.time - pressStartTime;
        bool isBigJump = duration >= settings.longPressThreshold;
        float force = isBigJump ? settings.bigJumpForce : settings.smallJumpForce;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {   
            isGrounded = true;
        }
    }
}