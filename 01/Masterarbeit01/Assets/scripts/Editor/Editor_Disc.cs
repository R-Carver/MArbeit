using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorTarget_Disc))]
public class Editor_Disc : Editor
{   
    float colliderRadius = 0.4f;

    void OnSceneGUI() 
    {
        EditorTarget_Disc discTarget = (EditorTarget_Disc)target;

        Handles.color = Color.grey;
        Handles.DrawWireDisc(discTarget.transform.position, new Vector3(0, 0, 1), colliderRadius);
    }
}
