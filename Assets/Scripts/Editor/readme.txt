Hay scripts que deben ser ejecutados sólamente en modo edición. Éstos scripts dan error en tiempo de compilación al generar el .exe del juego si no se encuentran en ésta carpeta, ya que sólamente sirven para brindar funcionalidad en modo edición (y no en modo juego).

Son scripts que permiten hacer niveles o configurar assets y no deberían estar disponibles en el producto final, añadiendo botones al inspector, o añadiendo botones en la escena (), o "gizmos"

Puedes saber más sobre éstos scripts siguiendo éste tutorial: https://unity3d.com/learn/tutorials/topics/scripting/introduction-editor-scripting (aún no veo este tutorial)

A continuación, el snippet de código que uso para hacer scripts en el editor:

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(ScriptQueHeredaDeMonoBehaviour))]
public class AlgunNombre : Editor {
    ScriptQueHeredaDeMonoBehaviour Target { get => return (ScriptQueHeredaDeMonoBehaviour) target; }

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        // aquí se personaliza lo que aparece en el inspector al inspeccionar el ScriptQueHeredaDeMonoBehaviour
        // Aquí puede usarse todo esto: https://docs.unity3d.com/ScriptReference/EditorGUILayout.html
        // y esto https://docs.unity3d.com/ScriptReference/GUILayout.html
        // aún no sé cuál es la diferencia entre esos dos conjuntos de cosas o.O
        // hasta ahora, me parece que hacen lo mismo O.o

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    public static void DrawGizmos (ScriptQueHeredaDeMonoBehaviour customTarget) {
        // la sigueinte línea de código hace que todos los scripts se dibujen en el transform local del padre.
        Handles.matrix = customTarget.transform.localToWorldMatrix;
        // dibujar gizmos!
        // aquí se pone todo lo que queremos que aparezca en la escena.
        // también se puede poner directamete en OnSceneGUI, pero si lo hacemos así, no vamos a poder
        // reusarlo en algún otro editor.
        // de esta forma, podemos hacer esto: EditorDelHijo.DrawGizmos(Target.hijoConCustomEditor)
        // aquí se puede usar todo esto: https://docs.unity3d.com/ScriptReference/Gizmos.html
        // y esto: https://docs.unity3d.com/ScriptReference/Handles.html
        // y probablemente, otras cosas raras que aún no descubro por completo
        // (como esto: https://docs.unity3d.com/ScriptReference/IMGUI.Controls.BoxBoundsHandle.html )
    }

    void OnSceneGUI () {
        DrawGizmos(customTarget)

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }
}
