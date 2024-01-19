using UnityEngine;

public class MoverTarjeta : MonoBehaviour
{
    public Transform llegarTarjeta; // Objeto de destino con el componente Transform
    public float velocidad = 5f; // Velocidad de movimiento
    public float velocidadHorizontal = 2f; // Velocidad de movimiento horizontal despu�s de llegar a llegarTarjeta
    public GameObject rangoMinObject; // Objeto que marca el l�mite m�nimo horizontal
    public GameObject rangoMaxObject; // Objeto que marca el l�mite m�ximo horizontal

    private bool enMovimiento = false;
    private bool seMovio = false; // Nueva variable para controlar si ya se movi�

    private float rangoHorizontalMin;
    private float rangoHorizontalMax;

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
            float nuevaPosicionX = transform.position.x + movimientoHorizontal * velocidadHorizontal * Time.deltaTime;
            float posicionXClamp = Mathf.Clamp(nuevaPosicionX, rangoHorizontalMin, rangoHorizontalMax);

            transform.position = new Vector3(posicionXClamp, transform.position.y, transform.position.z);
        }
    }
}
