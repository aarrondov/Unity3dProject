using UnityEngine;

public class MovimientoKid : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float velocidad = 2f;
    public float rotacionVelocidad = 30f;
    public float visionRange = 10f;
    public float visionAngle = 45f;
    public Color nuevoColorPausa = Color.blue;
    public float tiempoPausa = 5f;

    private Vector3 centroCirculo;
    private float radioCirculo;
    private bool estaPausado = false;

    void Start()
    {
        CalcularCentroYRadioCirculo();
        // Iniciar movimiento al inicio
        MoverPersonaje();
    }

    void Update()
    {
        // Solo rotar si no está pausado
        if (!estaPausado)
        {
            RotarHaciaPunto();
        }
    }

    void CalcularCentroYRadioCirculo()
    {
        centroCirculo = (startPoint.position + endPoint.position) / 2f;
        radioCirculo = Vector3.Distance(startPoint.position, endPoint.position) / 2f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("SnowBall"))
        {
            Destroy(collision.gameObject);
            // Cambiar color de los hijos durante la pausa
            CambiarColorRecursivo(transform, nuevoColorPausa);

            // Detener movimiento y rotación durante la pausa
            estaPausado = true;
            CancelInvoke();

            // Iniciar pausa
            Invoke("FinalizarPausa", tiempoPausa);

            // Resto de la lógica (por ejemplo, mostrar mensaje de muerte)
            print("Muerte del personaje");
        }
    }

    void FinalizarPausa()
    {
        // Restaurar color original de los hijos
        CambiarColorRecursivo(transform, Color.white);

        // Finalizar la pausa
        estaPausado = false;

        // Reiniciar movimiento después de la pausa
        MoverPersonaje();
    }

    void CambiarColorRecursivo(Transform objetoPadre, Color color)
    {
        foreach (Transform hijo in objetoPadre)
        {
            Renderer rendererHijo = hijo.GetComponent<Renderer>();
            if (rendererHijo != null)
            {
                rendererHijo.material.color = color;
            }

            CambiarColorRecursivo(hijo, color);
        }
    }

    void MoverPersonaje()
    {
        // Iniciar movimiento y rotación
        InvokeRepeating("MoverEnCirculo", 0f, Time.deltaTime);
    }

    void RotarHaciaPunto()
    {
        Vector3 direccion = centroCirculo - transform.position;
        Quaternion rotacionHaciaObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionHaciaObjetivo, rotacionVelocidad * Time.deltaTime);
    }

    void MoverEnCirculo()
    {
        // Calcular la nueva posición en el círculo
        float anguloActual = Mathf.Atan2(transform.position.z - centroCirculo.z, transform.position.x - centroCirculo.x);
        anguloActual += velocidad * Time.deltaTime;

        float x = centroCirculo.x + radioCirculo * Mathf.Cos(anguloActual);
        float z = centroCirculo.z + radioCirculo * Mathf.Sin(anguloActual);

        // Establecer la nueva posición
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
