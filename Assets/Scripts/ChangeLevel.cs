using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    List<string> niveles = new List<string>{"level1", "level2", "level3"};
    static int currentLevel = 0;


    public void NextScene() {
        print(niveles.Count);
        print(currentLevel);
        if (currentLevel < niveles.Count - 1 ) {
            SceneManager.LoadScene(niveles[currentLevel + 1]);
            currentLevel ++;
        }
    }


    public void PreviousScene() {
        if (currentLevel > 0) {
            SceneManager.LoadScene(niveles[currentLevel - 1]);
            currentLevel --;
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(niveles[currentLevel]);
    }
    
}

