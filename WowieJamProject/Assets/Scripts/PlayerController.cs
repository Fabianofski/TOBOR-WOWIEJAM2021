using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Moving")]
    Rigidbody2D rb2d;
    Vector2 input;
    [SerializeField] int Speed;

    [Header("GroundCheck")]
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float radius;
    [SerializeField] bool PlayerIsGrounded;

    [Header("Jumping")]
    [SerializeField] float CoyoteTime;
    [SerializeField] int JumpForce;
    public bool PlayerIsJumping;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void ChangeDirection(InputAction.CallbackContext _input)
    {
        input = _input.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext _input)
    {
        if (_input.performed && PlayerIsGrounded)
        {
            PlayerIsJumping = true;
            PerformJump();
        }
        else if (_input.canceled)
            PlayerIsJumping = false;
    }

    private void PerformJump()
    {
        rb2d.AddForce(JumpForce * transform.up);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(input.x * Speed, rb2d.velocity.y);
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapCircle(feet.position, radius, groundLayer))
            PlayerIsGrounded = true;
        else
            Invoke("ResetGroundCheck", CoyoteTime);
    }

    void ResetGroundCheck()
    {
        PlayerIsGrounded = Physics2D.OverlapCircle(feet.position, radius, groundLayer);
    }
}
