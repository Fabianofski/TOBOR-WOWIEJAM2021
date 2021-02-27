using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    bool canJump = true;
    public bool PlayerIsJumping;
    public bool PlayerIsLaunching;

    [Header("Animation")]
    SpriteRenderer spriteRenderer;
    Animator animator;

    [Header("Death")]
    [SerializeField] GameObject DeathTile;
    bool isDying;
    Vector2 startPos;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        startPos = transform.position;
    }

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

    private void ResetJump()
    {
        canJump = true;
    }

    private void PerformJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(JumpForce * transform.up);
    }

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

    public void Die()
    {
        if (isDying) return;

        isDying = true;

        animator.SetTrigger("Death");
        FreezeMovement = true;
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x - .5f) +.5f, transform.position.y);
        Invoke("DestroyPlayer", 1f);
    }

    void DestroyPlayer()
    {
        Instantiate(DeathTile, transform.position, Quaternion.identity, transform.parent);
        transform.position = startPos;
        isDying = false;
        animator.SetTrigger("Alive");
        FreezeMovement = false;
    }
}
