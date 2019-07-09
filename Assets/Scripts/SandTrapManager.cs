﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandTrapManager : MonoBehaviour
{

    public List<GameObject> SandTrapList = new List<GameObject>();

    public void FindSandTrapsInTheScene() {
        foreach(GameObject GameObjectInTheScene in GameObject.FindObjectsOfType(typeof(GameObject)))
         {
             if(GameObjectInTheScene.GetComponent<SandTrap>() != null)
             SandTrapList.Add (GameObjectInTheScene);
         }  

    }       

}