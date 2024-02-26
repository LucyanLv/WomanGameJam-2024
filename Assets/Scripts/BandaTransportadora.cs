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
