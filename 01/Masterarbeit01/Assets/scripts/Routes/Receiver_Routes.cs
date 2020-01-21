using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver_Routes
{   
    public static Receiver_Routes Instance;

    public Receiver_Routes()
    {
        if(Instance == null)
        {
            Instance = this;
            InitRoutes();
        }else
        {
            //Debug.LogError("There shouldnt ever be 2 Receiver_Ropoute classes");
        }
        
    }

    //make a dict from enum to route
    public Dictionary<RouteName, Route> routes = new Dictionary<RouteName, Route>();

    void InitRoutes()
    {   
        //forward in our world is +x and left is +y
        //use those vectors to construct the routes
        Vector2 forward = new Vector2(1, 0);
        Vector2 forward_left = new Vector2(1, 1);
        Vector2 forward_right = new Vector2(1, -1);
        Vector2 left = new Vector2(0, 1);
        Vector2 right = new Vector2(0, -1);


        //post
        Vector2[] postPoints = new Vector2[1];

        postPoints[0] = forward * 7;
        
        Route postRoute = new Route(RouteName.Post, postPoints);

        routes.Add(postRoute.routeName, postRoute);

        //--------------------------------------------------------------

        //slant-left
        Vector2[] slantLPoints = new Vector2[2];

        slantLPoints[0] = forward * 2;
        slantLPoints[1] = forward_left * 4;
        
        Route slantLRoute = new Route(RouteName.Slant_Left, slantLPoints);

        routes.Add(slantLRoute.routeName, slantLRoute);

        //--------------------------------------------------------------

        //slant-right
        Vector2[] slantRPoints = new Vector2[2];

        slantRPoints[0] = forward * 2;
        slantRPoints[1] = forward_right * 4;
        
        Route slantRRoute = new Route(RouteName.Slant_Right, slantRPoints);

        routes.Add(slantRRoute.routeName, slantRRoute);

        //--------------------------------------------------------------

        //insode-left
        Vector2[] insideLPoints = new Vector2[2];

        insideLPoints[0] = forward * 2;
        insideLPoints[1] = left * 5;
        
        Route insideLRoute = new Route(RouteName.Inside_Left, insideLPoints);

        routes.Add(insideLRoute.routeName, insideLRoute);

        //--------------------------------------------------------------

        //insode-right
        Vector2[] insideRPoints = new Vector2[2];

        insideRPoints[0] = forward * 2;
        insideRPoints[1] = right * 5;
        
        Route insideRRoute = new Route(RouteName.Inside_Right, insideRPoints);

        routes.Add(insideRRoute.routeName, insideRRoute);

        //--------------------------------------------------------------
    }

}

public class Route
{   
    public RouteName routeName;

    //the vectors are relative
    public Vector2[] routePoints;
    int currentRoutePoint = 0;

    public Route(RouteName name, Vector2[] routePoints)
    {   
        this.routeName = name;
        this.routePoints = routePoints;
    }

    public Vector2 GetFirstRoutePoint()
    {
        return routePoints[0];
    }

    public Vector2 GetNextRoutePoint()
    {   
        currentRoutePoint++;
        if(currentRoutePoint < routePoints.Length)
        {
            Vector2 outPoint = routePoints[currentRoutePoint];
            return outPoint;
        }else
        {
            return Vector2.zero;
        }
    }

    public Vector2 peakCurrentRoutePoint()
    {
        if(currentRoutePoint < routePoints.Length)
        {
            return routePoints[currentRoutePoint];
        }else
        {
            //Debug.LogError("Asked for a Routepoint which is not there");
            return Vector2.zero;
        }
    }

    public Vector2 peakNextRoutePoint()
    {
        if(currentRoutePoint+1 < routePoints.Length)
        {
            return routePoints[currentRoutePoint+1];
        }else
        {
            //Debug.LogError("Asked for a Routepoint which is not there");
            return Vector2.zero;
        }
    }

    public void ResetRoute()
    {
        currentRoutePoint = 0;
    }
    
}

public enum RouteName{Post, Slant_Left, Slant_Right, Inside_Left, Inside_Right};
