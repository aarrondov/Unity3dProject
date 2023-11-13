using UnityEngine;

public class CambiadorTextura : MonoBehaviour
{
    public Material nuevaTextura; // Asigna la nueva textura en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CambiarTextura();
        }
    }

    private void CambiarTextura()
    {
        // Asegúrate de que el objeto "Regalo" tiene un Mesh Renderer
        MeshRenderer regaloMeshRenderer = GetComponent<MeshRenderer>();

        if (regaloMeshRenderer != null)
        {
            // Cambia el material al nuevo material asignado
            regaloMeshRenderer.material = nuevaTextura;
        }
        else
        {
            Debug.LogWarning("El objeto 'Regalo' no tiene un Mesh Renderer.");
        }
    }
}