using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public Object[] scenes;
    static int currentLevel;
    List<string> levels = new List<string>();


    void Start(){
        for (int i = 0; i < scenes.Length ; i++){
            SceneAsset level = scenes[i] as SceneAsset;
            levels.Add(level.name);
        }
        currentLevel = levels.IndexOf(SceneManager.GetActiveScene().name);
        print(currentLevel); 
        print("niveles :" + levels.Count);
    }


    public void NextScene() {
        if (currentLevel < levels.Count - 1 ) {
            SceneManager.LoadScene(levels[currentLevel + 1]);
        }
    }


    public void PreviousScene() {
        if (currentLevel > 0) {
            SceneManager.LoadScene(levels[currentLevel - 1]);
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(levels[currentLevel]);
    }
    
}

