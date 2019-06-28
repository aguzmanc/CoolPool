using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEditor;

public class AddEnemies : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> listEnemies = new List<GameObject>();
    
    void OnEnable() {
    }
    
    void Start(){
    }

    public GameObject CreateEnemy(Vector3 position){
        GameObject newEnemy = SafePrefabEnemyInstantiate(position, Quaternion.identity);
        return newEnemy;
    }

    public GameObject SafePrefabEnemyInstantiate (Vector3 position, Quaternion rotation) {
        
        #if UNITY_EDITOR
        if (Application.isPlaying) {
            return GameObject.Instantiate(enemy, position, rotation);
        } 
        
        else {
            GameObject obj = PrefabUtility.InstantiatePrefab(enemy) as GameObject;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.parent = gameObject.transform;
            InsertEnemy(obj);
            return obj;
        }
        #else
        return Instantiate(reference);
        #endif
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
