using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{ 

    AudioSource effectsSoundsBall;
    public AudioClip ballImpactWithEnemy;

    void Awake() {
        effectsSoundsBall = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision c) {
        if (c.gameObject.name == "Capsule"){
            effectsSoundsBall.PlayOneShot(ballImpactWithEnemy);
            Destroy(c.gameObject);

        }
        
    }
}
