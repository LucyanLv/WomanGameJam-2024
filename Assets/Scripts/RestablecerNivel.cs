using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestablecerNivel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Verifica si se ha presionado la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Llama a la funci�n para restablecer la escena
            RestablecerEscena();
        }
    }

    // Funci�n para restablecer la escena
    void RestablecerEscena()
    {
        // Obtiene el �ndice de la escena actual
        int escenaActual = SceneManager.GetActiveScene().buildIndex;

        // Carga la escena actual
        SceneManager.LoadScene(escenaActual);
    }
}
