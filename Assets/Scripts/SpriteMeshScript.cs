using UnityEngine;

public class SpriteMeshScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture("_BaseMap", spriteRenderer.sprite.texture);
    }
}
