using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
public class Receiver_Controller_FSM : MonoBehaviour, IResettable
{
    #region Player vars

    public RouteName currentRouteName;
    public Route currentRoute;
    //public Transform currentTarget;
    public Vector2 currentTarget;

    public PlayerSide mySide;

    [HideInInspector]
    public Rigidbody2D myRb;
    public float speed;
    public float rotationSpeed;

    //for the editor
    public Vector3 startPos;
    Quaternion startRot;

    #endregion

    #region FSM

    public Receiver_Base_State currentState;
    public readonly Receiver_RunRoute_State runRoute_State = new Receiver_RunRoute_State();
    public readonly Receiver_RunFinished_State runFinished_State = new Receiver_RunFinished_State();

    #endregion

    void Awake()
    {   
        Receiver_Routes routes_script = new Receiver_Routes();
        myRb = GetComponent<Rigidbody2D>();

        startPos = this.transform.position;
        startRot = this.transform.rotation;

        //get a random spped for this player
        speed = Random.Range(1.0f, 2.0f);

    }

    // Start is called before the first frame update
    void Start()
    {   
        //register yourself with the GameManager
        GameManager.Instance.allPlayers.Add(this);

        //set the current Route depending on the chosen route Name
        //we use the one from the routes dict as template and create a new one so its not shared
        mySide = checkPlayerSide();
        
        //currentRoute = new Route(currentRouteName, Receiver_Routes.Instance.routes[currentRouteName].routePoints);
        currentRoute = Receiver_Routes.getRandomSideRoute(mySide);
        currentRouteName = currentRoute.routeName;

        //set the first Target
        //currentTarget = this.transform.position;
        currentTarget = (Vector2)this.transform.position + currentRoute.GetFirstRoutePoint();

        TransitionToState(runRoute_State);
    }

    private PlayerSide checkPlayerSide()
    {
        if(this.transform.position.y > GameManager.Instance.GameOrigin.position.y)
        {
            //this means the player is on the left side
            return PlayerSide.Left;
        }else
        {   
            //right side
            return PlayerSide.Right;
        }
    }

    public void TransitionToState(Receiver_Base_State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(TargetReached())
        {
            //currentTarget = (Vector2)this.transform.position + currentRoute.GetNextRoutePoint();
            currentTarget = currentTarget + currentRoute.GetNextRoutePoint();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //TODO: change the state implementation later to OnTriggerEnter instead of OnCollisionEnter
        currentState.OnCollisionEnter(this, other);
    }

    void FixedUpdate() 
    {   
        if(currentState != null)
        {
            currentState.Update(this);
        }   
    }

    public bool TargetReached()
    {
        if((currentTarget - (Vector2)this.transform.position).magnitude < 0.25f)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public void Reset()
    {
        this.transform.position = startPos;
        //this.transform.position = Receiver_StartPositions.Instance.getRandomStartPos();
        this.transform.rotation = startRot;

        //get a random spped for this player
        speed = Random.Range(1.0f, 2.0f);

        myRb.angularVelocity = 0;
        myRb.velocity = Vector2.zero;

        currentRoute.ResetRoute();
        currentRoute = Receiver_Routes.getRandomSideRoute(mySide);
        currentRouteName = currentRoute.routeName;

        //set the first Target
        //currentTarget = this.transform.position;
        currentTarget = (Vector2)this.transform.position + currentRoute.GetFirstRoutePoint();

        TransitionToState(runRoute_State);
    }
}
}
