using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    public string nombreProximaEscena; // Nombre de la próxima escena

    // Este método se llama cuando algo entra en contacto con el collider del objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter called");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            SceneManager.LoadScene(nombreProximaEscena);
        }
    }
}