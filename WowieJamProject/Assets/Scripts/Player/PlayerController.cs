using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;

public class PlayerController : MonoBehaviour
{

    [Header("Moving")]
    public bool FreezeMovement;
    Rigidbody2D rb2d;
    Vector2 input;
    public int Speed;

    [Header("GroundCheck")]
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float radius;
    [SerializeField] bool PlayerIsGrounded;

    [Header("Jumping")]
    [SerializeField] float CoyoteTime;
    [SerializeField] int JumpForce;
    [SerializeField] float JumpCooldown;
    [SerializeField] GameObject JumpSound;
    [SerializeField] GameObject JumpParticle;
    bool canJump = true;
    public bool PlayerIsJumping;
    public bool PlayerIsLaunching;

    [Header("Animation")]
    SpriteRenderer spriteRenderer;
    [SerializeField] VoidBaseEventReference SpawnPlayerEvent;
    Animator animator;

    [Header("Death")]
    [SerializeField] GameObject DeathTile;
    [SerializeField] GameObject DeathSound;
    bool isDying;
    Vector2 startPos;

    [Header("Doorsystem")]
    [SerializeField] LayerMask DoorLayer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SpawnPlayerEvent.Event.Raise();
        startPos = transform.position;
    }
    #region InputActions
    public void ChangeDirection(InputAction.CallbackContext _input)
    {
        input = _input.ReadValue<Vector2>();

        animator.SetBool("Walk", input.x != 0);

        if(!FreezeMovement)
         spriteRenderer.flipX = input.x < 0;

    }

    public void Jump(InputAction.CallbackContext _input)
    {
        if (!_input.canceled)
            PlayerIsJumping = true;
        else if (_input.canceled)
            PlayerIsJumping = false;
    }

    public void EnterDoor(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, .5f, DoorLayer);
            if (collider)
            {
                Debug.Log("EnterDoor");
                collider.GetComponent<Door>().EnterDoor(transform);
            }
        }

    }

    public void RestartGame(InputAction.CallbackContext _context)
    {
        if (_context.performed)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
    private void FixedUpdate()
    {
        if (FreezeMovement)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        rb2d.velocity = new Vector2(input.x * Speed, rb2d.velocity.y);
        if (PlayerIsJumping && canJump && PlayerIsGrounded)
        {
            canJump = false;
            Invoke("ResetJump", JumpCooldown);
            PerformJump();
        }

        GroundCheck();
    }
    #region Groundcheck
    private void GroundCheck()
    {
        bool grounded = Physics2D.OverlapCircle(feet.position, radius, groundLayer);
        animator.SetBool("Jump", !grounded);

        if (PlayerIsLaunching && grounded)
            PlayerIsLaunching = false;

        if (grounded)
            PlayerIsGrounded = true;
        else
            Invoke("ResetGroundCheck", CoyoteTime);
    }

    void ResetGroundCheck()
    {
        PlayerIsGrounded = Physics2D.OverlapCircle(feet.position, radius, groundLayer);
    }
    #endregion
    #region Jump
    private void PerformJump()
    {
        GameObject sound = Instantiate(JumpSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);

        GameObject particle = Instantiate(JumpParticle, transform.position, Quaternion.identity);
        Destroy(particle, 2f);

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(JumpForce * transform.up);
    }

    private void ResetJump()
    {
        canJump = true;
    }
    #endregion
    #region Death
    public void Die()
    {
        if (isDying) return;

        isDying = true;

        animator.SetTrigger("Death");
        FreezeMovement = true;
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x - .5f) +.5f, transform.position.y);
        Invoke("DestroyPlayer", 1f);

        GameObject sound = Instantiate(DeathSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);
    }

    void DestroyPlayer()
    {
        Instantiate(DeathTile, transform.position, Quaternion.identity, transform.parent);
        transform.position = startPos;
        isDying = false;
        animator.SetTrigger("Alive");
        FreezeMovement = false;
        SpawnPlayerEvent.Event.Raise();
    }
    #endregion
}
