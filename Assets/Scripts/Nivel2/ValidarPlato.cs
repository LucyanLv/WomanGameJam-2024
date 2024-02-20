using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidarPlato : MonoBehaviour
{
    public string nombreSpriteValido;
    public bool platoCorrecto = false;
    

    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite.name == nombreSpriteValido)
        {
            Debug.Log("El nombre del sprite es válido.");
            platoCorrecto = true;
        }
        else
        {
            platoCorrecto = false;
        }
    }
}
