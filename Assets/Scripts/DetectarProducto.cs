using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectarProducto : MonoBehaviour
{
    public Sprite nuevoSprite;
    public bool detecto;
    public int contador = 0;
    public bool terminoJuegoMaquina = false;
    private Vector3 tamanoOriginal;

    [Header("Sprite Correcto")]
    public SpriteRenderer spriteCorrecto;
    public Sprite luzVerde;

    [Header("Sprite Incorrecto")]
    public SpriteRenderer spriteIncorrecto;
    public Sprite luzRoja;

    [Header("Mensajes En MiniJuego")]
    public TMP_Text numeroCajas;
    public string caja1;
    public string caja2;
    public string caja3;
    public string caja4;
    public string caja5;
    public string caja6;
    public string caja7;
    public string caja8;
    public string caja9;
    public string caja10;

    public Color tareaRealizada;
    public Color tareaAMedia;
    public Color tareaCasiCompleta;
    private void Start()
    {
        // Guardar el tamaño original del objeto al inicio
        tamanoOriginal = transform.localScale;
    }

    private void Update()
    {
        if (contador == 1)
        {
            numeroCajas.text = caja1;
        }
        if (contador == 2)
        {
            numeroCajas.text = caja2;
        }
        if (contador == 3)
        {
            numeroCajas.text = caja3;
            numeroCajas.color = tareaCasiCompleta;
        }
        if (contador == 4)
        {
            numeroCajas.text = caja4;
        }
        if (contador == 5)
        {
            numeroCajas.text = caja5;
        }
        if (contador == 6)
        {
            numeroCajas.text = caja6;
            numeroCajas.color = tareaAMedia;
        }
        if (contador == 7)
        {
            numeroCajas.text = caja7;
        }
        if (contador == 8)
        {
            numeroCajas.text = caja8;
        }
        if (contador == 9)
        {
            numeroCajas.text = caja9;
        }
        if (contador >= 10)
        {
            numeroCajas.text = caja10;
            numeroCajas.color = tareaRealizada;
            StartCoroutine(Desactivar());
            spriteCorrecto.sprite = luzVerde;
            spriteIncorrecto.sprite = luzRoja;
            terminoJuegoMaquina = true;
            StartCoroutine(Desactivar());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto entrante tiene el tag "DetectoProducto"
        if (other.CompareTag("DetectoProducto"))
        {
            detecto = true;

            // Guardar el tamaño actual del objeto antes de cambiar el sprite
            Vector3 tamanoActual = transform.localScale;

            // Cambiar el sprite del objeto que entró en contacto al nuevo sprite
            if (other.GetComponent<SpriteRenderer>() != null && nuevoSprite != null)
            {
                other.GetComponent<SpriteRenderer>().sprite = nuevoSprite;

                // Restaurar el tamaño original del objeto
                transform.localScale = tamanoOriginal;

                // Incrementar el contador
                contador++;
                Debug.Log("Contador: " + contador);
            }
        }
    }

    IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(1.5f);
    }

    private void OnDisable()
    {
        terminoJuegoMaquina = false;
    }
}