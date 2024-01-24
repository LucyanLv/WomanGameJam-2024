using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;

    // Activar MiniJuegos
    public GameObject miniJuegoEnchufe;
    public GameObject miniJuegoTarjeta;

    // Puede jugar miniJuegos
    public bool estaEnMiniJuego = false;
    public bool juegoEnchufe = false;
    public bool juegoTarjeta = false;

    // Referencias de scripts
    public MoverEnchufe moverEnchufe;
    public MoverTarjeta moverTarjeta;

    public float tiempoDesactivar;

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
            StartCoroutine(EsperarParaDesactivar());
        }

        // 2
        if (juegoTarjeta && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoTarjeta.SetActive(true);
            estaEnMiniJuego = true;
        }
        if (moverTarjeta.terminoJuego)
        {
            StartCoroutine(EsperarParaDesactivar());
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
    }
    
    IEnumerator EsperarParaDesactivar()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoEnchufe.SetActive(false);
        miniJuegoTarjeta.SetActive(false);
        estaEnMiniJuego = false;
    }
}