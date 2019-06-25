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
    AudioSource timerSound;
    
    public GameObject hook;
    float sliderVolumeValue = 0.1f;
    bool isBeeping = false;

    public static UIManager instance{
        get{
            return _instance;
        }
    }
    
    void Awake() {
        ChargeFinishGameButtons();

        timerText = GameObject.Find("Timer").GetComponent<UnityEngine.UI.Text>();
        timerAnimations = GameObject.Find("Timer").GetComponent<Animator>();  
        timerSound =  GameObject.Find("Timer").GetComponent<AudioSource>();
    }

    void Start() {
        _instance = this;
    }

    void Update() {
        float time = GameManager.instance.getElapsedTime();
        time = Mathf.RoundToInt(time);
        
        if(time == 0) {
            timerAnimations.enabled = false;
        }
        
        if(GameManager.instance.isGameEnded()) {
            StopSoundTimer();
            StopAllCoroutines();
            timerAnimations.SetBool("Less10Seconds", false);
            return;
        }

        if(GameManager.instance.getTimeCountingMethod() == TimeCountingMethod.Temporized) {
            if(GameManager.instance.getElapsedTime() < 10) {
                if(isBeeping == false) {
                    isBeeping = true;
                    StartCoroutine(SoundBeep());
                }

                timerAnimations.SetBool("Less10Seconds", true);
                timerText.text = "Tiempo: " + time.ToString();
            }
            else {
                timerText.text = "Tiempo: " + time.ToString();
            }
        }
        
        else {
            timerText.text = "Tiempo: " + time.ToString();
        }
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

    IEnumerator SoundBeep() {
        while (true) {
            
            timerSound.volume = sliderVolumeValue;
            timerSound.Play();
            sliderVolumeValue = sliderVolumeValue + 0.1f;
            yield return new WaitForSeconds(1);
        }
    }

    void StopSoundTimer() {
        timerSound.Stop();
    }
}