using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidarPlatoCompleto : MonoBehaviour
{
    public ValidarPlato validarPlato1;
    public ValidarPlato validarPlato2;
    public ValidarPlato validarPlato3;
    public ValidarPlato validarPlato4;
    public ValidarPlato validarVaso;
    public bool armado;
    public bool completo;

    //public float intervaloMensajeMal = 5f;
    //public float tiempoUltimoMensajeMal;

    public GameObject feliz;
    public GameObject enojado;

    private void Start()
    {
        armado = true;
        completo = true;
        //tiempoUltimoMensajeMal = -intervaloMensajeMal;
    }
    void Update()
    {
        if (completo)
        {
            if (validarPlato1.platoCorrecto && validarPlato2.platoCorrecto &&
                validarPlato3.platoCorrecto && validarPlato4.platoCorrecto &&
                validarVaso.platoCorrecto)
            {
                if (armado)
                {
                    Debug.Log("EL PLATO ESTA BIEN ARMADO");
                    armado = false;
                    completo = false;
                    StartCoroutine(Feliz());
                }
            }
            //else
            //{
            //    siguienteValidar.SetActive(true);
            //    // Verificar si ha pasado el tiempo suficiente desde el último mensaje "MAL"
            //    if (Time.time - tiempoUltimoMensajeMal >= intervaloMensajeMal)
            //    {
            //        Debug.Log("MAL");
            //        StartCoroutine(Enojado());
            //        tiempoUltimoMensajeMal = Time.time; // Actualizar el tiempo del último mensaje "MAL"
            //    }
            //}
        }
    }
    IEnumerator Feliz()
    {
        feliz.SetActive(true);
        yield return new WaitForSeconds(3);
        feliz.SetActive(false);
    }
    IEnumerator Enojado()
    {
        enojado.SetActive(true);
        yield return new WaitForSeconds(3);
        enojado.SetActive(false);
    }
}