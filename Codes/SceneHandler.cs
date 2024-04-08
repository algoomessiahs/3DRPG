using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadAwesome()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void LoadOld()
    {
        SceneManager.LoadScene(0);
    }
}
