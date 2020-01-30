using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_Controller_FSM : MonoBehaviour, IResettable
{
    #region Player vars

    public Transform ThrowTarget;

    public GameObject Receiver_current;
    public GameObject[] Receivers;

    Ball_Controller ball_Controller;

    public Transform prefab_TargetIndicator;

    public float targetOffset;

    bool isSacked = false;

    //for testing the ml agents action values
    public int receiverAction = 0;

    Vector2 startPos;
    Quaternion startRot;

    #endregion

    #region FSM

    public QB_Base_State currentState;

    public readonly QB_ChooseTarget_State chooserTarget_State = new QB_ChooseTarget_State();
    public readonly QB_Throw_State throw_State = new QB_Throw_State();
    public readonly QB_Sacked_State sacked_State = new QB_Sacked_State();

    #endregion
    
    void Awake()
    {
        GameObject receiversObj = GameObject.Find("Receivers");
        Receivers = new GameObject[receiversObj.transform.childCount];

        int childIndex = 0;
        foreach(Transform receiver in receiversObj.transform)
        {
            Receivers[childIndex++] = receiver.gameObject;
        }

        ball_Controller = GetComponent<Ball_Controller>();

    }
    
    
    // Start is called before the first frame update
    void Start()
    {   
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        //Change this later
        TransitionToState(chooserTarget_State);  

        //register yourself with the GameManager
        GameManager.Instance.allPlayers.Add(this);  
    }

    public void TransitionToState(QB_Base_State state)
    {   
        if(currentState != null)
        {
            currentState.CleanUp(this);
        }
        currentState = state;
        currentState.EnterState(this);
    }

    void Update() 
    {
        if(currentState != null)
        {
            currentState.Update(this);
        }

        /*if(receiverAction > 0 && receiverAction <= Receivers.Length && ball_Controller.launched == false)
        {
            ChooseReceiver(receiverAction);
        }*/    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.tag == "PassRusher")
        {   
            if(isSacked == false)
            {   
                isSacked = true;
                TransitionToState(sacked_State);
            }
        }
        
    }

    public Transform InstantiateTargetIndicator(Vector3 position)
    {
        return Instantiate(prefab_TargetIndicator, position, Quaternion.identity);
    }

    public void ChooseReceiver(int receverIndex)
    {
        Receiver_current = Receivers[receverIndex - 1] ;
        TransitionToState(throw_State);
    }

    public void Reset()
    {
        this.transform.position = startPos;
        this.transform.rotation = startRot;

        isSacked = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        TransitionToState(chooserTarget_State);
    }
}
