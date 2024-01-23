using UnityEngine;

public class MoverTarjeta : MonoBehaviour
{
    public Transform objetivoDestino;
    private Vector3 posicionInicial; // Nueva variable para almacenar la posición inicial
    public bool enMovimiento = false;
    public bool haLlegado = false;
    public bool puedeMoverHorizontal = false;
    public float velocidad = 5f;

    void Start()
    {
        // Al inicio, guarda la posición inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Verificar si el usuario está manteniendo presionado el clic sobre el objeto
        if (Input.GetMouseButton(0))
        {
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!enMovimiento && !haLlegado)
                {
                    // Iniciar el movimiento hacia el objetivo si no está en movimiento ni ha llegado
                    enMovimiento = true;
                }
                else if (haLlegado)
                {
                    // Habilitar el movimiento horizontal si ha llegado y se mantiene clic
                    puedeMoverHorizontal = true;
                }
            }
            else
            {
                // Desactivar el movimiento horizontal si se mantiene clic fuera del objeto
                puedeMoverHorizontal = false;
            }
        }
        else
        {
            // Desactivar el movimiento horizontal si se suelta el clic
            puedeMoverHorizontal = false;
        }

        // Mover el objeto hacia el objetivo si está en movimiento
        if (enMovimiento)
        {
            MoverHaciaObjetivo();
        }

        // Mover horizontalmente solo si ha llegado y se mantiene clic sobre el objeto
        if (puedeMoverHorizontal)
        {
            MoverHorizontalConMouse();
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

            // Actualizar la posición inicial al llegar al destino
            posicionInicial = transform.position;
        }
    }

    void MoverHorizontalConMouse()
    {
        // Obtener la posición del mouse en el mundo y asignarla al objeto en el eje X
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(posicionMouse.x, transform.position.y, transform.position.z);
    }
}