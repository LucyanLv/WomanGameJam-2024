using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    private bool arrastrando = false;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void OnMouseDown()
    {
        offset = transform.position - ObtenerPosicionMouse();
        arrastrando = true;
    }

    private void OnMouseUp()
    {
        arrastrando = false;
        transform.position = posicionInicial;
    }

    private void Update()
    {
        if (arrastrando)
        {
            Vector3 nuevaPosicion = ObtenerPosicionMouse() + offset;
            transform.position = nuevaPosicion;
        }
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Vector3 posicionMouse = Input.mousePosition;
        posicionMouse.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(posicionMouse);
    }
}