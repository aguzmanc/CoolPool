using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandTrap : MonoBehaviour {

    public float speedPenalization;
    public float angularPenalization;

    void OnTriggerEnter (Collider c) {

        if  (c.GetComponent<PushBall>() != null) {

            Rigidbody ball = c.GetComponent<Rigidbody>();
            //c.GetComponent<PushBall>().strengthMultiplier = speedPenalization;
            ball.drag = speedPenalization;
            ball.angularDrag = angularPenalization;
            //ball.velocity = new Vector3 (0,0,0);
            //ball.angularVelocity = new Vector3 (0,0,0);
        }
    }
    void OnTriggerExit (Collider c) {

        if (c.GetComponent<PushBall>() != null) {

            Rigidbody ball = c.GetComponent<Rigidbody>();
            ball.drag = 0;
            ball.angularDrag = 0.05f;
        }
    }
}
