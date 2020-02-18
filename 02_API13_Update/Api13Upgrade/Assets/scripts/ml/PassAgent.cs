using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent : Agent
{   
    QB_Controller_FSM qB_Controller;
    GameManager gameManager;

    bool firstStart;
    
    //keeps track whether the learning part of this episode is done,
    //such that some visual things can happen without constantly resetting the agent
    public bool episodeDone;

    float targetTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        qB_Controller = GetComponent<QB_Controller_FSM>();
        gameManager = GameManager.Instance;
        //register yourself with the GameManager
        gameManager.passAgent = this;

        firstStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
    }

    public override void AgentReset()
    {
        //Debug.Log("Agent was reset");
        if(!firstStart)
        {
            GameManager.Instance.ScheduleReset();
        }

        firstStart = false;

        //learning reset bug
        targetTime = 30.0f;
    }

    public override void CollectObservations()
    {   
        //dont allow to throw when the receivers are not ready
        if(gameManager.canThrow == false)
        {
            SetActionMask(0, new int[5]{1, 2, 3, 4, 5});
        }

        //dont allow to throw when the ball is thrown
        if(gameManager.ballLaunched == true)
        {
            SetActionMask(0, new int[5]{1, 2, 3, 4, 5});
        }
    }

    public override void AgentAction(float[] vectorAction)
    {   
        //Debug.Log("Agent Action" + vectorAction[0]);
        if((int)vectorAction[0] == 0)
        {
            //do nothing
        }else
        {
            qB_Controller.ChooseReceiver((int)vectorAction[0]);
        }

        if(episodeDone == false)
        {
            if (gameManager.ballCaught || gameManager.qbSacked || gameManager.ballIntercepted)
            {
                if (gameManager.ballCaught)
                {
                    SetReward(1.0f);
                }
                if (gameManager.qbSacked)
                {
                    SetReward(-1.0f);
                }
                if (gameManager.ballIntercepted)
                {
                    SetReward(-1.0f);
                }
                Done();
                episodeDone = true;
            }

            //learning reset bug
            if(targetTime <= 0.0f)
            {   
                Debug.LogError("Learning reset Error occured");
                Done();
                episodeDone = true;
            }
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
        }else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            action[0] = 4;
        }else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            action[0] = 5;
        }else
        {
            action[0] = 0;
        }

        return action;
    }
}
