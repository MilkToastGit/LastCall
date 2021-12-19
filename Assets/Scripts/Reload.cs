using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void ReloadScene ()
    {
        SceneManager.LoadScene (0);
    }

    public void Quit ()
    {
        Application.Quit ();
    }
}
