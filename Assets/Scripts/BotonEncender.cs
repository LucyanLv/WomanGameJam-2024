using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEncender : MonoBehaviour
{
    public MoverBombillo moverBombillo1;
    public MoverBombillo moverBombillo2;
    public MoverBombillo moverBombillo3;
    public MoverBombillo moverBombillo4;
    public float desactivarJuego;

    public bool terminoJuegoLuz = false;
    public GameObject correcto;
    public GameObject incorrecto;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = GetComponent<Collider2D>();

            if (collider.OverlapPoint(mousePosition))
            {
                Debug.Log("Objeto clickeado: " + gameObject.name);
                if (moverBombillo1.detecto && moverBombillo1.objetoEnContacto == moverBombillo1.id)
                {
                    if (moverBombillo2.detecto && moverBombillo2.objetoEnContacto == moverBombillo2.id)
                    {
                        if (moverBombillo3.detecto && moverBombillo3.objetoEnContacto == moverBombillo3.id)
                        {
                            if (moverBombillo4.detecto && moverBombillo4.objetoEnContacto == moverBombillo4.id)
                            {
                                Debug.Log("Ganaste mini juego");
                                terminoJuegoLuz = true;
                                StartCoroutine(Correcto());
                            }
                            else
                            {
                                StartCoroutine(Incorrecto());
                                Debug.Log("Aun no puedes salir");
                            }
                        }
                        else
                        {
                            StartCoroutine(Incorrecto());
                            Debug.Log("Aun no puedes salir");
                        }
                    }
                    else
                    {
                        StartCoroutine(Incorrecto());
                        Debug.Log("Aun no puedes salir");
                    }
                }
                else
                {
                    StartCoroutine(Incorrecto());
                    Debug.Log("Aun no puedes salir");
                }
            }
        }
    }

    void OnDisable()
    {
        // Al desactivarse, establecer terminoJuego a false
        terminoJuegoLuz = false;
    }
    IEnumerator Correcto()
    {
        correcto.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        correcto.SetActive(false);
        terminoJuegoLuz = false;
    }
    IEnumerator Incorrecto()
    {
        incorrecto.SetActive(true);
        correcto.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        incorrecto.SetActive(false);
    }
}
