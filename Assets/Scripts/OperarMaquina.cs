using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperarMaquina : MonoBehaviour
{
    public Animator animadorObjetoAAnimar; // Animator del objeto que se va a animar asignado desde el Inspector
    public float tiempoBloqueo = 1.0f; // Tiempo en segundos antes de permitir otro clic

    private bool puedeClic = true; // Variable para controlar si se puede hacer clic

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
        animadorObjetoAAnimar.SetTrigger("Activar"); // Asegúrate de tener un trigger en el Animator con el nombre adecuado
    }

    IEnumerator BloquearClic()
    {
        // Bloquear clic durante un tiempo determinado
        puedeClic = false;
        yield return new WaitForSeconds(tiempoBloqueo);
        puedeClic = true;
    }
}
