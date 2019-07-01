using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemies : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> listEnemies = new List<GameObject>();
    
    void OnEnable() {
    }
    
    void Start(){
    }

    public GameObject CreateEnemy(Vector3 pos) {
        GameObject enemyCreated = Instantiate(enemy);
        enemyCreated.transform.position = pos;
        enemyCreated.transform.parent = transform.parent;
        InsertEnemy(enemyCreated);
        enemyCreated.transform.parent = gameObject.transform;
        return enemyCreated;
    }

    public List<GameObject> GetEnemiesList() {
        return listEnemies;
    }

    public void InsertEnemy(GameObject enemy) {
        listEnemies.Add(enemy);
    }

    public void DestroyEnemy(GameObject enemy) {
        for(int i = 0; i < listEnemies.Count; i++) {
            if(listEnemies[i].transform.position == enemy.transform.position) {
                if (Application.isEditor)
                    Object.DestroyImmediate(listEnemies[i]);
                listEnemies.RemoveAt(i);
                break;
            }
        }
    }

    public void udpateListEnemies() {
        for(int i = 0; i < listEnemies.Count; i++) {
            if(listEnemies[i] == null) {
                listEnemies.RemoveAt(i);
            }
        }
    }
}
