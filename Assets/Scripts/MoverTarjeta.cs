using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTarjeta : MonoBehaviour
{
    public Transform objetivoDestino;
    private Vector3 posicionInicial;
    public bool enMovimiento = false;
    public bool haLlegado = false;
    public bool puedeMoverHorizontal = false;
    public float velocidad = 5f;
    public float velocidadVuelta = 5f;
    public Transform objetoRangoMinimo;
    public Transform objetoRangoMaximo;

    // Nuevas variables para el contador
    public float contadorInicial = 10f; // Número inicial del contador definido desde el Inspector
    public float contadorActual; // Contador que disminuirá
    public bool mensajeEnviado = false; // Variable para controlar si ya se ha enviado el mensaje
    public bool EmpezoTiempo = false;

    // Para activar y desactivar y activar las respuestas
    public GameObject rCorecta;
    public GameObject rIncorecta;
    public float desactivar;

    // Termino juego
    public bool terminoJuego;

    void Start()
    {
        terminoJuego = false;
        posicionInicial = transform.position;
        contadorActual = contadorInicial; // Inicializar el contador al valor inicial definido desde el Inspector
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

        // Reducir el contador y enviar el mensaje cuando llega a cero
        if (EmpezoTiempo)
        {
            if (contadorActual > 0)
            {
                contadorActual -= Time.deltaTime;
            }
            else
            {
                // Acciones a realizar cuando el contador llega a cero
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
            // Reiniciar el contador al valor inicial definido desde el Inspector
            contadorActual = contadorInicial;
            mensajeEnviado = false; // Permitir enviar el mensaje nuevamente
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
}