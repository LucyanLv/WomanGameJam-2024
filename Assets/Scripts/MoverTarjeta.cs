using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTarjeta : MonoBehaviour
{
    public Transform objetivoDestino;
    private Vector3 posicionInicial;
    private bool estadoInicialActivado = false;

    public bool enMovimiento = false;
    public bool haLlegado = false;
    public bool puedeMoverHorizontal = false;
    public float velocidad = 5f;
    public float velocidadVuelta = 5f;
    public Transform objetoRangoMinimo;
    public Transform objetoRangoMaximo;

    // Nuevas variables para el contador
    public float contadorInicial = 10f;
    public float contadorActual;
    public bool mensajeEnviado = false;
    public bool EmpezoTiempo = false;

    // Para activar y desactivar y activar las respuestas
    public GameObject rCorecta;
    public GameObject rIncorecta;
    public float desactivar;

    // Termino juego
    public bool terminoJuego = false;

    void Start()
    {
        posicionInicial = transform.position;
        if (!estadoInicialActivado)
        {
            estadoInicialActivado = true;
            EstablecerEstadoInicial();
        }
    }

    void OnEnable()
    {
        if (estadoInicialActivado)
        {
            EstablecerEstadoInicial();
        }
    }

    void OnDisable()
    {
        // Aquí puedes realizar acciones de limpieza o guardar estado si es necesario
        ReiniciarEstado();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!enMovimiento && !haLlegado)
                {
                    enMovimiento = true;
                }
                else if (haLlegado)
                {
                    puedeMoverHorizontal = true;
                }
            }
            else
            {
                puedeMoverHorizontal = false;
            }
        }
        else
        {
            if (puedeMoverHorizontal)
            {
                enMovimiento = false;
                haLlegado = false;
            }
            puedeMoverHorizontal = false;
        }

        if (enMovimiento)
        {
            MoverHaciaObjetivo();
        }

        if (puedeMoverHorizontal)
        {
            MoverHorizontalConMouse();
        }
        else if (!enMovimiento)
        {
            VolverAPosicionInicial();
        }

        if (EmpezoTiempo)
        {
            if (contadorActual > 0)
            {
                contadorActual -= Time.deltaTime;
            }
            else
            {
                if (!mensajeEnviado)
                {
                    Debug.Log("Tiempo ha llegado a 0");
                    Debug.Log("Registrado");
                    mensajeEnviado = true;
                }
            }
        }
    }

    void MoverHaciaObjetivo()
    {
        Vector3 direccion = (objetivoDestino.position - transform.position).normalized;
        Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;

        transform.position = nuevaPosicion;

        if (Vector3.Distance(transform.position, objetivoDestino.position) < 0.1f)
        {
            enMovimiento = false;
            haLlegado = true;
            posicionInicial = transform.position;
        }
    }

    void MoverHorizontalConMouse()
    {
        float rangoMinimoX = objetoRangoMinimo.position.x;
        float rangoMaximoX = objetoRangoMaximo.position.x;

        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float nuevaPosX = Mathf.Clamp(posicionMouse.x, rangoMinimoX, rangoMaximoX);
        transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);
    }

    void VolverAPosicionInicial()
    {
        transform.position = Vector3.Lerp(transform.position, posicionInicial, velocidadVuelta * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DetectoTarjeta") && !EmpezoTiempo)
        {
            contadorActual = contadorInicial;
            mensajeEnviado = false;
            EmpezoTiempo = true;
        }
        if (collision.gameObject.CompareTag("DetectoTarjeta"))
        {
            if (contadorActual <= 0)
            {
                rCorecta.SetActive(true);
                terminoJuego = true;
                StartCoroutine(Desactivar());
            }
            else
            {
                rIncorecta.SetActive(true);
                StartCoroutine(Desactivar());
            }
        }
    }

    IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(desactivar);
        rCorecta.SetActive(false);
        rIncorecta.SetActive(false);
    }

    void EstablecerEstadoInicial()
    {
        enMovimiento = false;
        haLlegado = false;
        puedeMoverHorizontal = false;
        mensajeEnviado = false;
        EmpezoTiempo = false;
        terminoJuego = false;

        transform.position = posicionInicial;

        rCorecta.SetActive(false);
        rIncorecta.SetActive(false);
    }

    void ReiniciarEstado()
    {
        // Almacena la posición actual antes de reiniciar
        posicionInicial = transform.position;

        enMovimiento = false;
        haLlegado = false;
        puedeMoverHorizontal = false;
        mensajeEnviado = false;
        EmpezoTiempo = false;
        terminoJuego = false;

        rCorecta.SetActive(false);
        rIncorecta.SetActive(false);
    }

}
