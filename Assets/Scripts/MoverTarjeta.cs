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

    private Transform objetoDetectado;

    void Start()
    {
        if (rangoMinObject != null)
        {
            rangoHorizontalMin = rangoMinObject.transform.position.x;
        }

        if (rangoMaxObject != null)
        {
            rangoHorizontalMax = rangoMaxObject.transform.position.x;
        }

        posicionInicial = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enMovimiento && !seMovio)
        {
            Vector2 clicPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(clicPos);
            if (collider != null && collider.gameObject == gameObject)
            {
                enMovimiento = true;
            }
        }

        if (enMovimiento)
        {
            if (llegarTarjeta != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, llegarTarjeta.position, velocidad * Time.deltaTime);

                if (Vector2.Distance(transform.position, llegarTarjeta.position) <= 0.01f)
                {
                    enMovimiento = false;
                    seMovio = true;
                    posicionInicial = transform.position;
                }
            }
            else
            {
                Debug.LogError("El objeto de destino no está asignado en el inspector.");
                enMovimiento = false;
            }
        }

        if (seMovio && Input.GetMouseButton(0) && objetoDetectado == null)
        {
            float movimientoHorizontal = Input.GetAxis("Mouse X");
            float nuevaPosicionX = transform.position.x + movimientoHorizontal;
            float posicionXClamp = Mathf.Clamp(nuevaPosicionX, rangoHorizontalMin, rangoHorizontalMax);

            transform.position = new Vector3(posicionXClamp, transform.position.y, transform.position.z);
        }

        if (seMovio && !Input.GetMouseButton(0))
        {
            enMovimiento = false;
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidadRegreso * Time.deltaTime);
        }

        if (objetoDetectado != null)
        {
            Vector3 posicionRelativa = transform.position - objetoDetectado.position;
            transform.parent = objetoDetectado;
            transform.position = objetoDetectado.position + posicionRelativa;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DetectoTarjeta"))
        {
            objetoDetectado = other.transform;
            posicionInicial = objetoDetectado.position;
            seMovio = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DetectoTarjeta"))
        {
            objetoDetectado = null;
            transform.parent = null;
            posicionInicial = transform.position;
            seMovio = false;
        }
    }
}
