using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public Sprite primerSprite;  // Asigna el primer sprite en el Inspector
    public Sprite segundoSprite; // Asigna el segundo sprite en el Inspector

    public GameObject objetoConSprite; // Asigna el objeto con SpriteRenderer en el Inspector

    public Sprite primerSpriteOtroObjeto;  // Asigna el primer sprite del otro objeto en el Inspector
    public Sprite segundoSpriteOtroObjeto; // Asigna el segundo sprite del otro objeto en el Inspector

    private SpriteRenderer spriteRenderer;
    public bool apagado = true;
    public bool encendido = false;

    void Start()
    {
        // Obtén la referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Asegúrate de que haya un SpriteRenderer adjunto
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró SpriteRenderer en el objeto.");
        }
    }

    void OnMouseDown()
    {
        CambiarSprite();
        CambiarSpriteOtroObjeto();
    }

    void CambiarSprite()
    {
        // Cambia el sprite según la alternancia entre los dos sprites
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

    void CambiarSpriteOtroObjeto()
    {
        // Verifica si se ha asignado un objeto con SpriteRenderer
        if (objetoConSprite != null)
        {
            // Obtén la referencia al SpriteRenderer del otro objeto
            SpriteRenderer otroObjetoSpriteRenderer = objetoConSprite.GetComponent<SpriteRenderer>();

            // Verifica si el otro objeto tiene un SpriteRenderer
            if (otroObjetoSpriteRenderer != null)
            {
                // Cambia el sprite del otro objeto según la alternancia
                if (apagado)
                {
                    otroObjetoSpriteRenderer.sprite = segundoSpriteOtroObjeto;
                }
                else
                {
                    otroObjetoSpriteRenderer.sprite = primerSpriteOtroObjeto;
                }
            }
            else
            {
                Debug.LogError("No se encontró SpriteRenderer en el objeto asignado.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado un objeto con SpriteRenderer para cambiar el sprite.");
        }
    }
}
