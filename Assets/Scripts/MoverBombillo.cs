using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBombillo : MonoBehaviour
{
    private bool isMouseDrag;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Transform parentRoseta;  // Agrega una variable para almacenar la referencia al objeto Roseta

    public bool detecto;
    public string objetoEnContacto;
    public string id;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        isMouseDrag = true;
    }

    void OnMouseUp()
    {
        isMouseDrag = false;

        if (detecto && parentRoseta != null && parentRoseta.childCount == 0)
        {
            // Verifica que no haya un hijo emparentado con el objeto Roseta

            // Calcula la posición centrada en el objeto Roseta
            Vector3 centeredPosition = parentRoseta.position;

            // Establece el objeto actual como hijo del objeto Roseta en la posición centrada
            transform.SetParent(parentRoseta);
            transform.position = centeredPosition;
        }
        else
        {
            // Si ya hay un hijo emparentado o no estás sobre un objeto Roseta, regresa a la posición inicial
            transform.position = initialPosition;
            // Desemparenta el objeto al salir de la zona de la Roseta
            transform.SetParent(null);
        }
    }

    void Update()
    {
        if (isMouseDrag)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            transform.position = currentPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Roseta"))
        {
            detecto = true;

            // Almacena la referencia al objeto Roseta
            parentRoseta = other.transform;

            objetoEnContacto = other.gameObject.name;

            //Debug.Log("en contacto con " + objetoEnContacto);

            if (objetoEnContacto == id)
            {
                Debug.Log("¡coincide con el ID!");
            }
            else
            {
                Debug.Log("NO coincide con el ID.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Roseta"))
        {
            detecto = false;
            parentRoseta = null;  // Resetea la referencia al objeto Roseta
            Debug.Log("El nombre del objeto en contacto NO coincide con el ID.");
        }
    }
}
