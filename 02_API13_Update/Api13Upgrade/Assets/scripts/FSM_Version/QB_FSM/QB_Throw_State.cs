using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_Throw_State : QB_Base_State
{   
    Receiver_Controller_FSM targetReceiver;
    Transform targetIndicator;

    Ball_Controller ball_Controller;

    float targetThreshold = 0.3f;
    public override void CleanUp(QB_Controller_FSM qb)
    {
        targetIndicator.gameObject.SetActive(false);
    }

    public override void EnterState(QB_Controller_FSM qb)
    {   
        targetReceiver = qb.Receiver_current.GetComponent<Receiver_Controller_FSM>();
        ball_Controller = qb.GetComponent<Ball_Controller>();

        //this is so that we throw imedialty when we enter the state
        if(targetIndicator == null)
        {
            targetIndicator = qb.InstantiateTargetIndicator(CalcTarget(qb));
        }else
        {   
            targetIndicator.gameObject.SetActive(true);
            targetIndicator.position = CalcTarget(qb);
        }
        
        ball_Controller.target = targetIndicator;
        ball_Controller.Launch();
        GameManager.Instance.ballLaunched = true;
    }

    public override void OnCollisionEnter(QB_Controller_FSM qb)
    {
        
    }

    public override void Update(QB_Controller_FSM qb)
    {   
        //for ml this mechanism is changed. We throw emediatly after entering this state 
        /*if(targetIndicator == null)
        {   
            targetIndicator =  qb.InstantiateTargetIndicator(CalcTarget(qb));
        }
        targetIndicator.position = CalcTarget(qb);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ball_Controller.target = targetIndicator;
            ball_Controller.Launch();
        }*/

    }

    public Vector2 CalcTarget(QB_Controller_FSM qb)
    {   
        //create the target on the current RouteVEctor - it might not fit onto it

        Vector2 dir = targetReceiver.currentRoute.peakCurrentRoutePoint().normalized * qb.targetOffset;
        Vector2 target = (Vector2)targetReceiver.transform.position + dir;

        if((targetReceiver.currentTarget - (Vector2)targetReceiver.transform.position).magnitude < qb.targetOffset)
        //if((targetReceiver.currentTarget - target).magnitude < targetThreshold)
        {
            //Target doesnt fit on current vector
            //so now the we need to consider the position of the receiver
            float distToEdge = qb.targetOffset - (targetReceiver.currentTarget - (Vector2)targetReceiver.transform.position).magnitude;
            dir = targetReceiver.currentRoute.peakNextRoutePoint().normalized * distToEdge;
            target = targetReceiver.currentTarget + dir;
        }
        
        return target;
    }
}
