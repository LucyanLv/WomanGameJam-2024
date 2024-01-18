using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnchufe : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    public bool estaSiendoArrastrado = false;
    public bool estaDetectandoEnchufe = false; // Nuevo booleano para indicar si est� detectando el enchufe

    void OnEnable()
    {
        // Guardar la posici�n inicial del objeto al activarse
        posicionInicial = transform.position;
    }

    void OnDisable()
    {
        // Restablecer la posici�n inicial al desactivarse
        transform.position = posicionInicial;
    }

    void OnMouseDown()
    {
        // Calcular el desplazamiento desde el centro del objeto hasta el punto donde se hizo clic
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        estaSiendoArrastrado = true;
    }

    void OnMouseUp()
    {
        estaSiendoArrastrado = false;

        // Si est� detectando el enchufe, cambiar la posici�n al enchufe
        if (estaDetectandoEnchufe)
        {
            // Obtener la posici�n del objeto con el tag "Enchufe"
            GameObject enchufe = GameObject.FindGameObjectWithTag("Enchufe");
            if (enchufe != null)
            {
                transform.position = enchufe.transform.position;
            }
        }
        else
        {
            // Si no est� detectando el enchufe, restablecer la posici�n inicial
            transform.position = posicionInicial;
        }

        // Apagar el booleano al soltar el objeto
        estaDetectandoEnchufe = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto colisionado tiene el tag "Enchufe"
        if (other.CompareTag("Enchufe"))
        {
            estaDetectandoEnchufe = true; // Encender el booleano al detectar el enchufe
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto deja de colisionar con el objeto que tiene el tag "Enchufe"
        if (other.CompareTag("Enchufe"))
        {
            estaDetectandoEnchufe = false; // Apagar el booleano al salir de la colisi�n con el enchufe
        }
    }

    void Update()
    {
        if (estaSiendoArrastrado)
        {
            // Convertir la posici�n del mouse a coordenadas del mundo
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Aplicar el desplazamiento y actualizar la posici�n del objeto al puntero del mouse
            transform.position = new Vector3(posicionMouse.x + offset.x, posicionMouse.y + offset.y, transform.position.z);
        }
    }
}
