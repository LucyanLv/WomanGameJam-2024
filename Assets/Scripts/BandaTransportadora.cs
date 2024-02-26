using UnityEngine;

public class BandaTransportadora : MonoBehaviour
{
    public float velocidad = 5f;
    public Transform posicionInicial;
    public bool detecto = false;
    private SpriteRenderer spriteRenderer;
    public Sprite volverSprite;

    void Start()
    {
        if (posicionInicial == null)
        {
            posicionInicial = transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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
