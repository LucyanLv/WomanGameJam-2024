using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    public string nombreProximaEscena; // Nombre de la próxima escena
    public GameObject fondoNegro;
    // Este método se llama cuando algo entra en contacto con el collider del objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovimientoPlayer>().validateResponses();
            nombreProximaEscena = collision.gameObject.GetComponent<MovimientoPlayer>().nextLevel;
            StartCoroutine(ActivarPantalla());
        }
    }
    IEnumerator ActivarPantalla()
    {
        fondoNegro.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nombreProximaEscena);
    }
}