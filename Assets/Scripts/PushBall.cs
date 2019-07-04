using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour, IMoveObjects {
    public float strength = 30;
    Rigidbody ballRigidbody;

    AudioSource effectsSoundsBall;
    public AudioClip ballImpactWithHookSound;
    public AudioClip impactBlock;
    
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        effectsSoundsBall = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Goal>()) {
           GameManager.TriggerVictory();
           
        }
    }

    public void hitBall(Collision collision) {
        Vector3 direction = collision.transform.position - transform.parent.position;
        direction.y = 0;
        Vector3 position = collision.GetContact(0).point;
        position.y = 0;
        float strength = collision.transform.GetComponent<PushBall>().strength;
        collision.transform.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * strength, position, ForceMode.Impulse);
        ActivateImpactHookWithBall();
    }

    void ActivateImpactHookWithBall() {
        effectsSoundsBall.clip = ballImpactWithHookSound;
        effectsSoundsBall.Play();
    }


    void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<BlockCube>()){
            effectsSoundsBall.PlayOneShot(impactBlock);
        }
    }
}
