using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidadMovimiento; // Velocidad de movimiento del jugador
    public GameObject juegoEnchufe; // Referencia al objeto que se activa y desactiva
    public GameObject fondoUse; // Primer objeto a desactivar
    public GameObject use; // Segundo objeto a desactivar
    public float tiempoDesactivacion; // Tiempo en segundos antes de desactivar el objeto

    private Rigidbody2D rb;
    private bool estaEnMiniJuego = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del jugador
    }

    void FixedUpdate()
    {
        MoverJugador();
    }

    void Update()
    {
        if (estaEnMiniJuego && Input.GetKeyDown(KeyCode.Space))
        {
            ActivarDesactivarObjeto();
        }
    }

    void MoverJugador()
    {
        // Verificar si el objeto está activo antes de permitir el movimiento del jugador
        if (juegoEnchufe == null || !juegoEnchufe.activeSelf)
        {
            float movimientoHorizontal = Input.GetAxis("Horizontal"); // Obtener la entrada horizontal del teclado
            float movimientoVertical = Input.GetAxis("Vertical"); // Obtener la entrada vertical del teclado

            Vector2 movimiento = new Vector2(movimientoHorizontal, movimientoVertical) * velocidadMovimiento * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movimiento);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MiniJuego"))
        {
            estaEnMiniJuego = true;
            use.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MiniJuego"))
        {
            estaEnMiniJuego = false;
            use.SetActive(false);
        }
    }

    void ActivarDesactivarObjeto()
    {
        // Si está en detección con el tag "MiniJuego", activar el objeto y desactivar otros dos
        if (juegoEnchufe != null)
        {
            juegoEnchufe.SetActive(true);

            // Desactivar otros dos objetos
            if (fondoUse != null)
            {
                fondoUse.SetActive(false);
            }

            if (use != null)
            {
                use.SetActive(false);
            }

            StartCoroutine(DesactivarObjetoConRetardo());
        }
    }

    IEnumerator DesactivarObjetoConRetardo()
    {
        yield return new WaitForSeconds(tiempoDesactivacion);

        // Desactivar el objeto después del tiempo especificado
        if (juegoEnchufe != null)
        {
            juegoEnchufe.SetActive(false);

            // Volver a activar los otros dos objetos
            if (fondoUse != null)
            {
                fondoUse.SetActive(true);
            }
        }
    }
}
