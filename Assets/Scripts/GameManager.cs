using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Object[] scenes;
    static int currentLevel;
    // List<string> levels = new List<string>();
    public TimeCountingMethod timeCountingMethod;
    public float elapsedTime;
    static bool gameEnd;
    static int timeIncrease;
    static AudioSource gameEffects;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    static AudioClip victory;
    static AudioClip defeat;


    public static GameManager instance {
        get{
            if(_instance == null){
                GameObject go = new GameObject();
                _instance = go.AddComponent<GameManager>();
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
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
        if (timeCountingMethod == TimeCountingMethod.Timed){
            elapsedTime = 0;
        }
        gameEnd = false;
        timeIncrease = 1;
        gameEffects = GetComponent<AudioSource>();
        victory = victorySound;
        defeat = defeatSound;

    }

    void Start() {
        // RetrieveAllScenes();
        // currentLevel = levels.IndexOf(SceneManager.GetActiveScene().name);
        for (int i=0; i<scenes.Length; i++) {
            if (scenes[i].name == SceneManager.GetActiveScene().name) {
                currentLevel = i;
                break;
            }
        }
        if (timeCountingMethod == TimeCountingMethod.Timed){
            elapsedTime = 0;
        }

    }


    void Update()
    {
        MeasureTime();
        if (TimeIsUp()){
            Defeat();
        }
    }




    // void RetrieveAllScenes() {
    //     for (int i = 0; i < scenes.Length ; i++){
    //         levels.Add(scenes[i].name);
    //     }
    // }

    public void NextScene() {
        if (currentLevel < scenes.Length - 1 ) {
            SceneManager.LoadScene(scenes[currentLevel + 1].name);
        }
    }


    public void PreviousScene() {
        if (currentLevel > 0) {
            SceneManager.LoadScene(scenes[currentLevel - 1].name);
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(scenes[currentLevel].name);
    }

    public static void TriggerVictory() {
        gameEffects.clip = victory;
        gameEffects.Play();
        UIManager.instance.ShowGameEndedOverlay(GameEndings.Victory);
        gameEnd = true;
        timeIncrease = 0;
        ConffetiGunsManager.instance.activateConffetiGuns();
    }

    public static void  Defeat() {
        gameEffects.clip = defeat;
        gameEffects.Play();
        UIManager.instance.ShowGameEndedOverlay(GameEndings.GameOver);
        gameEnd = true;
        timeIncrease = 0;
    }

    void MeasureTime() {
         switch(timeCountingMethod) {
            case TimeCountingMethod.Temporized:
                elapsedTime -= timeIncrease * Time.deltaTime;
                break;
            case TimeCountingMethod.Timed:
                elapsedTime += timeIncrease * Time.deltaTime;
                break;
        }
    }

    bool TimeIsUp() {
        return elapsedTime <= Mathf.Epsilon && !gameEnd;
    }

    public TimeCountingMethod getTimeCountingMethod() {
        return _instance.timeCountingMethod;
    }

    public float getElapsedTime() {
        return _instance.elapsedTime;
    }

    public bool isGameEnded() {
        return gameEnd;
    }
}
