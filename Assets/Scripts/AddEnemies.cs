using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemies : MonoBehaviour
{
    public GameObject enemy;
    void Start(){
    }

    void Update () {
    }

    public GameObject CreateEnemy(Vector3 pos) {
        GameObject enemyCreated = Instantiate(enemy);
        enemyCreated.transform.position = pos;
        enemyCreated.transform.parent = transform.parent;
        return enemyCreated;
    }
}
