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
                if (Handles.Button(obstacle.transform.position, Quaternion.Euler(90, 0, 0), 1.5f, 1.5f, Handles.RectangleHandleCap)){
                    thingBeingMoved = obstacle;
                }
            }

        }

        if (thingBeingMoved) {
            // posición
            Vector3 newPos = Handles.PositionHandle(thingBeingMoved.transform.position,
                                                    Quaternion.identity);
            if (newPos != thingBeingMoved.transform.position) {
                Undo.RecordObject(thingBeingMoved, "obstáculo fue movido");
            }
            thingBeingMoved.transform.position = newPos;

            // escala
            // (ver ObstaclesHandler línea 7)
            // si fuera una propiedad del script Obstacle.cs, se le llamaría desde aquí
            // así: thingBeingMoved.GetComponent<Obstacle>.scalePlaceHolder;
            newPos = Handles.PositionHandle(Target.scalePlaceholder,
                                            Quaternion.identity);
            if (newPos != Target.scalePlaceholder) {
                Undo.RecordObject(Target.gameObject, "scale placeholder fue movido"); // porque cambiamos una propiedad de Target
                // si fuera una propiedad de Obstacle.cs, en vez de Target.gameObject
                // habría que guardar a thingBeingMoved!
            }
            Target.scalePlaceholder = newPos;

            thingBeingMoved.transform.localScale =
                Target.scalePlaceholder - thingBeingMoved.transform.position;
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
