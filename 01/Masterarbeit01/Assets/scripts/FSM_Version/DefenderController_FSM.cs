using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderController_FSM : MonoBehaviour,IResettable
{   
    #region Player vars

    public Transform otherObject;
    public Transform QBtransform;

    [HideInInspector]
    public Rigidbody2D myRb;
    [HideInInspector]
    public Rigidbody2D otherRb;

    //[HideInInspector]
    public Collider2D[] colliders;
    [HideInInspector]
    public FixedJoint2D joint;

    public float speed = 0.5f;
    public float pushPower = 1.0f;
    float colliderRadius = 0.4f;

    Vector2 startPos;
    Quaternion startRot;
    #endregion

    #region FSM

    public DefenderBaseState currentState;

    public readonly Def_PassBlock_State passBlock_State = new Def_PassBlock_State();
    public readonly Def_Snap_State snap_State = new Def_Snap_State();
    public readonly Def_PassBlock_Push_State push_State = new Def_PassBlock_Push_State();
    public readonly Def_Idle_State idle_state = new Def_Idle_State();
    public readonly Def_KnockedOver_State down_state = new Def_KnockedOver_State();

    #endregion

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        otherRb = otherObject.gameObject.GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();

    }

    // Start is called before the first frame update
    void Start()
    {   
        //register yourself with the GameManager
        GameManager.Instance.allPlayers.Add(this);

        //save startPos for reset later
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        TransitionToState(passBlock_State);
    }

    public void TransitionToState(DefenderBaseState state)
    {   
        if(currentState != null)
        {
            currentState.CleanUp(this);
        }
        currentState = state;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {   
        //only check for the oposing team
        LayerMask oposingMask = LayerMask.GetMask("BlueTeam");

        colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, colliderRadius, oposingMask);

        currentState.Update(this);
    }

    void Reset() {
        
    }

    void IResettable.Reset()
    {
        this.transform.position = startPos;
        this.transform.rotation = startRot;

        myRb.angularVelocity = 0;
        myRb.velocity = Vector2.zero;

        TransitionToState(passBlock_State);
    }
}
