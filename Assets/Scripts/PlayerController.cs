using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public int coin;
    // For health
    public int health = 100;
    public Image healthImage;

    // Public variables appear in the Inspector, so you can tweak them without editing code.
    public float moveSpeed = 4f;       // How fast the player moves left/right

    //Jump realated variables for the Jump Feature (later)
    public float jumpForce = 8f;      // How strong the jump is (vertical speed)
    public Transform groundCheck;      // Empty child object placed at the player's feet
    public float groundCheckRadius = 0.2f; // Size of the circle used to detect ground
    public LayerMask groundLayer;      // Which layer counts as "ground" (set in Inspector)

    //public variables for audio clips
    public AudioClip jumpClip;
    public AudioClip HurtClip;

    // Private variables are used internally by the script.
    private Rigidbody2D rb;            // Reference to the Rigidbody2D component
    private bool isGrounded;           // True if player is standing on ground

    private SpriteRenderer spriteRenderer;

    //private variables to refernce the animator when doing animations
    private Animator animator;

    //audio variable for the player

    private AudioSource audioSource;
    //double jump script
    public int extraJumpsValue = 1;
    private int extraJumps;

    void Start()
    {
        // Grab the Rigidbody2D attached to the Player object once at the start.
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        //for the animator
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        //for the audio
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // --- Horizontal movement ---
        // Get input from keyboard (A/D or Left/Right arrows).
        float moveInput = Input.GetAxis("Horizontal");
        // Apply horizontal speed while keeping the current vertical velocity.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //double jump for episode 3
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        //modified for episode 3
        // Jump for episode 1
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                PlaySFX(jumpClip);
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                PlaySFX(jumpClip);
            }
        }

        /* Jump realated code for the Jump Feature (later)
        // --- Ground check ---
        // Create an invisible circle at the GroundCheck position.
        // If this circle overlaps any collider on the "Ground" layer, player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Jump ---
        // If player is grounded AND the Jump button (Spacebar by default) is pressed:
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Set vertical velocity to jumpForce (launch upward).
            // Horizontal velocity stays the same.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        */

        // For health
        healthImage.fillAmount = health / 100f;


        //for the animations
        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            PlaySFX(HurtClip, 0.4f);
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
        else if (collision.gameObject.tag == "BounchPad")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 2);
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //for the animations
    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_idle");
            }
            else
            {
                animator.Play("Player_run");
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("Player_jump");
            }
            else
            {
                animator.Play("Player_fall");
            }
        }

    }
    public void PlaySFX(AudioClip audioClip, float volume = 1f)
    {
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
