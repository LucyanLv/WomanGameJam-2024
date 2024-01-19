using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBombillo : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    public bool estaSiendoArrastrado = false;
    public bool estaDetectandoEnchufe = false;
    public bool EmparentadoARoseta = false;
    public int iD;

    void OnEnable()
    {
        // Guardar la posición inicial del objeto al activarse
        posicionInicial = transform.position;
    }

    void OnDisable()
    {
        // Restablecer la posición inicial al desactivarse
        transform.position = posicionInicial;
        EmparentadoARoseta = false; // Resetear el booleano al desactivarse
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

        // Si está detectando el enchufe y está emparentado con Roseta, activar el booleano
        if (estaDetectandoEnchufe && EmparentadoARoseta)
        {
            Debug.Log("El objeto está emparentado con Roseta y detectando el enchufe.");
        }

        if (estaDetectandoEnchufe && !EmparentadoARoseta)
        {
            // Obtener la posición del objeto con el tag "Roseta"
            GameObject Roseta = GameObject.FindGameObjectWithTag("Roseta");
            if (Roseta != null)
            {
                transform.position = Roseta.transform.position;
                EmparentadoARoseta = true; // Activar el booleano al emparentarse
            }
        }
        else
        {
            // Si no está detectando el enchufe, restablecer la posición inicial y desactivar el booleano
            transform.position = posicionInicial;
            EmparentadoARoseta = false;
        }

        // Apagar el booleano al soltar el objeto
        estaDetectandoEnchufe = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto colisionado tiene el tag "Roseta"
        if (other.CompareTag("Roseta"))
        {
            estaDetectandoEnchufe = true; // Encender el booleano al detectar el enchufe
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto deja de colisionar con el objeto que tiene el tag "Roseta"
        if (other.CompareTag("Roseta"))
        {
            estaDetectandoEnchufe = false; // Apagar el booleano al salir de la colisión con el enchufe
            EmparentadoARoseta = false; // Resetear el booleano al salir de la colisión con Roseta
        }
    }

    void Update()
    {
        if (estaSiendoArrastrado)
        {
            // Convertir la posición del mouse a coordenadas del mundo
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Aplicar el desplazamiento y actualizar la posición del objeto al puntero del mouse
            transform.position = new Vector3(posicionMouse.x + offset.x, posicionMouse.y + offset.y, transform.position.z);
        }
    }
}
