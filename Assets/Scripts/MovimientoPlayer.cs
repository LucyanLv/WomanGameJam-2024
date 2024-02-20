using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float velocidadCaja;
    public bool manteniendoEspacio = false;
    public int llegadasACaja = 0;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private Vector2 movement;

    // Activar MiniJuegos
    [Header("Activar MiniJuegos")]
    public GameObject miniJuegoEnchufe;
    public GameObject miniJuegoTarjeta;
    public GameObject miniJuegoLuces;
    public GameObject miniJuegoMaquina;
    public GameObject miniJuegoEncenderLuz;
    public GameObject puedeUsar;

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

    [Header("Boleanos Corrutinas")]
    public bool corrutinaEnchufe = false;
    public bool corrutinaTarjeta = false;
    public bool corrutinaLuz = false;
    public bool corrutinaMaquina = false;
    public bool corrutinaEncenderLuz = false;

    [Header("Textos")]
    public GameObject mensaje1;
    public Animator primerMensaje;
    public GameObject mensaje2;
    public Animator segundoMensaje;
    public GameObject mensaje3;
    public Animator tercerMensaje;
    public GameObject mensaje4;
    public Animator cuartoMensaje;
    public GameObject mensaje5;
    public Animator quintoMensaje;
    public GameObject mensaje6;
    public Animator sextoMensaje;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!estaEnMiniJuego)
        {
            float movimientoX = Input.GetAxisRaw("Horizontal");
            float movimientoY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(movimientoX, movimientoY).normalized;

            playerAnimator.SetFloat("Horizontal", movimientoX);
            playerAnimator.SetFloat("Vertical", movimientoY);
            playerAnimator.SetFloat("Velocidad", movement.sqrMagnitude);
        }

        if ((juegoCaja || manteniendoEspacio) && Input.GetKey(KeyCode.Space))
        {
            speed = velocidadCaja;
            manteniendoEspacio = true;
            playerAnimator.SetBool("TieneCaja", true);
        }
        else
        {
            playerAnimator.SetBool("TieneCaja", false);
            speed = 5f;
            manteniendoEspacio = false;
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
            estaEnMiniJuego = true;
            miniJuegoTarjeta.SetActive(true);
            tarjeta.enabled = false;
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
            estaEnMiniJuego = true;
            miniJuegoEncenderLuz.SetActive(true);
            encenderLuz.enabled = false;
        }
        if (validar.terminoJuegoInterruptor)
        {
            StartCoroutine(DesactivarEncenderLuz());
        }

        // 6
        if (llegadasACaja == 2)
        {
            caja.enabled = false;
            llegarCaja.enabled = false;
            StartCoroutine(Correcto());
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
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
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
            playerAnimator.SetBool("TieneCaja", false);

            if (Mathf.Approximately(speed, velocidadCaja))
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

    IEnumerator DesactivarEncenderLuz()
    {
        if (!corrutinaEncenderLuz)
        {
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoEncenderLuz.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaEncenderLuz = true;
            mensaje1.SetActive(true);
            yield return new WaitForSeconds(5);
            primerMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje1.SetActive(false);
        }
    }
    IEnumerator DesactivarTarjeta()
    {
        if (!corrutinaTarjeta)
        {
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoTarjeta.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaTarjeta = true;
            mensaje2.SetActive(true);
            yield return new WaitForSeconds(5);
            segundoMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje2.SetActive(false);
        }
    }
    IEnumerator DesactivarEnchufe()
    {
        if (!corrutinaEnchufe)
        {
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoEnchufe.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaEnchufe = true;
            mensaje3.SetActive(true);
            yield return new WaitForSeconds(5);
            tercerMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje3.SetActive(false);
        }
    }
    IEnumerator DesactivarLuz()
    {
        if (!corrutinaLuz)
        {
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoLuces.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaLuz = true;
            mensaje4.SetActive(true);
            yield return new WaitForSeconds(7);
            cuartoMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje4.SetActive(false);
        }
    }
    IEnumerator DesactivarMaquina()
    {
        if (!corrutinaMaquina)
        {
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoMaquina.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaMaquina = true;
            mensaje5.SetActive(true);
            yield return new WaitForSeconds(5);
            quintoMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje5.SetActive(false);
        }
    }

    IEnumerator Correcto()
    {
        correcto.SetActive(true);
        yield return new WaitForSeconds(tiempoDesactivar);
        correcto.SetActive(false);
        mensaje6.SetActive(true);
        yield return new WaitForSeconds(5);
        sextoMensaje.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(1);
        mensaje6.SetActive(false);
    }
}