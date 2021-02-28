using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] Door[] doors;
    [SerializeField] Sprite PressedSprite;
    [SerializeField] Sprite NotPressedSprite;
    [SerializeField] GameObject ButtonClickSound;
    [SerializeField] GameObject ButtonParticle;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) return;

        foreach (Door door in doors)
            door.OpenDoor(true);
        spriteRenderer.sprite = PressedSprite;

        GameObject sound = Instantiate(ButtonClickSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);

        GameObject particle = Instantiate(ButtonParticle, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) return;

        foreach (Door door in doors)
            door.OpenDoor(false);
        spriteRenderer.sprite = NotPressedSprite;

        GameObject sound = Instantiate(ButtonClickSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);
    }
}
