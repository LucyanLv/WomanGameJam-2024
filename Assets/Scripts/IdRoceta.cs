using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdRoceta : MonoBehaviour
{
    public int idRoceta;
    public MoverBombillo moverBombillo;
    public bool respuestaCorrecta = false;

    void Update()
    {
        ValidarRespuesta();
    }
    void ValidarRespuesta()
    {
        if (moverBombillo.EmparentadoARoseta)
        {
            if (idRoceta == moverBombillo.iD)
            {
                respuestaCorrecta = true;
                Debug.Log("Los Id coinsiden");
            }
            else
            {
                respuestaCorrecta = false;
                Debug.Log("Los Id no coinsiden");
            }
        }
    }
}
