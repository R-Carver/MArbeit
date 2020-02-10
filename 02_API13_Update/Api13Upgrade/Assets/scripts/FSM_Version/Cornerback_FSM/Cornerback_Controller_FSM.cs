using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornerback_Controller_FSM : MonoBehaviour, IResettable, IBallAwareness
{
    #region Player vars

    public Transform PlayerToCover;

    [HideInInspector]
    public Rigidbody2D myRb;

    public float speed;
    public float rotationSpeed;

    public int reactionDelay;

    [HideInInspector]
    public Ball_Controller ball_controller;
    public GameObject ballGo;

    [HideInInspector]
    public QB_Controller_FSM qB_Controller;

    Vector2 startPos;
    Quaternion startRot;

    #endregion

    #region FSM

    public Cornerback_Base_State currentState;
    public readonly Cornerback_Cover_State cover_State = new Cornerback_Cover_State();
    public readonly Cornerback_Push_State push_State = new Cornerback_Push_State();
    public readonly Cornerback_Intercept_State intercept_State = new Cornerback_Intercept_State();
    public readonly Cornerback_BallCaught_State ballCaught_State = new Cornerback_BallCaught_State();

    #endregion

    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();

        
        ball_controller = GameObject.Find("QB").GetComponent<Ball_Controller>();
        qB_Controller = GameObject.Find("QB").GetComponent<QB_Controller_FSM>();

        ball_controller.ballAwarePlayers.Add(this);
    }

    void Start()
    {
        //ballGo = GameObject.Find("Ball");

        TransitionToState(push_State);

        //save startPos for reset later
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        //register yourself with the GameManager
        GameManager.Instance.allPlayers.Add(this);
    }

    public void TransitionToState(Cornerback_Base_State state)
    {   
        if(currentState != null)
        {
            currentState.CleanUp(this);
        }
        currentState = state;
        currentState.EnterState(this);
    }

    private void FixedUpdate() 
    {
        if(currentState != null)
        {
            currentState.Update(this);
        }

        /*//try to intercept the ball if its in the air
        if(qB_Controller.Receiver_current == PlayerToCover.gameObject)
        {
            //here we make sure that onlt the right cornerback tries to intercept the ball
            if (ball_controller.launched == true)
            {
                TransitionToState(intercept_State);
            }
        } */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        currentState.OnCollisionEnter(this,other);    
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        currentState.OnCollisionExit(this, other);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {   
        currentState.OnTriggerExit(this, other);
    }

    public void Reset()
    {
        this.transform.position = startPos;
        this.transform.rotation = startRot;

        transform.GetComponent<BoxCollider2D>().enabled = true;

        myRb.velocity = Vector2.zero;
        myRb.angularVelocity = 0;

        TransitionToState(push_State);
    }

    public void UpdateBallInstance(GameObject ball)
    {
        ballGo = ball;
    }
}
