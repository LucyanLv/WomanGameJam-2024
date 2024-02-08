using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarProducto : MonoBehaviour
{
    public Sprite nuevoSprite;
    public bool detecto;
    public int contador = 0;
    public bool terminoJuegoMaquina = false;
    private Vector3 tamanoOriginal;

    [Header("Sprite Correcto")]
    public SpriteRenderer spriteCorrecto;
    public Sprite luzVerde;

    [Header("Sprite Incorrecto")]
    public SpriteRenderer spriteIncorrecto;
    public Sprite luzRoja;

    private void Start()
    {
        // Guardar el tamaño original del objeto al inicio
        tamanoOriginal = transform.localScale;
    }

    private void Update()
    {
        if (contador >= 10)
        {
            terminoJuegoMaquina = true;
            spriteCorrecto.sprite = luzVerde;
            spriteIncorrecto.sprite = luzRoja;
            StartCoroutine(Desactivar());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto entrante tiene el tag "DetectoProducto"
        if (other.CompareTag("DetectoProducto"))
        {
            detecto = true;

            // Guardar el tamaño actual del objeto antes de cambiar el sprite
            Vector3 tamanoActual = transform.localScale;

            // Cambiar el sprite del objeto que entró en contacto al nuevo sprite
            if (other.GetComponent<SpriteRenderer>() != null && nuevoSprite != null)
            {
                other.GetComponent<SpriteRenderer>().sprite = nuevoSprite;

                // Restaurar el tamaño original del objeto
                transform.localScale = tamanoOriginal;

                // Incrementar el contador
                contador++;
                Debug.Log("Contador: " + contador);
            }
        }
    }

    IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(1.2f);
    }

    private void OnDisable()
    {
        terminoJuegoMaquina = false;
    }
}