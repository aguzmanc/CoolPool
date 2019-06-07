﻿using System.Collections;
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

    public GameObject hook;

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
        GameController.instance.NextScene();
    }

    public void PrevLevel(){
        GameController.instance.PreviousScene();
    }

    public void PlayAgain(){
        GameController.instance.ReloadScene();
    }

    public void DestroyHookControl() {
        Destroy(hook);
    }
}