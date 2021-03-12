using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class DropShadowTilemap : MonoBehaviour
{
    [SerializeField] Vector2 ShadowOffset;
    [SerializeField] Material ShadowMaterial;

    TilemapRenderer tileMapRenderer;
    TilemapRenderer shadowTileMapRenderer;
    GameObject shadowGameobject;

    void Start()
    {
        tileMapRenderer = GetComponent<TilemapRenderer>();

        shadowGameobject = Instantiate(transform.parent.gameObject, transform.parent.parent);
        shadowGameobject.GetComponentInChildren<DropShadowTilemap>().enabled = false;
        shadowGameobject.GetComponentInChildren<TilemapCollider2D>().enabled = false;
        shadowGameobject.transform.position = (Vector2)transform.parent.position + ShadowOffset;

        shadowTileMapRenderer = shadowGameobject.GetComponentInChildren<TilemapRenderer>();

        shadowTileMapRenderer.material = ShadowMaterial;

        shadowTileMapRenderer.sortingLayerName = tileMapRenderer.sortingLayerName;
        shadowTileMapRenderer.sortingOrder = tileMapRenderer.sortingOrder - 1;
    }
}