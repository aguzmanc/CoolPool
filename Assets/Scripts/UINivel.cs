using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINivel : MonoBehaviour
{
    public void NextLevel(){
        GameManager.instance.NextScene();
    }

    public void PrevLevel(){
        GameManager.instance.PreviousScene();
    }

    public void PlayAgain(){
        GameManager.instance.ReloadScene();
    }
}
