using UnityEngine;

public class BandaTransportadora : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    public Transform posicionInicial; // Almacena la posici�n inicial del objeto
    public bool detecto = false;
    private SpriteRenderer spriteRenderer;
    public Sprite volverSprite;

    void Start()
    {
        // Si la posici�n inicial no se asigna desde el Inspector, usa la posici�n inicial del objeto
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
            RestaurarPosicionInicial(); // Aqu� se llama a la funci�n para devolver el objeto a su posici�n inicial
        }
    }

    void OnDisable()
    {
        RestaurarPosicionInicial();
    }

    private void RestaurarPosicionInicial()
    {
        transform.position = posicionInicial.position; // Corregimos aqu� para usar la posici�n inicial asignada desde el Inspector
    }
}
