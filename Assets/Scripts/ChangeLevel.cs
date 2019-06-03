using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public Object[] scenes;
    static int currentLevel = 0;
    List<SceneAsset> levels = new List<SceneAsset>();
    void Start()
    {
        for (int i = 0; i < scenes.Length ; i++){
             levels.Add( scenes[i] as SceneAsset);
          
        }
    }
    public void NextScene() {
        if (currentLevel < levels.Count - 1 ) {
            SceneManager.LoadScene(levels[currentLevel + 1].name);
            currentLevel ++;
        }
    }


    public void PreviousScene() {
        if (currentLevel > 0) {
            SceneManager.LoadScene(levels[currentLevel - 1].name);
            currentLevel --;
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(levels[currentLevel].name);
    }
    
}

