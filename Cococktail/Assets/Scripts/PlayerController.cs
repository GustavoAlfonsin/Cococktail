using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float gravedad = -9.81f;
    public float fuerzaSalto = 2f;

    [Header("Cámara")]
    public Transform camara;  // Asigna la cámara del jugador
    public float sensibilidadMouse = 2f;
    public float limiteVertical = 80f;

    private CharacterController controller;
    private Vector3 velocidadJugador;
    private float rotacionX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mover();
        RotarCamara();
    }

    void Mover()
    {
        // Movimiento en plano XZ
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;
        Vector3 movimiento = direccion * velocidad;

        // Si está en el suelo
        if (controller.isGrounded)
        {
            if (velocidadJugador.y < 0)
                velocidadJugador.y = -2f; // asegura que quede pegado al suelo

            // Salto
            if (Input.GetButtonDown("Jump"))
            {
                velocidadJugador.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
            }
        }

        // Aplicar gravedad
        velocidadJugador.y += gravedad * Time.deltaTime;

        // Mover jugador
        controller.Move((movimiento + velocidadJugador) * Time.deltaTime);
    }

    void RotarCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        // Rotación horizontal (jugador)
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical (cámara)
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);
        camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }
}
