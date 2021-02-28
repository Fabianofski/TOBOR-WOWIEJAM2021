using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] Door[] doors;
    [SerializeField] Sprite PressedSprite;
    [SerializeField] Sprite NotPressedSprite;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Door door in doors)
            door.OpenDoor(true);
        spriteRenderer.sprite = PressedSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (Door door in doors)
            door.OpenDoor(false);
        spriteRenderer.sprite = NotPressedSprite;
    }
}
