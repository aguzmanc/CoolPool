using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour
{
    void Start()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("EscenaA");

        SceneManager.LoadScene("EscenaA", LoadSceneMode.Additive);
    }
}
