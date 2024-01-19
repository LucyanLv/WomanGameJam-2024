using UnityEngine;

public class MoverTarjeta : MonoBehaviour
{
    public Transform llegarTarjeta;
    public float velocidad;
    public float velocidadRegreso;
    public GameObject rangoMinObject;
    public GameObject rangoMaxObject;

    private bool enMovimiento = false;
    private bool seMovio = false;

    private float rangoHorizontalMin;
    private float rangoHorizontalMax;
    private Vector3 posicionInicial;

    private Transform objetoDetectado; // Nuevo: almacenar el objeto detectado

    void Start()
    {
        // Obtener las posiciones de los objetos que marcan los l�mites
        if (rangoMinObject != null)
        {
            rangoHorizontalMin = rangoMinObject.transform.position.x;
        }

        if (rangoMaxObject != null)
        {
            rangoHorizontalMax = rangoMaxObject.transform.position.x;
        }

        // Almacenar la posici�n inicial al inicio
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enMovimiento && !seMovio)
        {
            // Obtener la posici�n del clic en el mundo
            Vector2 clicPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Verificar si el clic fue en este objeto
            Collider2D collider = Physics2D.OverlapPoint(clicPos);
            if (collider != null && collider.gameObject == gameObject)
            {
                // Iniciar el movimiento hacia el objeto destino
                enMovimiento = true;
            }
        }

        if (enMovimiento)
        {
            if (llegarTarjeta != null)
            {
                // Mover hacia el objeto destino
                transform.position = Vector2.MoveTowards(transform.position, llegarTarjeta.position, velocidad * Time.deltaTime);

                // Verificar si ha llegado al objeto destino
                if (Vector2.Distance(transform.position, llegarTarjeta.position) <= 0.01f)
                {
                    enMovimiento = false;
                    seMovio = true; // Desactivar la capacidad de moverse despu�s de llegar al destino

                    // Actualizar la posici�n inicial al llegar a llegarTarjeta
                    posicionInicial = transform.position;
                }
            }
            else
            {
                Debug.LogError("El objeto de destino no est� asignado en el inspector.");
                enMovimiento = false;
            }
        }

        // Verificar si ya lleg� al objeto destino y se permite el movimiento horizontal
        if (seMovio && Input.GetMouseButton(0))
        {
            float movimientoHorizontal = Input.GetAxis("Mouse X");
            float nuevaPosicionX = transform.position.x + movimientoHorizontal;
            float posicionXClamp = Mathf.Clamp(nuevaPosicionX, rangoHorizontalMin, rangoHorizontalMax);

            transform.position = new Vector3(posicionXClamp, transform.position.y, transform.position.z);
        }

        // Verificar si se solt� el bot�n del mouse y regresar a la posici�n inicial solo si seMovio es verdadero
        if (seMovio && !Input.GetMouseButton(0))
        {
            enMovimiento = false; // Detener el movimiento horizontal
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidadRegreso * Time.deltaTime);
        }

        // Nuevo: verificar si se detect� un objeto y emparentarlo
        if (objetoDetectado != null)
        {
            // Calcular la posici�n relativa al emparentar
            Vector3 posicionRelativa = transform.position - objetoDetectado.position;

            // Emparentar al objeto detectado
            transform.parent = objetoDetectado;

            // Ajustar la posici�n para evitar movimientos bruscos
            transform.position = objetoDetectado.position + posicionRelativa;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Nuevo: verificar si el objeto tiene el tag "DetectoTarjeta"
        if (other.CompareTag("DetectoTarjeta"))
        {
            // Nuevo: almacenar el objeto detectado
            objetoDetectado = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Nuevo: verificar si el objeto tiene el tag "DetectoTarjeta"
        if (other.CompareTag("DetectoTarjeta"))
        {
            // Nuevo: resetear el objeto detectado al salir de la colisi�n
            objetoDetectado = null;
            // Nuevo: resetear el padre al salir de la colisi�n para evitar problemas de posici�n
            transform.parent = null;
        }
    }
}

