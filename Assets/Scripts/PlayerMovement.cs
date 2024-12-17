using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    public Sprite[] runFrames;
    public Sprite[] idleFrames;
    public Sprite[] jumpFrames;
    public Sprite[] fallFrames;
    public float frameRate = 0.1f;
    public float frameRateIdle = 0.25f;
    public float frameRateJump = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private bool movementEnabled = true;  // Impostato a true per abilitare il movimento di default

    private Sprite[] currentFrames;
    private int currentFrame;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (runFrames.Length == 0 || idleFrames.Length == 0 || jumpFrames.Length == 0 || fallFrames.Length == 0)
        {
            Debug.LogError("Assicurati che tutti i frame siano assegnati nell'Inspector!");
            return;
        }

        currentFrames = idleFrames;
        spriteRenderer.sprite = currentFrames[0];
    }

    void Update()
    {
        if (!movementEnabled) return;  // Se il movimento è disabilitato, esci

        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal * speed, rb.linearVelocity.y);  // Usa rb.velocity invece di linearVelocity
        rb.linearVelocity = movement;

        // Gestione della direzione del personaggio
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Salto solo se a terra
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Imposta la velocità verticale per il salto
            SetJumpingState(true);
            isGrounded = false;
        }

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            SetFallingState(true);
        }

        if (isGrounded && rb.linearVelocity.y == 0)
        {
            SetGroundedState(true);
            SetFallingState(false);
        }

        if (isGrounded && !isJumping)
        {
            SetJumpingState(false);
            SetGroundedState(true);
        }

        Animate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            SetGroundedState(true);
            SetJumpingState(false);
            SetFallingState(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            SetFallingState(true);
        }
    }

    public void EnableMovement()  // Funzione per abilitare il movimento
    {
        movementEnabled = true;
    }

    public void DisableMovement()  // Funzione per disabilitare il movimento
    {
        movementEnabled = false;
    }

    void SetJumpingState(bool isJumping)
    {
        this.isJumping = isJumping;
        if (isJumping)
        {
            SetCurrentFrames(jumpFrames);
        }
    }

    void SetFallingState(bool isFalling)
    {
        this.isFalling = isFalling;
        if (isFalling)
        {
            SetCurrentFrames(fallFrames);
        }
    }

    void SetGroundedState(bool isGrounded)
    {
        if (isGrounded)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > 0)  // Se il personaggio si sta muovendo orizzontalmente
            {
                SetCurrentFrames(runFrames);
            }
            else
            {
                SetCurrentFrames(idleFrames);
            }
        }
    }

    void SetCurrentFrames(Sprite[] frames)
    {
        if (frames == null || frames.Length == 0)  // Controllo per evitare array vuoti
        {
            Debug.LogError("L'array di frame è vuoto o null.");
            return;  // Esce dalla funzione se l'array è vuoto
        }

        if (currentFrames != frames)
        {
            currentFrames = frames;
            currentFrame = 0;
            timer = 0;
            spriteRenderer.sprite = currentFrames[currentFrame];
        }
    }

    void Animate()
    {
        float currentFrameRate = (currentFrames == jumpFrames || currentFrames == fallFrames) ? frameRateJump : (currentFrames == idleFrames ? frameRateIdle : frameRate);
        timer += Time.deltaTime;

        if (timer >= currentFrameRate)
        {
            timer -= currentFrameRate;
            currentFrame++;

            if (currentFrame >= currentFrames.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = currentFrames[currentFrame];
        }
    }
}
