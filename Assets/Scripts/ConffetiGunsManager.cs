using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConffetiGunsManager : MonoBehaviour
{
    public bool activar;
    List<Transform> conffetiGuns = new List<Transform>();
    
    private static ConffetiGunsManager _instance;

    public static ConffetiGunsManager instance{
        get{
            
            return _instance;
        }
    }
    
    void Awake() {
        _instance = this;

    }
    void Start() {
        foreach (Transform child in transform)
             conffetiGuns.Add(child);
    }

    void Update() {
        if(activar) 
            activateConffetiGuns();
    }

    public void activateConffetiGuns() {
        foreach (Transform conffetiGun in conffetiGuns)
            conffetiGun.gameObject.SetActive(true);
    }
}
