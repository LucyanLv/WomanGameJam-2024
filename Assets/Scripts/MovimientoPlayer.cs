using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;

    // Activar MiniJuegos
    public GameObject miniJuegoEnchufe;
    public GameObject miniJuegoTarjeta;
    public GameObject miniJuegoLuz;

    // Puede jugar miniJuegos
    public bool estaEnMiniJuego = false;
    public bool juegoEnchufe = false;
    public bool juegoTarjeta = false;
    public bool juegoLuz = false;

    // Referencias de scripts
    public MoverEnchufe moverEnchufe;
    public MoverTarjeta moverTarjeta;
    public BotonEncender botonEncender;

    // Tiempo antes de desactivar los minijuegos
    public float tiempoDesactivar;

    // Colaiders para desactivarlos
    public Collider2D enchufe;
    public Collider2D tarjeta;
    public Collider2D luz;

    void Update()
    {
        if (!estaEnMiniJuego)
        {
            // Obtener la entrada del teclado
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcular la dirección del movimiento
            Vector2 movement = new Vector2(horizontalInput, verticalInput);

            // Mover el personaje usando Rigidbody
            transform.Translate(movement * speed * Time.deltaTime);
        }

        // 1 si esta en el mini juego y preciona espacio MINIJUEGO ENCHUFE
        if (juegoEnchufe && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoEnchufe.SetActive(true);
            estaEnMiniJuego = true;
        }
        if (moverEnchufe.EmparentadoAEnchufe)
        {
            StartCoroutine(DesactivarEnchufe());
        }

        // 2
        if (juegoTarjeta && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoTarjeta.SetActive(true);
            estaEnMiniJuego = true;
        }
        if (moverTarjeta.terminoJuego)
        {
            StartCoroutine(DesactivarTarjeta());
        }

        // 3
        if (juegoLuz && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoLuz.SetActive(true);
            estaEnMiniJuego = true;
        }
        if (botonEncender.terminoJuego)
        {
            StartCoroutine(DesactivarLuz());
        }

        // Desactivar Colaiders
        if (estaEnMiniJuego)
        {
            enchufe.enabled = false;
            tarjeta.enabled = false;
            luz.enabled = false;
        }
        else
        {
            enchufe.enabled = true;
            tarjeta.enabled = true;
            luz.enabled = true;
        }
    }

    // Método llamado cuando se produce una colisión en 2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("JuegoEnchufe"))
        {
            juegoEnchufe = true;
        }
        if (other.CompareTag("JuegoTarjeta"))
        {
            juegoTarjeta = true;
        }
        if (other.CompareTag("JuegoLuz"))
        {
            juegoLuz = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JuegoEnchufe"))
        {
            juegoEnchufe = false;
        }
        if (other.CompareTag("JuegoTarjeta"))
        {
            juegoTarjeta = false;
        }
        if (other.CompareTag("JuegoLuz"))
        {
            juegoLuz = false;
        }
    }
    
    IEnumerator DesactivarEnchufe()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoEnchufe.SetActive(false);
        estaEnMiniJuego = false;
    }

    IEnumerator DesactivarTarjeta()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoTarjeta.SetActive(false);
        estaEnMiniJuego = false;
    }

    IEnumerator DesactivarLuz()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoLuz.SetActive(false);
        estaEnMiniJuego = false;
    }
}