using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    // Método que se ejecuta al hacer clic en el botón
    public GameObject pantallaNegra;
    public GameObject pantallaNegraInicio;
    public string nombreEscena;
    private void Start()
    {
        StartCoroutine(DesactivarPantalla());
    }
    public void CambiarEscena()
    {
        StartCoroutine(EsperarCambioEscena());
    }
    IEnumerator EsperarCambioEscena()
    {
        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nombreEscena);
    }
    IEnumerator DesactivarPantalla()
    {
        yield return new WaitForSeconds(1.2f);
        pantallaNegraInicio.SetActive(false);
    }
}
