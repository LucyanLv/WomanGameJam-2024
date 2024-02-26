using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Collider2D encenderLuz;
    public Collider2D tarjeta;
    public Collider2D enchufe;
    public Collider2D luz;
    public Collider2D maquina;
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

    [Header("Tareas")]
    public TMP_Text tarea1;
    public TMP_Text tarea2;
    public TMP_Text tarea3;
    public TMP_Text tarea4;
    public TMP_Text tarea5;
    public TMP_Text tarea6;
    public string texto1;
    public string texto2;
    public string texto3;
    public Color tareaRealizada;
    public Color tareaAMedia;
    public Color tareaCasiCompleta;

    [Header("Tareas")]
    public GameObject tareas;
    public GameObject use;
    public bool useUnaVez = false;

    [Header("Tutorial")]
    public GameObject fondoNegro;
    public Animator negroFondo;
    public GameObject bienvenidos;
    public Animator bienvenido;
    public GameObject resto;
    public Animator restoBienvenido;
    public GameObject comencemos;
    public Animator comencemo;

    [Header("Tutorial Moverse")]
    public GameObject moverse;
    public Animator mMoverse;
    public GameObject interactuar;
    public Animator mInteractuar;

    [Header("Mensajes En Juego")]
    public bool activarMensajes = false;
    public float contadorHacer;
    public float subeContador;
    public GameObject hacer1;
    public Animator queHacer1;
    public GameObject hacer2;
    public Animator queHacer2;
    public GameObject hacer3;
    public Animator queHacer3;
    public GameObject hacer4;
    public Animator queHacer4;
    public GameObject hacer5;
    public Animator queHacer5;
    public GameObject hacer6;
    public Animator queHacer6;

    [Header("Particula")]
    public GameObject particula1;
    public GameObject particula2;
    public GameObject particula3;
    public GameObject particula4;
    public GameObject particula5;
    public GameObject particula6;
    public GameObject particula7;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        StartCoroutine(TutorialInicial());

        encenderLuz.enabled = false;
        tarjeta.enabled = false;
        enchufe.enabled = false;
        luz.enabled = false;
        maquina.enabled = false;
        caja.enabled = false;
    }

    void Update()
    {
        if (!estaEnMiniJuego)
        {
            if (activarMensajes)
            {
                StartCoroutine(MensajesJuego());
            }
            tareas.SetActive(true);
            speed = 5f;
            float movimientoX = Input.GetAxisRaw("Horizontal");
            float movimientoY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(movimientoX, movimientoY).normalized;

            playerAnimator.SetFloat("Horizontal", movimientoX);
            playerAnimator.SetFloat("Vertical", movimientoY);
            playerAnimator.SetFloat("Velocidad", movement.sqrMagnitude);
        }
        else
        {
            queHacer1.SetBool("Desaparecer", true);
            queHacer2.SetBool("Desaparecer", true);
            queHacer3.SetBool("Desaparecer", true);
            queHacer4.SetBool("Desaparecer", true);
            queHacer5.SetBool("Desaparecer", true);

            tareas.SetActive(false);
            speed = 0f;
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
            speed = 0;
            miniJuegoEnchufe.SetActive(true);
            enchufe.enabled = false;
            estaEnMiniJuego = true;
        }
        if (moverEnchufe.emparentadoAEnchufe)
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
        if (llegadasACaja == 1)
        {
            tarea6.text = texto1;
            tarea6.color = tareaAMedia;
        }
        if (llegadasACaja == 2)
        {
            tarea6.text = texto2;
            tarea6.color = tareaCasiCompleta;
        }
        if (llegadasACaja == 3)
        {
            tarea6.text = texto3;
            tarea6.color = tareaRealizada;
            caja.enabled = false;
            llegarCaja.enabled = false;
            queHacer6.SetBool("Desaparecer", true);

            StartCoroutine(Correcto());
        }
    }
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }

    // Método llamado cuando se produce una colisión en 2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JuegoEnchufe"))
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
                playerAnimator.SetBool("TieneCaja", false);
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
            if (subeContador == 0)
            {
                contadorHacer = contadorHacer + 1;
                subeContador = subeContador + 1;
            }
            encenderLuz.enabled = false;
            tarea1.color = tareaRealizada;
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoEncenderLuz.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaEncenderLuz = true;
            mensaje1.SetActive(true);
            use.SetActive(false);
            yield return new WaitForSeconds(6);
            primerMensaje.SetBool("Desaparecer", true);
            use.SetActive(true);
            yield return new WaitForSeconds(1);
            mensaje1.SetActive(false);
        }
    }
    IEnumerator DesactivarTarjeta()
    {
        if (!corrutinaTarjeta)
        {
            if (subeContador == 1)
            {
                contadorHacer = contadorHacer + 1;
                subeContador = subeContador + 1;
            }
            tarjeta.enabled = false;
            tarea2.color = tareaRealizada;
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoTarjeta.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaTarjeta = true;
            mensaje2.SetActive(true);
            use.SetActive(false);
            yield return new WaitForSeconds(7);
            segundoMensaje.SetBool("Desaparecer", true);
            use.SetActive(true);
            yield return new WaitForSeconds(1);
            mensaje2.SetActive(false);
        }
    }
    IEnumerator DesactivarEnchufe()
    {
        if (!corrutinaEnchufe)
        {
            enchufe.enabled = false;
            tarea3.color = tareaRealizada;
            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoEnchufe.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaEnchufe = true;
            if (subeContador == 2)
            {
                contadorHacer = contadorHacer + 1;
                subeContador = subeContador + 1;
            }
        }
    }
    IEnumerator DesactivarLuz()
    {
        if (!corrutinaLuz)
        {
            luz.enabled = false;
            tarea4.color = tareaRealizada;

            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoLuces.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaLuz = true;
            if (subeContador == 3)
            {
                contadorHacer = contadorHacer + 1;
                subeContador = subeContador + 1;
            }
        }
    }
    IEnumerator DesactivarMaquina()
    {
        if (!corrutinaMaquina)
        {
            maquina.enabled = false;
            tarea5.color = tareaRealizada;

            yield return new WaitForSeconds(tiempoDesactivar);
            miniJuegoMaquina.SetActive(false);
            estaEnMiniJuego = false;
            corrutinaMaquina = true;
            if (subeContador == 4)
            {
                contadorHacer = contadorHacer + 1;
                subeContador = subeContador + 1;
            }
        }
    }

    IEnumerator Correcto()
    {
        if (subeContador == 5)
        {
            contadorHacer = contadorHacer + 1;
            subeContador = subeContador + 1;
        }
        correcto.SetActive(true);
        yield return new WaitForSeconds(tiempoDesactivar);
        correcto.SetActive(false);
        mensaje3.SetActive(true);
        if (!useUnaVez)
        {
            use.SetActive(false);
            yield return new WaitForSeconds(6);
            tercerMensaje.SetBool("Desaparecer", true);
            yield return new WaitForSeconds(1);
            mensaje3.SetActive(false);
            yield return new WaitForSeconds(2);
            mensaje4.SetActive(true);
            use.SetActive(true);
        }
        useUnaVez = true;
    }
    IEnumerator MensajesJuego()
    {
        if (contadorHacer == 0)
        {
            yield return new WaitForSeconds(1);
            encenderLuz.enabled = true;
            hacer1.SetActive(true);
            yield return new WaitForSeconds(1);
            particula1.SetActive(true);
        }
        else
        {
            hacer1.SetActive(false);
            particula1.SetActive(false);
            encenderLuz.enabled = false;
        }
        if (contadorHacer == 1)
        {
            yield return new WaitForSeconds(7);
            tarjeta.enabled = true;
            hacer2.SetActive(true);
            yield return new WaitForSeconds(1);
            particula2.SetActive(true);
        }
        else
        {
            hacer2.SetActive(false);
            particula2.SetActive(false);
            tarjeta.enabled = false;
        }
        if (contadorHacer == 2)
        {
            yield return new WaitForSeconds(10);
            enchufe.enabled = true;
            hacer3.SetActive(true);
            yield return new WaitForSeconds(1);
            particula3.SetActive(true);
        }
        else
        {
            particula3.SetActive(false);
            hacer3.SetActive(false);
            enchufe.enabled = false;
        }
        if (contadorHacer == 3)
        {
            luz.enabled = true;
            hacer4.SetActive(true);
            yield return new WaitForSeconds(1);
            particula4.SetActive(true);
        }
        else
        {
            hacer4.SetActive(false);
            particula4.SetActive(false);
            luz.enabled = false;
        }
        if (contadorHacer == 4)
        {
            maquina.enabled = true;
            hacer5.SetActive(true);
            yield return new WaitForSeconds(1);
            particula5.SetActive(true);
        }
        else
        {
            hacer5.SetActive(false);
            particula5.SetActive(false);
            maquina.enabled = false;
        }
        if (contadorHacer == 5)
        {
            caja.enabled = true;
            hacer6.SetActive(true);
            yield return new WaitForSeconds(1);
            particula6.SetActive(true);
            yield return new WaitForSeconds(1);
            particula7.SetActive(true);
        }
        else
        {
            hacer6.SetActive(false);
            particula6.SetActive(false);
            particula7.SetActive(false);
            caja.enabled = false;
        }
    }
    IEnumerator TutorialInicial()
    {
        yield return new WaitForSeconds(1.5f);
        bienvenidos.SetActive(true);

        yield return new WaitForSeconds(2);
        bienvenido.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(2);
        resto.SetActive(true);
        yield return new WaitForSeconds(12);
        restoBienvenido.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(2);
        comencemos.SetActive(true);
        yield return new WaitForSeconds(2);
        comencemo.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(2);
        negroFondo.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(1);
        fondoNegro.SetActive(false);
        comencemos.SetActive(false);
        // Empezo Juego
        yield return new WaitForSeconds(1);
        moverse.SetActive(true);
        yield return new WaitForSeconds(5);
        mMoverse.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(1);
        moverse.SetActive(false);
        interactuar.SetActive(true);
        yield return new WaitForSeconds(6);
        mInteractuar.SetBool("Desaparecer", true);
        yield return new WaitForSeconds(1);
        interactuar.SetActive(false);
        activarMensajes = true;
    }
}