using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(EditorTarget_Vector))]
public class Editor_Vector : Editor
{   
    void OnEnable() 
    {
        SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));

    }

    void CustomOnSceneGUI(SceneView sceneview)
    {
        EditorTarget_Vector vectorTarget = (EditorTarget_Vector)target;

        Handles.color = Color.red;

        Vector2 extendedVector = 2 * (vectorTarget.ToPoint.position - vectorTarget.FromPoint.position);
        //Debug.Log(extendedVector);

        Handles.DrawDottedLine((Vector2)vectorTarget.FromPoint.position, (Vector2)vectorTarget.ToPoint.position + extendedVector, 3.0f);
    }
}
