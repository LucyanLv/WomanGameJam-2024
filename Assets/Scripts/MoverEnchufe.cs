using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnchufe : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 offset;
    public bool estaSiendoArrastrado = false;
    public bool estaDetectandoEnchufe = false;
    public bool emparentadoAEnchufe = false;
    public Transform objetoApuntar;
    private SpriteRenderer spriteRenderer;
    public Sprite enchufado;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        posicionInicial = transform.position;
    }

    void OnDisable()
    {
        transform.position = posicionInicial;
        emparentadoAEnchufe = false;
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        estaSiendoArrastrado = true;
    }

    void OnMouseUp()
    {
        estaSiendoArrastrado = false;

        if (estaDetectandoEnchufe && emparentadoAEnchufe)
        {
            Debug.Log("El objeto está emparentado con el enchufe y detectando el enchufe.");
        }

        if (estaDetectandoEnchufe)
        {
            GameObject enchufe = GameObject.FindGameObjectWithTag("Enchufe");
            if (enchufe != null)
            {
                transform.position = enchufe.transform.position;
                emparentadoAEnchufe = true;
            }
        }
        else
        {
            transform.position = posicionInicial;
        }

        estaDetectandoEnchufe = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enchufe"))
        {
            estaDetectandoEnchufe = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enchufe"))
        {
            estaDetectandoEnchufe = false;
            emparentadoAEnchufe = false;
        }
    }

    void Update()
    {
        if (estaSiendoArrastrado)
        {
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(posicionMouse.x + offset.x, posicionMouse.y + offset.y, transform.position.z);
        }

        // Apuntar hacia el objeto definido en el Inspector
        if (objetoApuntar != null)
        {
            Vector3 direccion = objetoApuntar.position - transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        }
        if (emparentadoAEnchufe)
        {
            spriteRenderer.sprite = enchufado;
        }
    }
}
