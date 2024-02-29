using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlato3 : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    public bool arrastrando = false;
    public bool enPlato = false;
    private GameObject platoActual;

    public Sprite nuevoSpritePlato;

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
        if (enPlato && platoActual != null)
        {
            CambiarSpritePlato(platoActual);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plato3"))
        {
            enPlato = true;
            platoActual = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plato3"))
        {
            enPlato = false;
            platoActual = null;
        }
    }

    private void CambiarSpritePlato(GameObject plato)
    {
        SpriteRenderer spriteRendererPlato = plato.GetComponent<SpriteRenderer>();
        if (spriteRendererPlato != null && nuevoSpritePlato != null)
        {
            spriteRendererPlato.sprite = nuevoSpritePlato;
        }
        else
        {
            Debug.LogError("No se pudo cambiar el sprite del plato");
        }
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Vector3 posicionMouse = Input.mousePosition;
        posicionMouse.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(posicionMouse);
    }
}
