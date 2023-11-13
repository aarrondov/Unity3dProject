using UnityEngine;

public class LanzadorDeSnowBalls : MonoBehaviour
{
    public GameObject snowBallPrefab; // Asigna el prefab de la esfera en el Inspector
    public float fuerzaLanzamiento = 10f; // Ajusta la fuerza de lanzamiento según sea necesario

    public Camera playerCamera; // Asigna la cámara del jugador en el Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LanzarSnowBall();
        }
    }

    void LanzarSnowBall()
    {
        GameObject snowBall = Instantiate(snowBallPrefab, transform.position, Quaternion.identity);
        Rigidbody snowBallRb = snowBall.GetComponent<Rigidbody>();

        if (snowBallRb != null)
        {
            Vector3 direccionLanzamiento = playerCamera.transform.forward;
            snowBallRb.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("El prefab de la esfera ('SnowBall') no tiene un Rigidbody.");
        }
    }
}