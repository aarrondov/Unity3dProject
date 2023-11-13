using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController playerController;
    public float gravityScale;
    public Camera playerCamera;
    public float normalFOV;
    public float runFOV;
    private bool isRunning = false;

    private Vector3 moveDirection;
    private float yStore;
    private bool isGrounded = true;

    void Start()
    {
        playerCamera.fieldOfView = normalFOV;
    }

    void Update()
    {
        // print(isGrounded);
        yStore = moveDirection.y;
    
        // Movimiento
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 forwardMovement = playerCamera.transform.forward * verticalInput;
        Vector3 rightMovement = playerCamera.transform.right * horizontalInput;
        forwardMovement.y = 0f;
        rightMovement.y = 0f;
        moveDirection = (forwardMovement + rightMovement).normalized * (isRunning ? moveSpeed * 2f : moveSpeed);

        moveDirection.y = yStore;

        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        playerController.Move(moveDirection * Time.deltaTime);

        // Cambio de FOV al correr
        isRunning = Input.GetKey(KeyCode.LeftShift);
        playerCamera.fieldOfView = isRunning ? runFOV : normalFOV;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HouseDoor"))
        {
            // Obtener la posición actual del jugador
            Vector3 playerPosition = transform.position;

            // Almacenar la posición actual en ControladorPosicion
            // ControladorPosicion.Instance.posicionJugador = playerPosition;
            ControladorPosicion.Instance.posicionJugador = new Vector3(playerPosition.x,playerPosition.y,playerPosition.z - 1);

            // Cargar la escena de la casa
            String scene = other.transform.parent.name;
            SceneManager.LoadScene("Inside" + scene);
        }
        
        if (other.CompareTag("ExitHouse"))
        {
            SceneManager.LoadScene("Inicio");
        }

        if (other.CompareTag("Gift"))
        {
            print("Enter on gift");
        }

        if (other.CompareTag("DeathVision"))
        {
            print("PJ debe morir");
        }

        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}