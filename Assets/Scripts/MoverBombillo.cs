using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBombillo : MonoBehaviour
{
    private bool isMouseDrag;
    private Vector3 offset;
    private Vector3 initialPosition;
    public bool detecto;
    public string nombreObjetoEnContacto;
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

        transform.position = initialPosition;
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

            nombreObjetoEnContacto = other.gameObject.name;

            Debug.Log("en contacto con " + nombreObjetoEnContacto);

            if (nombreObjetoEnContacto == id)
            {
                Debug.Log("¡coincide con el ID!");
            }
            else
            {
                Debug.Log("El nombre del objeto en contacto NO coincide con el ID.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Roseta"))
        {
            detecto = false;
            Debug.Log("El nombre del objeto en contacto NO coincide con el ID.");
        }
    }
}
