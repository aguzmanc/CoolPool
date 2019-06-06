using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaVictoria : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        UIManager.instance.ShowGameEndedOverlay(GameEndings.Victory);
    }
}
