using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    enum GameEndings {GameOver, Victory};
    Transform buttons;

    void Start() {
        buttons = transform.GetChild(0);
        print(buttons.name);
    }

    void Update() {
    }

    public void ShowGameEndOverLay() {
        
    }
}