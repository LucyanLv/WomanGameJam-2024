using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float velocidadEnJuegoCaja;
    public bool manteniendoEspacio = false;
    public int llegadasACaja = 0;

    // Activar MiniJuegos
    [Header("Activar MiniJuegos")]
    public GameObject miniJuegoEnchufe;
    public GameObject miniJuegoTarjeta;
    public GameObject miniJuegoLuces;
    public GameObject miniJuegoMaquina;
    public GameObject miniJuegoEncenderLuz;
    public GameObject puedeUsar;
    public GameObject fondoPuedeUsar;

    // Puede jugar miniJuegos
    [Header("Puede Jugar MiniJuegos")]
    public bool estaEnMiniJuego = false;
    public bool juegoEnchufe = false;
    public bool juegoTarjeta = false;
    public bool juegoLuz = false;
    public bool juegoMaquina = false;
    public bool juegoInterruptor = false;
    public bool juegoCaja = false;

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
    public Collider2D caja;
    public Collider2D llegarCaja;

    public GameObject correcto;

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
            fondoPuedeUsar.SetActive(true);
        }
        else
        {
            fondoPuedeUsar.SetActive(false);
        }

        if ((juegoCaja || manteniendoEspacio) && Input.GetKey(KeyCode.Space))
        {
            speed = velocidadEnJuegoCaja;
            manteniendoEspacio = true;
            puedeUsar.SetActive(true);
        }
        else
        {
            speed = 5f;
            manteniendoEspacio = false;
            puedeUsar.SetActive(false);
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

        // 6
        if (llegadasACaja == 2)
        {
            StartCoroutine(Correcto());
            caja.enabled = false;
            llegarCaja.enabled = false;
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
            puedeUsar.SetActive(true);
        }
        if (other.CompareTag("JuegoTarjeta"))
        {
            juegoTarjeta = true;
            puedeUsar.SetActive(true);
        }
        if (other.CompareTag("JuegoLuz"))
        {
            juegoLuz = true;
            puedeUsar.SetActive(true);
        }
        if (other.CompareTag("JuegoMaquina"))
        {
            juegoMaquina = true;
            puedeUsar.SetActive(true);
        }
        if (other.CompareTag("JuegoInterruptor"))
        {
            juegoInterruptor = true;
            puedeUsar.SetActive(true);
        }
        if (other.CompareTag("JuegoCaja"))
        {
            puedeUsar.SetActive(true);
            juegoCaja = true;
        }
        if (other.CompareTag("LlegoCaja"))
        {
            if (Mathf.Approximately(speed, velocidadEnJuegoCaja))
            {
                manteniendoEspacio = false;
                llegadasACaja++;
                speed = 5;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JuegoEnchufe"))
        {
            juegoEnchufe = false;
            puedeUsar.SetActive(false);
        }
        if (other.CompareTag("JuegoTarjeta"))
        {
            juegoTarjeta = false;
            puedeUsar.SetActive(false);
        }
        if (other.CompareTag("JuegoLuz"))
        {
            juegoLuz = false;
            puedeUsar.SetActive(false);
        }
        if (other.CompareTag("JuegoMaquina"))
        {
            juegoMaquina = false;
            puedeUsar.SetActive(false);
        }
        if (other.CompareTag("JuegoInterruptor"))
        {
            juegoInterruptor = false;
            puedeUsar.SetActive(false);
        }
        if (other.CompareTag("JuegoCaja"))
        {
            puedeUsar.SetActive(false);
            juegoCaja = false;
            manteniendoEspacio = Input.GetKey(KeyCode.Space);
        }
        if (other.CompareTag("LlegoCaja"))
        {
            speed = 5;
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

    IEnumerator Correcto()
    {
        correcto.SetActive(true);
        yield return new WaitForSeconds(tiempoDesactivar);
        correcto.SetActive(false);
    }
}