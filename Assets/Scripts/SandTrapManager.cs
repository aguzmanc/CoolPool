using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandTrapManager : Singleton<SandTrapManager> {

    public List<GameObject> SandTrapList = new List<GameObject>();


    public void FindSandTraps() {
        foreach(GameObject GameObjectInTheScene in GameObject.FindObjectsOfType(typeof(GameObject)))
         {
             if(GameObjectInTheScene.GetComponent<SandTrap>() != null)
             SandTrapList.Add (GameObjectInTheScene);
         } 
    
    }
    public void ClearList() {
        SandTrapList.Clear();
    }       

}