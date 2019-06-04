using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINivel : MonoBehaviour
{
   public void SiguienteNivel(){
        //ChangeLevel level = GameObject.FindObjectOfType<ChangeLevel>();

        //ChangeLevel.GetInstance().NextScene();
        ChangeLevel.instance.NextScene();
        //ChangeLevel.instance.NextScene();

        //ChangeLevel.NextScene();
        //level.NextScene();

        //ChangeLevel._instance = null; // no es posible acceder
    }

    public void AnteriorNivel(){}

    public void JugarDeNuevo(){}
}
