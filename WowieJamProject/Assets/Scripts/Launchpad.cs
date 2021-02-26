using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    
    [SerializeField] float LaunchForce;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Launch");

            Rigidbody2D rb2d = collision.GetComponent<Rigidbody2D>();
            collision.GetComponentInChildren<Animator>().SetTrigger("JumpPad");
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(transform.up * LaunchForce, ForceMode2D.Force) ;
        }
    }

}
