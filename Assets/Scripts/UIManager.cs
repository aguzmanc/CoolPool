using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    
    Animator victoryAnimation;
    Animator gameOverAnimation;

    Animator backLevelButton;
    Animator repeatLevelButton;
    Animator nextLevelButton;

    UnityEngine.UI.Text timerText;
    Animator timerAnimations;

    public GameObject hook;

    public static UIManager instance{
        get{
            return _instance;
        }
    }
    
    void Awake() {
        ChargeFinishGameButtons();

        timerText = GameObject.Find("TimerText").GetComponent<UnityEngine.UI.Text>();
        timerAnimations = GameObject.Find("TimerText").GetComponent<Animator>();       
    }

    void Start() {
        _instance = this;
    }

    void Update() {
        string time = "5";
        timerText.text = "Tiempo: ";
        timerAnimations.SetBool("Less10Seconds", true);
        // timerText.text = "Tiempo: " + GameManager.instance.GetElapsedTime();
    }

    public void ShowVictoryOverLay() {
        DestroyHookControl();
        victoryAnimation.SetBool("Activate", true);
        LevelChangeButtons();
    }
    
    public void ShowGameEndOverLay() {
        DestroyHookControl();
        gameOverAnimation.SetBool("Activate", true);
        LevelChangeButtons();
    }

    public void LevelChangeButtons() {
        backLevelButton.SetBool("Activate", true);
        repeatLevelButton.SetBool("Activate", true);
        nextLevelButton.SetBool("Activate", true);
    }

    public void ShowGameEndedOverlay(GameEndings ending) {
        switch(ending) {
            case GameEndings.Victory:
                ShowVictoryOverLay();
                break;
            case GameEndings.GameOver:
                ShowGameEndOverLay();
                break;
        }
    }

    public void NextLevel(){
        GameManager.instance.NextScene();
    }

    public void PrevLevel(){
        GameManager.instance.PreviousScene();
    }

    public void PlayAgain(){
        GameManager.instance.ReloadScene();
    }

    public void DestroyHookControl() {
        Destroy(hook);
    }

    public void ChargeFinishGameButtons() {
        victoryAnimation = GameObject.Find("Victory").GetComponent<Animator>();
        gameOverAnimation = GameObject.Find("GameOver").GetComponent<Animator>();

        backLevelButton = GameObject.Find("BackLevel").GetComponent<Animator>();
        repeatLevelButton = GameObject.Find("RepeatLevel").GetComponent<Animator>();
        nextLevelButton = GameObject.Find("NextLevel").GetComponent<Animator>();       
    }
}