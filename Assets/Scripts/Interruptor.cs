using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public Sprite primerSprite;  // Asigna el primer sprite en el Inspector
    public Sprite segundoSprite; // Asigna el segundo sprite en el Inspector

    private SpriteRenderer spriteRenderer;
    public bool apagado = true;
    public bool encendido = false;

    void Start()
    {
        // Obt�n la referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Aseg�rate de que haya un SpriteRenderer adjunto
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontr� SpriteRenderer en el objeto.");
        }
    }

    void OnMouseDown()
    {
        CambiarSprite();
    }

    void CambiarSprite()
    {
        // Cambia el sprite seg�n la alternancia entre los dos sprites
        if (apagado)
        {
            spriteRenderer.sprite = segundoSprite;
            encendido = true;
        }
        else
        {
            spriteRenderer.sprite = primerSprite;
            encendido = false;
        }

        // Cambia el estado de la alternancia
        apagado = !apagado;
    }
}
