using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Validar : MonoBehaviour
{
    public Interruptor interruptor;
    public Interruptor interruptor1;
    public Interruptor interruptor2;
    public Interruptor interruptor3;
    public Interruptor interruptor4;
    public Interruptor interruptor5;
    public bool terminoJuegoInterruptor = false;

    private SpriteRenderer spriteRenderer;
    public Sprite spriteDefault;
    public Sprite spriteCambio;

    // Prender luz
    public GameObject encenderLuz;

    private void Start()
    {
        terminoJuegoInterruptor = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteDefault;
    }
    private void Update()
    {
        if (interruptor.encendido)
        {
            if (interruptor1.encendido)
            {
                if (interruptor2.encendido)
                {
                    if (interruptor3.encendido)
                    {
                        if (interruptor4.encendido)
                        {
                            if (interruptor5.encendido)
                            {
                                // Cambia el sprite del objeto vinculado si interruptor5 es verdadero
                                spriteRenderer.sprite = interruptor5.encendido ? spriteCambio : spriteDefault;
                                encenderLuz.SetActive(false);
                                terminoJuegoInterruptor = true;
                            }
                            else
                            {
                                // Restaura el sprite original del objeto vinculado si interruptor5 es falso
                                spriteRenderer.sprite = spriteDefault;

                                terminoJuegoInterruptor = false;
                                Debug.Log("NO HAS COMPLETADO EL JUEGO");
                            }
                        }
                        else
                        {
                            terminoJuegoInterruptor = false;
                            Debug.Log("NO HAS COMPLETADO EL JUEGO");
                        }
                    }
                    else
                    {
                        terminoJuegoInterruptor = false;
                        Debug.Log("NO HAS COMPLETADO EL JUEGO");
                    }
                }
                else
                {
                    terminoJuegoInterruptor = false;
                    Debug.Log("NO HAS COMPLETADO EL JUEGO");
                }
            }
            else
            {
                terminoJuegoInterruptor = false;
                Debug.Log("NO HAS COMPLETADO EL JUEGO");
            }
        }
        else
        {
            terminoJuegoInterruptor = false;
            Debug.Log("NO HAS COMPLETADO EL JUEGO");
        }
    }
}
