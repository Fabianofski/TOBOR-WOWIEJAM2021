using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    
    [SerializeField] float LaunchForce;
    [SerializeField] GameObject LaunchSound;
    [SerializeField] GameObject LaunchParticle;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Launch(collision);
    }

    private void Launch(Collider2D _collision)
    {
        animator.SetTrigger("Launch");

        Rigidbody2D _rb2d = _collision.GetComponent<Rigidbody2D>();
        PlayerController _playerController = _collision.GetComponent<PlayerController>();

        if (_playerController)
        {
            // Launch Player
            _collision.GetComponentInChildren<Animator>().SetTrigger("JumpPad");
            _playerController.PlayerIsLaunching = true;
        }
        else
        {
            // Launch Playerbox
            _collision.transform.parent = null;
            Vector3 pos = transform.position + transform.up;
            _collision.transform.position = pos;
            _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Add Force
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
        _rb2d.AddForce(transform.up * LaunchForce, ForceMode2D.Impulse);

        // Play Sound
        GameObject sound = Instantiate(LaunchSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);

        // Instantiate Particle
        GameObject particle = Instantiate(LaunchParticle, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }
}
