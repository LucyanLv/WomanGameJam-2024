using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene: MonoBehaviour
{
    public static int tagScene;
    public void loadScene()
    {
        SceneManager.LoadScene(tagScene);
    }
}
