using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBombillo : MonoBehaviour
{
    private bool isMouseDrag;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Transform parentRoseta;

    public bool detecto;
    public string objetoEnContacto;
    public string id;

    public GameObject objetoConSprite;  
    public Sprite spriteEmparentado; 
    public Sprite spriteNoEmparentado;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteEste;

    public Sprite focoApagado;
    public Sprite focoEncendido;

    void Start()
    {
        initialPosition = transform.position;

        // Almacena el SpriteRenderer del objeto original
        spriteRenderer = objetoConSprite.GetComponent<SpriteRenderer>();
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
            spriteEste.sprite = focoEncendido;
            Vector3 centeredPosition = parentRoseta.position;
            transform.SetParent(parentRoseta);
            transform.position = centeredPosition;

            // Cambia el sprite del objeto con SpriteRenderer cuando está emparentado y las variables son iguales
            if (objetoEnContacto == id)
            {
                spriteRenderer.sprite = spriteEmparentado;
            }
        }
        else
        {

            transform.position = initialPosition;
            transform.SetParent(null);

            // Cambia el sprite del objeto con SpriteRenderer cuando no está emparentado y las variables son iguales
            if (objetoEnContacto == id)
            {
                spriteRenderer.sprite = spriteNoEmparentado;
            }
            spriteEste.sprite = focoApagado;

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
            parentRoseta = other.transform;
            objetoEnContacto = other.gameObject.name;

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
            parentRoseta = null;

            // Cambia el sprite del objeto con SpriteRenderer cuando no está emparentado y las variables son iguales
            if (objetoEnContacto == id)
            {
                spriteRenderer.sprite = spriteNoEmparentado;
            }
            Debug.Log("El nombre del objeto en contacto NO coincide con el ID.");
        }
    }
}
