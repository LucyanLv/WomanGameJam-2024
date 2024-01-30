using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    private Vector3 posicionInicial;
    private bool estaSiendoArrastrado = false;

    void OnMouseDown()
    {
        // Al hacer clic en el objeto
        posicionInicial = transform.position;
        estaSiendoArrastrado = true;
    }

    void OnMouseUp()
    {
        // Al soltar el clic
        estaSiendoArrastrado = false;
    }

    void Update()
    {
        if (estaSiendoArrastrado)
        {
            // Convertir la posición del mouse a coordenadas del mundo
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Actualizar la posición del objeto al puntero del mouse
            transform.position = new Vector3(posicionMouse.x, posicionMouse.y, transform.position.z);
        }
    }
}
