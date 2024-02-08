using UnityEngine;

public class BandaTransportadora : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    public Transform posicionInicial; // Almacena la posición inicial del objeto
    public bool detecto = false;
    private SpriteRenderer spriteRenderer;
    public Sprite volverSprite;

    void Start()
    {
        // Si la posición inicial no se asigna desde el Inspector, usa la posición inicial del objeto
        if (posicionInicial == null)
            posicionInicial = transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mover el objeto hacia la derecha
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Devolverse"))
        {
            detecto = true;
            spriteRenderer.sprite = volverSprite;
            RestaurarPosicionInicial(); // Aquí se llama a la función para devolver el objeto a su posición inicial
        }
    }

    void OnDisable()
    {
        RestaurarPosicionInicial();
    }

    private void RestaurarPosicionInicial()
    {
        transform.position = posicionInicial.position; // Corregimos aquí para usar la posición inicial asignada desde el Inspector
    }
}
