using UnityEngine;

public class RepeatTexture : MonoBehaviour
{
    public float tiling = 2.0f; // Ajusta esto para cambiar la frecuencia de repetición de la textura.

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.mainTextureScale = new Vector2(transform.localScale.x * tiling, transform.localScale.y * tiling);
        }
    }
}