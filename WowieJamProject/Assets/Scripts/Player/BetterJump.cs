using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetterJump : MonoBehaviour
{

    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    PlayerController playerController;
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (rb2d.velocity.y < 0)
            rb2d.velocity += vel(fallMultiplier);
        else if (rb2d.velocity.y > 0 && !playerController.PlayerIsJumping && !playerController.PlayerIsLaunching)
            rb2d.velocity += vel(lowJumpMultiplier);
    }

    Vector2 vel(float x)
    {
        return Vector2.up * Physics2D.gravity.y * (x - 1) * Time.fixedDeltaTime;
    }
}