using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarProducto : MonoBehaviour
{
    public Sprite nuevoSprite; // Nuevo sprite a asignar cuando entra en contacto con el objeto que tiene el tag "DetectoProducto"
    public bool detecto;
    public int contador = 0; // Contador que se incrementará cada vez que cambie el sprite
    public bool terminoJuegoMaquina = false;
    private Vector3 tamanoOriginal; // Almacena el tamaño original del objeto
    public GameObject correcto;

    private void Start()
    {
        // Guardar el tamaño original del objeto al inicio
        tamanoOriginal = transform.localScale;
    }

    private void Update()
    {
        if (contador == 10)
        {
            terminoJuegoMaquina = true;
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
        correcto.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        correcto.SetActive(false);

        // Restaurar el valor de terminoJuego cuando el objeto se desactiva
        terminoJuegoMaquina = false;
    }

    // Método que se llama cuando el objeto se desactiva
    private void OnDisable()
    {
        // Restaurar el valor de terminoJuego cuando el objeto se desactiva
        terminoJuegoMaquina = false;
    }
}
