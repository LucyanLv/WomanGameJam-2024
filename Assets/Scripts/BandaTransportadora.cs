using UnityEngine;

public class BandaTransportadora : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    private Vector3 posicionInicial; // Almacena la posición inicial del objeto
    public bool detecto = false;

    void Start()
    {
        // Guardar la posición inicial al inicio del juego
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Mover el objeto hacia la derecha
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto entrante tiene el tag "Devolverse"
        if (other.CompareTag("Devolverse"))
        {
            detecto = true;
            // Restaurar la posición inicial cuando entre en contacto con el tag "Devolverse"
            RestaurarPosicionInicial();
        }
    }

    void OnDisable()
    {
        // Restaurar la posición inicial cuando el objeto se desactiva
        RestaurarPosicionInicial();
    }

    // Método para restablecer la posición inicial
    private void RestaurarPosicionInicial()
    {
        transform.position = posicionInicial;
    }
}
