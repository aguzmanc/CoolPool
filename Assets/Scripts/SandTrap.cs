using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandTrap : MonoBehaviour {
    void OnTriggerEnter (Collider c) {
        // verificar si c es la bola u otra cosa
        // acceder a su componente Rigidbody (La bola se encuentra en el prefab LevelSetup.prefab, en el game object GameplayElements/Sphere)
        // setearle su velocidad a (0,0,0) ----> https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
    }
}
