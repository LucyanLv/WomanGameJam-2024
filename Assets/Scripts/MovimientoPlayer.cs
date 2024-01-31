using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;

    // Activar MiniJuegos
    [Header("Activar MiniJuegos")]
    public GameObject miniJuegoEnchufe;
    public GameObject miniJuegoTarjeta;
    public GameObject miniJuegoLuces;
    public GameObject miniJuegoMaquina;
    public GameObject miniJuegoEncenderLuz;

    // Puede jugar miniJuegos
    [Header("Puede Jugar MiniJuegos")]
    public bool estaEnMiniJuego = false;
    public bool juegoEnchufe = false;
    public bool juegoTarjeta = false;
    public bool juegoLuz = false;
    public bool juegoMaquina = false;
    public bool juegoInterruptor = false;

    // Referencias de scripts
    [Header("Referencias de Script")]
    public MoverEnchufe moverEnchufe;
    public MoverTarjeta moverTarjeta;
    public BotonEncender botonEncender;
    public DetectarProducto detectarProducto;
    public Validar validar;

    // Tiempo antes de desactivar los minijuegos
    public float tiempoDesactivar;

    // Colaiders para desactivarlos
    [Header("Desactivar Colaiders")]
    public Collider2D personaje;
    public Collider2D enchufe;
    public Collider2D tarjeta;
    public Collider2D luz;
    public Collider2D maquina;
    public Collider2D encenderLuz;

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
            enchufe.enabled = false;
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
            tarjeta.enabled = false;
            estaEnMiniJuego = true;
        }
        if (moverTarjeta.terminoJuegoTarjeta)
        {
            StartCoroutine(DesactivarTarjeta());
        }

        // 3
        if (juegoLuz && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoLuces.SetActive(true);
            luz.enabled = false;
            estaEnMiniJuego = true;
        }
        if (botonEncender.terminoJuegoLuz)
        {
            StartCoroutine(DesactivarLuz());
        }

        // 4
        if (juegoMaquina && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoMaquina.SetActive(true);
            maquina.enabled = false;
            estaEnMiniJuego = true;
        }
        if (detectarProducto.terminoJuegoMaquina)
        {
            StartCoroutine(DesactivarMaquina());
        }

        // 5
        if (juegoInterruptor && Input.GetKeyDown(KeyCode.Space))
        {
            miniJuegoEncenderLuz.SetActive(true);
            encenderLuz.enabled = false;
            estaEnMiniJuego = true;
        }
        if (validar.terminoJuegoInterruptor)
        {
            StartCoroutine(DesactivarEncenderLuz());
        }

        // Desactivar Colaiders
        if (estaEnMiniJuego)
        {
            personaje.enabled = false;
        }
        else
        {
            personaje.enabled = true;
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
        if (other.CompareTag("JuegoMaquina"))
        {
            juegoMaquina = true;
        }
        if (other.CompareTag("JuegoInterruptor"))
        {
            juegoInterruptor = true;
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
        if (other.CompareTag("JuegoMaquina"))
        {
            juegoMaquina = false;
        }
        if (other.CompareTag("JuegoInterruptor"))
        {
            juegoInterruptor = false;
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
        miniJuegoLuces.SetActive(false);
        estaEnMiniJuego = false;
    }
    IEnumerator DesactivarMaquina()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoMaquina.SetActive(false);
        estaEnMiniJuego = false;
    }
    IEnumerator DesactivarEncenderLuz()
    {
        yield return new WaitForSeconds(tiempoDesactivar);
        miniJuegoEncenderLuz.SetActive(false);
        estaEnMiniJuego = false;
    }
}