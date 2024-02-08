using System.Collections;
using UnityEngine;

public class OperarMaquina : MonoBehaviour
{
    public Animator animadorObjetoAAnimar;
    private bool puedeClic = true;

    [Header("Cambiar Sprite de este Objeto")]
    public float tiempoBloqueo;
    public Sprite spriteNormal;
    public Sprite spriteCambio;

    [Header("Cambiar Sprite de otro Objeto")]
    public SpriteRenderer spriteRenderer;
    public Sprite nuevoSprite;
    public Sprite originalSprite;

    void Start()
    {
        // Asegurarse de que el Animator del objeto a animar existe
        if (animadorObjetoAAnimar == null)
        {
            Debug.LogError("El Animator del objeto a animar no está asignado en el Inspector.");
        }
    }

    void OnMouseDown()
    {
        // Se ejecuta cada vez que se hace clic en el objeto con este script
        if (puedeClic)
        {
            ActivarAnimacion();
            StartCoroutine(BloquearClic());
        }
    }

    void ActivarAnimacion()
    {
        // Activar la animación en el objeto deseado
        animadorObjetoAAnimar.SetTrigger("Activar");
        // Cambiar el sprite del objeto a spriteCambio
        GetComponent<SpriteRenderer>().sprite = spriteCambio;
        spriteRenderer.sprite = nuevoSprite;
    }

    IEnumerator BloquearClic()
    {
        // Bloquear clic durante un tiempo determinado
        puedeClic = false;
        yield return new WaitForSeconds(tiempoBloqueo);
        // Cambiar el sprite del objeto a spriteNormal
        GetComponent<SpriteRenderer>().sprite = spriteNormal;
        spriteRenderer.sprite = originalSprite;
        puedeClic = true;
    }
}
