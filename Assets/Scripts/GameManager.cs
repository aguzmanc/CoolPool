using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public List<string> levels;
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
        gameEffects = GetComponent<AudioSource>();
        victory = victorySound;
        defeat = defeatSound;
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

    }

    void Start() {
        currentLevel = levels.IndexOf(SceneManager.GetActiveScene().name);
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
