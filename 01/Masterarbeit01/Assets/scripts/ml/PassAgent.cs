using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent : Agent
{   
    QB_Controller_FSM qB_Controller;
    GameManager gameManager;

    bool firstStart;

    // Start is called before the first frame update
    void Start()
    {
        qB_Controller = GetComponent<QB_Controller_FSM>();
        gameManager = GameManager.Instance;

        firstStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AgentReset()
    {
        Debug.Log("Agent was reset");
        if(!firstStart)
        {
            GameManager.Instance.ScheduleReset();
        }

        firstStart = false;
        
    }

    public override void CollectObservations()
    {   
        //dont allow to throw when the receivers are not ready
        if(gameManager.canThrow == false)
        {
            SetActionMask(0, new int[3]{1, 2, 3});
        }

        //dont allow to throw when the ball is caught
        if(gameManager.ballLaunched == true)
        {
            SetActionMask(0, new int[3]{1, 2, 3});
        }
    }

    public override void AgentAction(float[] vectorAction)
    {   
        //Debug.Log(vectorAction[0]);
        if((int)vectorAction[0] == 0)
        {
            //do nothing
        }else
        {
            qB_Controller.ChooseReceiver((int)vectorAction[0]);
        }

        if(gameManager.ballCaught || gameManager.qbSacked || gameManager.ballIntercepted) 
        {   
            if(gameManager.ballCaught)
            {
                SetReward(1.0f);
            }
            if(gameManager.qbSacked)
            {
                SetReward(-1.0f);
            }
            if(gameManager.ballIntercepted)
            {
                SetReward(-1.0f);
            }

            Done();
        }
        
    }

    public override float[] Heuristic()
    {
        var action = new float[1];

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            action[0] = 1;
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            action[0] = 2;
        }else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            action[0] = 3;
        }else
        {
            action[0] = 0;
        }

        return action;
    }
}
