using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;      // скорость рывка
    [SerializeField] private float dashDuration = 0.2f;  // длительность рывка
    private Rigidbody2D rb;
    public float moveSpeed;
    public float jump;
    public bool CanJump;          // true, когда на земле
    public int maxJumps = 2;
    private GroundCheck groundCheck;
    public int currentJumps = 0;

    private int facingDirection = 1;
    private bool isDashing = false;
    private float dashTimeLeft;

    // НОВОЕ: можно ли ещё раз дэшнуться в воздухе
    private bool hasAirDash = false;
    private bool wasGroundedLastFrame = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CanJump = true;
        moveSpeed = 3f;

        wasGroundedLastFrame = CanJump;
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // Если НЕ рывок — обычное движение
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        }

        // Обновляем направление взгляда
        if (move > 0) facingDirection = 1;
        if (move < 0) facingDirection = -1;

        // Прыжки (двойной прыжок)
        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            currentJumps++;
        }

        // --- ЛОГИКА РАЗРЕШЕНИЯ ВОЗДУШНОГО РЫВКА ---

        // Если в прошлом кадре были на земле, а сейчас уже нет — только что оторвались
        if (wasGroundedLastFrame && !CanJump)
        {
            // даём один воздушный рывок на этот "прыжок"
            hasAirDash = true;
        }

        // Запоминаем состояние на этот кадр
        wasGroundedLastFrame = CanJump;

        // --- КНОПКА РЫВКА ---

        bool canDashNow =
    (CanJump)
    || (!CanJump && hasAirDash);

    if (Input.GetKeyDown(KeyCode.LeftShift)
        && !isDashing
        && canDashNow)
    {
        if (!CanJump && hasAirDash)
        {
            hasAirDash = false;
        }

        StartDash();
    }

        // Логика самого рывка
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;

            rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0f); // рывок по направлению взгляда

            if (dashTimeLeft <= 0f)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;

        // Если хочешь "скользящий" рывок без падения:
        rb.gravityScale = 0f;
    }

    void EndDash()
    {
        isDashing = false;
        rb.gravityScale = 1f;
    }

    public void SetGrounded(bool value)
    {
        CanJump = value;

        if (CanJump)
        {
            currentJumps = 0; 
            hasAirDash = false; 
        }
    }
}