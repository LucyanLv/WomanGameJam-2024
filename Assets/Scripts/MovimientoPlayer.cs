using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidadMovimiento; // Velocidad de movimiento del jugador
    public float velocidadCaja; // Velocidad al agarrar la caja
    public GameObject canvasObj; // Referencia al objeto Canvas que queremos activar
    public GameObject interactuar;
    public float tiempoDesactivacion; // Tiempo en segundos antes de desactivar el Canvas

    private Rigidbody2D rb;
    private bool canvasActivado = false;
    private float tiempoTranscurrido = 0.0f;
    private bool puedeInteractuar = false;
    private bool agarrandoCaja = false;
    private Animator playerAnimator; // Referencia al componente Animator del jugador
    private string animacionAgarrarCaja; // Nombre de la animación al agarrar la caja

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del jugador
        playerAnimator = GetComponent<Animator>(); // Obtener el componente Animator del jugador
        animacionAgarrarCaja = "TuNombreDeAnimacion"; // Reemplaza "TuNombreDeAnimacion" con el nombre de tu animación
        if (canvasObj != null)
        {
            canvasObj.SetActive(false); // Desactivar el Canvas al inicio si está asignado
        }
    }

    void FixedUpdate()
    {
        if (!canvasActivado)
        {
            MoverJugador();
        }

        if (agarrandoCaja)
        {
            // Realizar acciones específicas cuando se está agarrando la caja
            // Puedes activar una animación específica aquí si es necesario
        }
    }

    void MoverJugador()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal"); // Obtener la entrada horizontal del teclado
        float movimientoVertical = Input.GetAxis("Vertical"); // Obtener la entrada vertical del teclado

        Vector2 movimiento = new Vector2(movimientoHorizontal, movimientoVertical) * velocidadMovimiento * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movimiento);

        if (Input.GetKey(KeyCode.Space) && agarrandoCaja)
        {
            // Realizar acciones específicas mientras se mantiene pulsada la tecla espacio
            // Puedes activar una animación específica aquí si es necesario
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MiniJuego"))
        {
            puedeInteractuar = true;
            if (!canvasActivado)
            {
                interactuar.SetActive(true);

            }

            if (Input.GetKeyDown(KeyCode.Space) && !canvasActivado)
            {
                ActivarCanvas();
            }
        }
        else
        {
            interactuar.SetActive(false);
        }

        if (other.gameObject.CompareTag("AgarrarCaja"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AgarrarCaja();
            }
            else
            {
                velocidadMovimiento = 5f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MiniJuego"))
        {
            puedeInteractuar = false;
        }
    }

    void AgarrarCaja()
    {
        agarrandoCaja = !agarrandoCaja; // Cambiar el estado de agarrar la caja
        velocidadMovimiento = velocidadCaja; // Cambiar la velocidad al agarrar o soltar la caja

        if (agarrandoCaja)
        {
            playerAnimator.Play(animacionAgarrarCaja); // Activar la animación al agarrar la caja
        }
        else
        {
            // Puedes agregar lógica aquí para detener la animación al soltar la caja si es necesario
        }
    }

    void ActivarCanvas()
    {
        if (canvasObj != null && puedeInteractuar)
        {
            canvasObj.SetActive(true); // Activar el Canvas
            canvasActivado = true;
            tiempoTranscurrido = 0.0f;
        }
    }

    void Update()
    {
        if (canvasActivado)
        {
            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido >= tiempoDesactivacion)
            {
                canvasObj.SetActive(false);
                canvasActivado = false;
            }
        }
    }
}
