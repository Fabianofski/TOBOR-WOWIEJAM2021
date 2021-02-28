using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    [SerializeField] Vector2 ShadowOffset;
    [SerializeField] Material ShadowMaterial;
    [SerializeField] bool UpdateSprite;

    SpriteRenderer spriteRenderer;
    SpriteRenderer shadowSpriteRenderer;
    GameObject shadowGameobject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");
        shadowGameobject.transform.parent = transform;
        shadowGameobject.transform.localPosition = Vector2.zero + ShadowOffset;
        shadowGameobject.transform.eulerAngles = transform.eulerAngles;

        shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();

        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowSpriteRenderer.material = ShadowMaterial;

        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }

    private void Update()
    {
        if (!UpdateSprite) return;

        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
    }
}