using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaivour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Luz tocó SantaPrefab");
            // Puedes agregar lógica adicional aquí según tus necesidades
        }
    }
}
