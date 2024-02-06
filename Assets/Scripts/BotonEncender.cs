using System.Collections;
using UnityEngine;

public class BotonEncender : MonoBehaviour
{
    public MoverBombillo moverBombillo1;
    public MoverBombillo moverBombillo2;
    public MoverBombillo moverBombillo3;
    public MoverBombillo moverBombillo4;
    public float desactivarJuego;

    public bool terminoJuegoLuz = false;

    public SpriteRenderer spriteRenderer;
    public Sprite nuevoSprite;
    public Sprite spriteOriginal;
    public float tiempoCambioSprite;
    public GameObject objetoACambiarSprite;
    public Sprite nuevoSpriteObjeto;

    void Start()
    {
        // Asegúrate de que el objeto tenga un componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Almacena el sprite original
        spriteOriginal = spriteRenderer.sprite;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = GetComponent<Collider2D>();

            if (collider.OverlapPoint(mousePosition))
            {
                CambiarSpriteObjeto();

                Debug.Log("Objeto clickeado: " + gameObject.name);
                if (moverBombillo1.detecto && moverBombillo1.objetoEnContacto == moverBombillo1.id)
                {
                    if (moverBombillo2.detecto && moverBombillo2.objetoEnContacto == moverBombillo2.id)
                    {
                        if (moverBombillo3.detecto && moverBombillo3.objetoEnContacto == moverBombillo3.id)
                        {
                            if (moverBombillo4.detecto && moverBombillo4.objetoEnContacto == moverBombillo4.id)
                            {
                                Debug.Log("Ganaste mini juego");
                                terminoJuegoLuz = true;

                                CambiarSpriteOtroObjeto();
                                StartCoroutine(Correcto());
                            }
                            else
                            {
                                Debug.Log("Aun no puedes salir");
                            }
                        }
                        else
                        {
                            Debug.Log("Aun no puedes salir");
                        }
                    }
                    else
                    {
                        Debug.Log("Aun no puedes salir");
                    }
                }
                else
                {
                    Debug.Log("Aun no puedes salir");
                }
            }
            else
            {
                Debug.Log("Aun no puedes salir");
            }
        }
    }

    void OnDisable()
    {
        // Al desactivarse, establecer terminoJuego a false
        terminoJuegoLuz = false;
    }

    IEnumerator Correcto()
    {
        yield return new WaitForSeconds(1.2f);

        // Vuelve al sprite original después de un tiempo
        VolverSpriteOriginal();
    }

    // Método para cambiar el sprite del objeto actual cuando se cumple la condición
    void CambiarSpriteObjeto()
    {
        // Asegúrate de que el objeto tenga un componente SpriteRenderer
        if (spriteRenderer != null)
        {
            // Cambia el sprite del objeto al nuevo sprite definido en el Inspector
            spriteRenderer.sprite = nuevoSprite;
            StartCoroutine(VolverSpriteOriginal());
        }
        else
        {
            Debug.LogError("El objeto no tiene un componente SpriteRenderer.");
        }
    }

    // Método para cambiar el sprite de otro objeto cuando se cumple la condición
    void CambiarSpriteOtroObjeto()
    {
        // Asegúrate de que el objeto a cambiar tenga un componente SpriteRenderer
        if (objetoACambiarSprite != null)
        {
            SpriteRenderer spriteRendererObjeto = objetoACambiarSprite.GetComponent<SpriteRenderer>();

            // Asegúrate de que el objeto a cambiar tenga un componente SpriteRenderer
            if (spriteRendererObjeto != null)
            {
                // Cambia el sprite del objeto al nuevo sprite definido en el Inspector
                spriteRendererObjeto.sprite = nuevoSpriteObjeto;
            }
            else
            {
                Debug.LogError("El objeto a cambiar de sprite no tiene un componente SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogError("La referencia al objeto a cambiar de sprite no está asignada.");
        }
    }

    IEnumerator VolverSpriteOriginal()
    {
        yield return new WaitForSeconds(tiempoCambioSprite);
        if (spriteRenderer != null)
        {
            // Vuelve al sprite original
            spriteRenderer.sprite = spriteOriginal;
        }
        else
        {
            Debug.LogError("El objeto no tiene un componente SpriteRenderer.");
        }
    }
}
