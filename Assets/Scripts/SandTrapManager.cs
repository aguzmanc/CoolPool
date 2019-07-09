using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandTrapManager : MonoBehaviour
{

    public static SandTrapManager instance = null;
    public List<GameObject> SandTrapList = new List<GameObject>();

    void Awake() {
        if (instance == null) {
            instance = this;
        }

        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void FindSandTrapsInTheScene() {
        foreach(GameObject GameObjectInTheScene in GameObject.FindObjectsOfType(typeof(GameObject)))
         {
             if(GameObjectInTheScene.GetComponent<SandTrap>() != null)
             SandTrapList.Add (GameObjectInTheScene);
         }  

    }       

}