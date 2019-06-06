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

    public static UIManager instance{
        get{
            return _instance;
        }
    }
    
    void Awake() {
        victoryAnimation = GameObject.Find("Victory").GetComponent<Animator>();
        gameOverAnimation = GameObject.Find("GameOver").GetComponent<Animator>();

        backLevelButton = GameObject.Find("BackLevel").GetComponent<Animator>();
        repeatLevelButton = GameObject.Find("RepeatLevel").GetComponent<Animator>();
        nextLevelButton = GameObject.Find("NextLevel").GetComponent<Animator>();
    }

    void Start() {
        _instance = this;
    }

    void Update() {
        
    }
    public void ShowVictoryOverLay() {
        victoryAnimation.SetBool("Activate", true);
        LevelChangeButtons();
    }
    
    public void ShowGameEndOverLay() {
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
}