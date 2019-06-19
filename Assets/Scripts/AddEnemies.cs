using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemies : MonoBehaviour
{
    public GameObject enemy;
    public Transform positionIndicator;

    void Start(){
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateEnemy(positionIndicator.position);
        }
    }

    public void CreateEnemy(Vector3 pos) {
        GameObject enemyCreated = Instantiate(enemy);
        enemyCreated.transform.position = pos;
        enemyCreated.transform.parent = transform.parent;
    }
}
