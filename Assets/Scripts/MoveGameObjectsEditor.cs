using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MoveGameObjects))]
public class MoveGameObjectsEditor : Editor {
    MoveGameObjects Target { get => (MoveGameObjects) target; }
    List<Transform> movableObjects = new List<Transform>();
   

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        GUILayout.Button("Mover Jugador");
        ChargeAllChilds(Target.transform);
        // if (Target.director == null) return;
        // if (GUILayout.Button("Play")) {
        //     Target.Play();
        // }
        // if (GUILayout.Button(Target.IsPaused? "Resume": "Play")) {
        //     Target.TogglePause();
        // }
        // isEditingGoal = GUILayout.Toggle(isEditingGoal, "Mover goal", "Button");
        // if (GUI.changed && !Application.isPlaying) {
        //     EditorUtility.SetDirty(Target);
        //     EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        // }
        foreach(Transform tranform in movableObjects) {
            Debug.Log(tranform);
        }
    }


    void ChargeAllChilds(Transform transform){
        for (int i= 0; i < transform.childCount; i++){
            ChargeAllChilds(transform.GetChild(i));
            if (transform.GetComponent<IMoveObjects>() != null){
                 movableObjects.Add(transform);
            }
            Debug.Log("xxdxd");
        }   
    }

}