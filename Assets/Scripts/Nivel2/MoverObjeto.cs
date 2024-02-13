using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    public bool arrastrando = false;
    public bool enPlato = false;

    public GameObject objetoACambiarSprite;
    public Sprite nuevoSpriteEnPlato;

    private SpriteRenderer spriteRendererObjetoACambiar;

    private void Start()
    {
        posicionInicial = transform.position;

        // Obtener el SpriteRenderer del objeto a cambiar sprite
        spriteRendererObjetoACambiar = objetoACambiarSprite.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        offset = transform.position - ObtenerPosicionMouse();
        arrastrando = true;
    }

    private void OnMouseUp()
    {
        arrastrando = false;
        // Siempre regresa a la posición inicial
        transform.position = posicionInicial;

        if (enPlato && nuevoSpriteEnPlato != null && spriteRendererObjetoACambiar != null)
        {
            spriteRendererObjetoACambiar.sprite = nuevoSpriteEnPlato;
        }
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
        if (collision.CompareTag("Plato"))
        {
            enPlato = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plato"))
        {
            enPlato = false;
        }
    }

    private Vector3 ObtenerPosicionMouse()
    {
        Vector3 posicionMouse = Input.mousePosition;
        posicionMouse.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(posicionMouse);
    }
}
