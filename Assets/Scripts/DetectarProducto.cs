using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarProducto : MonoBehaviour
{
    public Sprite nuevoSprite; // Nuevo sprite a asignar cuando entra en contacto con el objeto que tiene el tag "DetectoProducto"
    public bool detecto;
    public int contador = 0; // Contador que se incrementará cada vez que cambie el sprite
    public bool terminoJuego = false;

    private void Update()
    {
        if (contador > 9)
        {
            terminoJuego = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto entrante tiene el tag "DetectoProducto"
        if (other.CompareTag("DetectoProducto"))
        {
            detecto = true;

            // Cambiar el sprite del objeto que entró en contacto al nuevo sprite
            if (other.GetComponent<SpriteRenderer>() != null && nuevoSprite != null)
            {
                other.GetComponent<SpriteRenderer>().sprite = nuevoSprite;

                // Ajustar la escala para que tenga el mismo tamaño que el objeto
                Vector3 newSize = transform.localScale;
                newSize.x = other.bounds.size.x / nuevoSprite.bounds.size.x;
                newSize.y = other.bounds.size.y / nuevoSprite.bounds.size.y;
                other.transform.localScale = newSize;

                // Incrementar el contador
                contador++;
                Debug.Log("Contador: " + contador);
            }
        }
    }
}
