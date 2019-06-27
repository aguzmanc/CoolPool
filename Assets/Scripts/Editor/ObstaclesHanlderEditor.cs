using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(ObstaclesHandler))]
public class ObstaclesHandlerEditor : Editor {

    ObstaclesHandler Target { get => (ObstaclesHandler) target; }
    static bool isObstacleButtonPressed;
    static bool isEditObstaclePressed;
    static GameObject[] obstaclesList;
    public static GameObject thingBeingMoved;
    static Vector3 scalePos;


    void OnEnable () {
       obstaclesList = GameObject.FindGameObjectsWithTag("Obstacle");
    }
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        isObstacleButtonPressed = GUILayout.Toggle(isObstacleButtonPressed ,"Crear Obstaculo", "Button");
        isEditObstaclePressed = GUILayout.Toggle(isEditObstaclePressed ,"Editar Obstaculos", "Button");
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }


    void OnSceneGUI () {
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (isObstacleButtonPressed) {
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    Undo.RegisterCreatedObjectUndo( SafePrefabInstantiate(Target.GetPrefabReference(), hitInfo.point), "Se creo un obstaculo");
                }
            }
        }
        if (isEditObstaclePressed) {
            foreach (GameObject obstacle in obstaclesList) {
                Color aux = Handles.color;
                Handles.color = new Color(1, 0, 0, 1);  
                if (Handles.Button(obstacle.transform.position, Quaternion.Euler(90, 0, 0), 1.5f, 1.5f, Handles.RectangleHandleCap)){
                    thingBeingMoved = obstacle;
                }
            }

        }

        if (thingBeingMoved) {
            thingBeingMoved.transform.position = Handles.PositionHandle(thingBeingMoved.transform.position, Quaternion.identity);
            scalePos = Handles.PositionHandle(scalePos, Quaternion.identity);
            thingBeingMoved.transform.localScale = // -thingBeingMoved.transform.InverseTransformPoint(scalePos);
                Handles.PositionHandle(thingBeingMoved.transform.localScale + thingBeingMoved.transform.position, Quaternion.identity) - thingBeingMoved.transform.position;

                // Vector3 newPosition = Handles.PositionHandle(thingBeingMoved.transform.position, Quaternion.identity);
                // Vector3 Pos = Vector3.zero; //Guardar en el script o algo :¨v
                // Pos.y = thingBeingMoved.transform.position.y;
                // Vector3 newPosition2 = Handles.PositionHandle(Pos, Quaternion.identity);
                // //if (newPosition2 != Pos) {
                //     // Undo.RecordObject(thingBeingMoved, "algo se movió!");
                    
                //     thingBeingMoved.transform.position = newPosition;
                //     thingBeingMoved.transform.localScale = thingBeingMoved.transform.InverseTransformPoint(Pos);// newPosition2 - Pos;
                //     Vector3 a = thingBeingMoved.transform.localScale;
                //     thingBeingMoved.transform.localScale = new Vector3(a.x, 1, a.z);
                    
                // }
                
        }

        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
        
        
        
        
    }


    public GameObject SafePrefabInstantiate(GameObject reference, Vector3 position){
        GameObject newObstacle = SafePrefabInstantiate(reference,position,Quaternion.identity);
        return newObstacle;
    }


    public GameObject SafePrefabInstantiate (GameObject reference, 
                                                    Vector3 position,
                                                    Quaternion rotation) {
        if (Application.isPlaying) {
            return GameObject.Instantiate(reference, position, rotation);
        } else {
            GameObject obj = PrefabUtility.InstantiatePrefab(reference) as GameObject;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
    }
}