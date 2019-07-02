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

    // para poder añadir fácilmente los obstáculos que se crean en el futuro
    static List<GameObject> obstaclesList = new List<GameObject>();
    public static GameObject thingBeingMoved;


    void OnEnable () {
        obstaclesList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
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
                    GameObject created =
                        SafePrefabInstantiate(Target.GetPrefabReference(),
                                              hitInfo.point);
                    obstaclesList.Add(created);
                    Undo.RegisterCreatedObjectUndo(created, "Se creo un obstaculo");
                }
            }
        }
        if (isEditObstaclePressed) {
            foreach (GameObject obstacle in obstaclesList) {
                Color aux = Handles.color;
                Handles.color = new Color(1, 0, 0, 1);
                if (thingBeingMoved != obstacle) {
                    if (Handles.Button(obstacle.transform.position, Quaternion.Euler(90, 0, 0), 1.5f, 1.5f, Handles.RectangleHandleCap)){
                        thingBeingMoved = obstacle;
                        thingBeingMoved.GetComponent<Obstacle>().scalePlaceHolder = 
                            thingBeingMoved.transform.position + thingBeingMoved.transform.localScale;
                    }
                }
                
            }

        }

        if (thingBeingMoved) {
            Vector3 newPos = Handles.PositionHandle(thingBeingMoved.transform.position,
                                                    Quaternion.identity);
            if (newPos != thingBeingMoved.transform.position) {
                Undo.RecordObject(thingBeingMoved.transform, "obstáculo fue movido");
            }
            thingBeingMoved.transform.position = newPos;
            newPos = Handles.PositionHandle(thingBeingMoved.GetComponent<Obstacle>().scalePlaceHolder,
                                            Quaternion.identity);
            if (newPos != thingBeingMoved.GetComponent<Obstacle>().scalePlaceHolder) {
                Undo.RecordObject(thingBeingMoved.GetComponent<Obstacle>(), "scale placeholder fue movido");
                Undo.RecordObject(thingBeingMoved.transform, "obstáculo fue movido");
            }
            thingBeingMoved.GetComponent<Obstacle>().scalePlaceHolder = newPos;
            thingBeingMoved.transform.localScale =
                thingBeingMoved.GetComponent<Obstacle>().scalePlaceHolder - thingBeingMoved.transform.position;
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
