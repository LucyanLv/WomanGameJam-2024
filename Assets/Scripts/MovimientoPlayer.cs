using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidadMovimiento; // Velocidad de movimiento del jugador
    public GameObject juegoEnchufe; // Referencia al primer minijuego
    public GameObject fondoUse; // Primer objeto a desactivar
    public GameObject use; // Segundo objeto a desactivar

    private Rigidbody2D rb; // rigidbody del objeto
    private bool estaEnMiniJuego = false;

    public MoverEnchufe moverEnchufe;

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
        if (moverEnchufe.EmparentadoAEnchufe)
        {
            StartCoroutine(TerminoJuego());
        }
    }

    void MoverJugador()
    {
        // Verificar si el objeto está activo antes de permitir el movimiento del jugador
        if (juegoEnchufe == null || !juegoEnchufe.activeSelf)
        {
            float movimientoHorizontal = Input.GetAxis("Horizontal"); 
            float movimientoVertical = Input.GetAxis("Vertical"); 

            Vector2 movimiento = new Vector2(movimientoHorizontal, movimientoVertical) * velocidadMovimiento * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movimiento);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JuegoEnchufe"))
        {
            estaEnMiniJuego = true;
            use.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JuegoEnchufe"))
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
        }
    }

    IEnumerator TerminoJuego()
    {
        yield return new WaitForSeconds(1);
        juegoEnchufe.SetActive(false);
    }
}
