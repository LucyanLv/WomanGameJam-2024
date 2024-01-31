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
    public bool terminoJuegoInterruptor = false;
    public GameObject correcto;

    private void Start()
    {
        terminoJuegoInterruptor = false;
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
                            terminoJuegoInterruptor = true;
                            StartCoroutine(Desactivar());
                        }
                        else
                        {
                            correcto.SetActive(false);
                            terminoJuegoInterruptor = false;
                            Debug.Log("NO HAS COMPLETADO EL JUEGO");
                        }
                    }
                    else
                    {
                        correcto.SetActive(false);
                        terminoJuegoInterruptor = false;
                        Debug.Log("NO HAS COMPLETADO EL JUEGO");
                    }
                }
                else
                {
                    correcto.SetActive(false);
                    terminoJuegoInterruptor = false;
                    Debug.Log("NO HAS COMPLETADO EL JUEGO");
                }
            }
            else
            {
                correcto.SetActive(false);
                terminoJuegoInterruptor = false;
                Debug.Log("NO HAS COMPLETADO EL JUEGO");
            }
        }
        else
        {
            correcto.SetActive(false);
            terminoJuegoInterruptor = false;
            Debug.Log("NO HAS COMPLETADO EL JUEGO");
        }
    }
    IEnumerator Desactivar()
    {
        correcto.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        correcto.SetActive(false);
        terminoJuegoInterruptor = false;
    }
}
