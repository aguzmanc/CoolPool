using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    private static ChangeLevel _instance;

    public static ChangeLevel instance{
        get{ 
            if(_instance==null){
                GameObject go = new GameObject();
                _instance = go.AddComponent<ChangeLevel>();
            }


            return _instance;
        }
        //set{}
    }

    private int _algo;
    public int algo{
        get{ return _algo;}
        set{ _algo = value;}
    }
    public int GetAlgo(){return _algo;}
    public void SetAlgo(int algo){
        _algo = algo;
    }

    public static ChangeLevel GetInstance(){
        return _instance;
    }
    //public static ChangeLevel instance;


    public Object[] scenes;
    static int currentLevel;
    List<string> levels = new List<string>();


    void Start(){
        if(_instance==null)
            Debug.Log("no hay otro");
        else {
            Debug.Log("Si hay otro, ERROR!!");
            Destroy(gameObject);
            return;
        }
        

        _instance = this;

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

