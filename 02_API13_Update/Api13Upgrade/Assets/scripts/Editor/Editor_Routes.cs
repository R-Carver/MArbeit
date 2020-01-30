using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorTarget_Route))]
public class Editor_Routes : Editor
{   
    void OnSceneGUI()
    {
        EditorTarget_Route routeTarget = (EditorTarget_Route)target;
        Receiver_Controller_FSM receiver_controller = routeTarget.receiver_controller;

        if(receiver_controller.currentRoute != null)
        {
            int numOfRoutePoints = receiver_controller.currentRoute.routePoints.Length;

            //the lineSegments need a start and end point for EACH line, thus the *2
            Vector3[] lineSegments = new Vector3[numOfRoutePoints * 2];

            //Vector3 prevPoint = new Vector3(-2, -2, 0);
            Vector3 prevPoint = receiver_controller.startPos;
            
            int pointIndex = 0;
            for (int i = 0; i < numOfRoutePoints; i++)
            {
                //get the currentPoint
                Vector3 currPoint = receiver_controller.currentRoute.routePoints[i];
                currPoint = prevPoint + currPoint;

                //store the starting point of the line segment
                lineSegments[pointIndex] = prevPoint;
                pointIndex++;

                //store the endpoint of the line segment
                lineSegments[pointIndex] = currPoint;
                pointIndex++;

                prevPoint = currPoint;
            }
            
            /*
            //Handles.DrawLine((Vector2)routeTarget.transform.position, (Vector2)routeTarget.receiver_controller.currentTarget);
             Vector3[] lineSegments = new Vector3[4];
             lineSegments[0] = new Vector3(-2, 2, 0);
             //lineSegments[1] = lineSegments[0] + (Vector3)receiver_controller.currentRoute.routePoints[0];
             //lineSegments[2] = lineSegments[1] + (Vector3)receiver_controller.currentRoute.routePoints[1];
             lineSegments[1] = lineSegments[0] + new Vector3(1, 0, 0);
             lineSegments[2] = lineSegments[1];
             lineSegments[3] = lineSegments[2] + new Vector3(1, 1, 0);
             */

            Handles.DrawLines(lineSegments);
        }
    }
}
