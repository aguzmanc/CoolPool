using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

[CustomEditor(typeof(AddEnemies))]
public class AddEnemiesEditor : Editor {

    AddEnemies Target { get => (AddEnemies) target; }
    static bool isEnemyCreateButtonPressed;
    static bool isPathDeleteButtonPressed;
    static bool isPathMoveButtonPressed;
    static bool isEnemyDeleteButtonPressed;
    static List<bool> optionsInspector = new List<bool>();
    static List<bool> isAddPointToPathEnemyPressed = new List<bool>();
    static int courrentEnemy = -1;

    static int toolbarInt = 0;
    
    public void OnEnable() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        
        int enemiesListLength = enemiesList.Count;
        int enemiesPointPathButtonsLength = isAddPointToPathEnemyPressed.Count;
        if((enemiesListLength - enemiesPointPathButtonsLength) > 0) {
            for(int i = 0; i < (enemiesListLength - enemiesPointPathButtonsLength); i++) {
                isAddPointToPathEnemyPressed.Add(false);
            }
        }
    }

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        isEnemyCreateButtonPressed = GUILayout.Toggle(isEnemyCreateButtonPressed ,"Crear enemigo", "Button");
        isEnemyDeleteButtonPressed = GUILayout.Toggle(isEnemyDeleteButtonPressed ,"Eliminar enemigos", "Button");

        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void DrawButtonsToAddPointsToPathEnemies() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        for(int i = 0; i < enemiesList.Count; i++) {
            isAddPointToPathEnemyPressed[i] = GUILayout.Toggle(isAddPointToPathEnemyPressed[i], "Agregar puntos al path del enemigo: " + (i + 1), "Button");
        }
    }

    void updateListEnemies() {
        Target.udpateListEnemies();
    }

    void OnSceneGUI () {
        updateListEnemies();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        
        if(isEnemyCreateButtonPressed) {
            DisableAllButtonsInspectorOptions();
            isEnemyCreateButtonPressed = true;
            if (Physics.Raycast(worldRay, out hitInfo)) {
                if (Event.current.type == EventType.MouseDown) {
                    GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                    Event.current.Use();
                    Undo.RegisterCreatedObjectUndo(Target.CreateEnemy(hitInfo.point), "Se creo un enemigo");
                    isAddPointToPathEnemyPressed.Add(false);
                }
            }
        }

        if(isEnemyDeleteButtonPressed) {
            DisableAllButtonsInspectorOptions();
            isEnemyDeleteButtonPressed = true;
            DrawButtonsToDeleteEnemies();
        }
        
        if (GUI.changed && ! Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void AddPointsToPathByEnemy() {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        RaycastHit hitInfo;
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Handles.color = Color.black;
        for(int i = 0; i < isAddPointToPathEnemyPressed.Count; i++) {
            if(isAddPointToPathEnemyPressed[i]) {
                DrawPointsToThePathEnemy(i);
                if (Physics.Raycast(worldRay, out hitInfo)) {

                    if (Event.current.type == EventType.MouseDown) {
                        
                        GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
                        Event.current.Use();
                        List<Transform> listOfChilds = enemiesList[i].GetComponent<FollowPath>().GetAllChildsPath();
                        
                        if(listOfChilds == null) {
                            enemiesList[i].GetComponent<FollowPath>().CreatePath();
                            Undo.RegisterCreatedObjectUndo(enemiesList[i].GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                        }

                        else {
                            Undo.RegisterCreatedObjectUndo(enemiesList[i].GetComponent<FollowPath>().AddPointToPath(hitInfo.point), "Se agrego un punto al path");
                        }
                    }
                }
            }

        }
    }

    void DrawPointsToThePathEnemy(int index) {
        List<GameObject> enemiesList = Target.GetEnemiesList();
        List<Transform> listOfChilds;
        listOfChilds = enemiesList[index].GetComponent<FollowPath>().GetAllChildsPath();
        
        Handles.DrawSolidDisc(enemiesList[index].transform.position, Vector3.up, 0.5f);
        if(listOfChilds == null)
            return;

        for(int i = 0; i < listOfChilds.Count; i++) {
            Handles.DrawSolidDisc(listOfChilds[i].transform.position, Vector3.up, 0.5f);
        }
    }
    
    void DrawButtonsToDeleteEnemies() {
        Handles.color = new Color(1, 0, 0, 1);
        List<GameObject> enemiesList = Target.GetEnemiesList();
        for (int i = 0 ; i < enemiesList.Count; i++) {
            Handles.DrawSolidDisc(enemiesList[i].transform.position, Vector3.up, 0.5f);
            if (Handles.Button(enemiesList[i].transform.position, Quaternion.Euler(90, 0,0),
                0.5f, 0.5f, Handles.CircleHandleCap)) {
                Target.DestroyEnemy(enemiesList[i]);
            }
        }
    }

    void DisableAllButtonsInspectorOptions() {
        isEnemyCreateButtonPressed = false;
        isEnemyDeleteButtonPressed = false;   
    }
}
