using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlatillos : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initialPosition;

    void Start()
    {
        // Almacena la posición inicial del objeto al inicio
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            // Restaura la posición inicial solo si el objeto estaba siendo arrastrado
            transform.position = initialPosition;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            transform.position = currentPosition;
        }
    }
}
