using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBombillo : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    private bool estaSiendoArrastrado = false;
    private bool estaDetectandoEnchufe = false;
    public bool emparentadoARoseta = false;
    public int iD;

    // Almacena la referencia al objeto de Roseta actualmente detectado
    public GameObject rosetaDetectada;

    // Variable para almacenar el objeto emparentado
    public GameObject objetoEmparentado;

    void OnEnable()
    {
        // Guardar la posici�n inicial del objeto al activarse
        posicionInicial = transform.position;
    }

    void OnDisable()
    {
        // Restablecer la posici�n inicial al desactivarse
        transform.position = posicionInicial;
        emparentadoARoseta = false;
        rosetaDetectada = null;
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

        // Si est� detectando el enchufe y est� emparentado con Roseta, activar el booleano
        if (estaDetectandoEnchufe && emparentadoARoseta)
        {
            Debug.Log("El objeto est� emparentado con Roseta y detectando el enchufe.");
            // Almacena el objeto emparentado en la variable correspondiente
            objetoEmparentado = rosetaDetectada;
        }

        if (estaDetectandoEnchufe && !emparentadoARoseta && rosetaDetectada != null)
        {
            // Obtener la posici�n del objeto con el tag "Roseta" almacenado en rosetaDetectada
            transform.position = rosetaDetectada.transform.position;
            emparentadoARoseta = true;
        }
        else
        {
            // Si no est� detectando el enchufe, restablecer la posici�n inicial y desactivar el booleano
            transform.position = posicionInicial;
            emparentadoARoseta = false;
        }

        // Apagar el booleano al soltar el objeto
        estaDetectandoEnchufe = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto colisionado tiene el tag "Roseta"
        if (other.CompareTag("Roseta"))
        {
            estaDetectandoEnchufe = true;

            // Almacenar la referencia al objeto de Roseta detectado m�s recientemente
            rosetaDetectada = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto deja de colisionar con el objeto que tiene el tag "Roseta"
        if (other.CompareTag("Roseta"))
        {
            estaDetectandoEnchufe = false;
            emparentadoARoseta = false;

            // Limpiar la referencia al objeto de Roseta al salir
            rosetaDetectada = null;
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
