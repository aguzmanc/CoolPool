using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour {
    public static T Instance {
        get {
            InitializeIfNecessary();
            return _instance;
        }
    }
    static T _instance;

    static void InitializeIfNecessary () {
        if (_instance == null) {
            _instance = Object.FindObjectOfType<T>();
        }
    }

    void Awake () {
        if (_instance != null && _instance != this) {
            Destroy(this);
            return;
        }
        InitializeIfNecessary();
    }

    void OnDestroy () {
        _instance = null;
    }
}
