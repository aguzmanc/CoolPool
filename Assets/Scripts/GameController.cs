﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public Object[] scenes;
    static int currentLevel;
    List<string> levels = new List<string>();

    public static GameController instance {
        get{ 
            if(_instance == null){
                GameObject go = new GameObject();
                _instance = go.AddComponent<GameController>();
            }
            return _instance;
        }
    }


    void Awake()
    {
        if(_instance) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Start() {
        RetrieveAllScenes();
        currentLevel = levels.IndexOf(SceneManager.GetActiveScene().name);
    }
    void RetrieveAllScenes() {
        for (int i = 0; i < scenes.Length ; i++){
            SceneAsset level = scenes[i] as SceneAsset;
            levels.Add(level.name);
        }
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

    public static void TriggerVictory() {
        //UIManager.DisplayVictory(); 
        print ("Woooooooo Ganaste!!!");
    }
    
}
