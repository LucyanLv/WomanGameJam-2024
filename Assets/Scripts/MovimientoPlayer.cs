using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float velocidadEnJuegoCaja;
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

    // Colaiders Que se desactivan si esta en minijuego
    public Collider2D enchufe2;
    public Collider2D tarjeta2;
    public Collider2D luz2;
    public Collider2D maquina2;
    public Collider2D encenderLuz2;
    public Collider2D caja2;
    public Collider2D llegarCaja2;

    public GameObject correcto;

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

            //fondoPuedeUsar.SetActive(true);
            enchufe2.enabled = true;
            tarjeta2.enabled = true;
            luz2.enabled = true;
            maquina2.enabled = true;
            encenderLuz2.enabled = true;
            caja2.enabled = true;
            llegarCaja2.enabled = true;
        }
        else
        {
            enchufe2.enabled = false;
            tarjeta2.enabled = false;
            luz2.enabled = false;
            maquina2.enabled = false;
            encenderLuz2.enabled = false;
            caja2.enabled = false;
            llegarCaja2.enabled = false;
            //fondoPuedeUsar.SetActive(false);
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