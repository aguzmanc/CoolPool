using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton3D : MonoBehaviour
{
    MeshRenderer rend;

    public string Escena;

    void Start(){
        rend = GetComponent<MeshRenderer>();
    }


    public void Hover(){
        rend.material.color = Color.red;
    }

    public void Exit(){
        rend.material.color = Color.white;
    }

    public void ClickDown(){
        rend.material.color = Color.blue;

        SceneManager.LoadScene(Escena, LoadSceneMode.Additive);
    }
}
