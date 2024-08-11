using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    public List<GameObject> objetos;

    public float tiempoEspera = 0.5f;

    private int objetoActivoActual;

    void Start()
    {
        StartCoroutine(CambiarObjetosAleatoriamente());
    }

    IEnumerator CambiarObjetosAleatoriamente()
    {
        while (true)
        {
            objetos[objetoActivoActual].SetActive(false);

            objetoActivoActual = Random.Range(0, objetos.Count);

            objetos[objetoActivoActual].SetActive(true);

            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
