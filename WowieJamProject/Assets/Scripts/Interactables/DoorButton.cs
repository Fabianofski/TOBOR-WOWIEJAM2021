using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class DoorButton : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite PressedSprite;
    [SerializeField] Sprite NotPressedSprite;
    SpriteRenderer spriteRenderer;

    [Header("Collisions")]
    [SerializeField] Vector2 ButtonPressedSize;
    [SerializeField] Vector2 ButtonPressedPos;
    [SerializeField] BoolVariableInstancer ButtonPressedVariable;

    [Header("Polish")]
    [SerializeField] GameObject ButtonClickSound;
    [SerializeField] GameObject ButtonParticle;

    private void FixedUpdate()
    {
        ButtonPressedVariable.Value = CheckforCollision();
    }

    bool CheckforCollision()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.TransformPoint(ButtonPressedPos), ButtonPressedSize, 0);
        foreach (Collider2D col in collider)
            if (col.CompareTag("Player"))
                return true;
        return false;
    }

    public void ButtonPressed(bool _ButtonPressed)
    {
        if(_ButtonPressed)
            spriteRenderer.sprite = NotPressedSprite;
        else
            spriteRenderer.sprite = PressedSprite;

        GameObject sound = Instantiate(ButtonClickSound, transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);

        GameObject particle = Instantiate(ButtonParticle, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.TransformPoint(ButtonPressedPos), ButtonPressedSize);
    }
}
