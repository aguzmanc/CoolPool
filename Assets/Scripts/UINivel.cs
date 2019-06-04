using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINivel : MonoBehaviour
{
   public void NextLevel(){
        GameController.instance.NextScene();
    }

    public void PrevLevel(){
        GameController.instance.PreviousScene();
    }

    public void PlayAgain(){
        GameController.instance.ReloadScene();
    }
}
