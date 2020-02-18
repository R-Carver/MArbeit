using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour, IResettable
{
    //public Rigidbody ball;
    Rigidbody ball;
    public Transform target;
    public Transform  prefab_Ball;

    public float h;
    public float gravity;

    public bool launched = false;
    //public bool activateThrowHack = false;

    Vector3 ballStartPosition;
    Quaternion ballStartRotation;

    //this is the go with the 2d collider
    GameObject ball2D;

    public List<IBallAwareness> ballAwarePlayers = new List<IBallAwareness>();

    void Awake() 
    {
        GameObject ballGo = InstantiateBall();
        UpdateBallAwarePlayers(ballGo);
        ball = ballGo.GetComponent<Rigidbody>();
    }
    void Start(){

        //GameManager.Instance.allPlayers.Add(this);
        Physics.gravity = Vector3.forward * gravity;

        ball.useGravity = false;

        ballStartPosition = ball.gameObject.transform.position;
        ballStartRotation = ball.gameObject.transform.rotation;

        //use this to change the tag of the ball when resetting
        ball2D = GameObject.Find("Ball2d");
    }

    void Update(){

        // keep track if the ball fell of the plane
        if(ball != null)
        {
            if (ball.position.z < 0)
            {
                //make the ball stop when hits the ground for debug
                ball.isKinematic = true;
            }
        }
        
    }

    GameObject InstantiateBall()
    {
        //try to instantiate the ball here to prevent the nothrow bug
        Vector3 qbPos = this.gameObject.transform.position;
        //Vector3 ballPos = new Vector3()
        Transform ballGo  = Instantiate(prefab_Ball, qbPos, Quaternion.identity);
        ballGo.parent = this.gameObject.transform;
        ballGo.transform.position += new Vector3(0f, -0.28f, 0f);
        ballGo.name = "Ball";
        return ballGo.gameObject;
    }

    void UpdateBallAwarePlayers(GameObject ball)
    {
        foreach(IBallAwareness player in ballAwarePlayers)
        {
            player.UpdateBallInstance(ball);
        }
    }

    public void Launch(){
        
        ball.useGravity = true;
        //print("curr LaunchData   " + currentLaunchData.initialVelocity);
        ball.velocity = CalculateLaunchData().initialVelocity;
        //Debug.Log("<color=red> Ball velocity  <b>" + ball.velocity + "</b></color>");
        ball.angularVelocity = Vector3.zero;
        launched = true;

        StartCoroutine(TestForThrowBug(ball.velocity));
    }

    IEnumerator TestForThrowBug(Vector3 velocity)
    {   
        yield return new WaitForSeconds(1f);
        //hack for fixing the "not throwing" bug
        //here we try to find out if the bug is currently happening
        if(launched == true && ball.velocity == Vector3.zero)
        {
            //Debug.Log("<color=teal> Ball velocity  <b>" + ball.velocity + "</b></color>");
            //Debug.Log("<color=teal> Shouldnt be zero  <b>" + velocity + "</b></color>");
            Debug.Log("<color=brown> <b>" + "Hier ist der Fehler" + "</b></color>");

            //activateThrowHack = true;
            //set the velocity again, this should solve the bug
            //ball.velocity = velocity;
            
        }
    }

    public LaunchData CalculateLaunchData(){

        float displacementY = target.position.z - ball.position.z;
        //Debug.Log("displ Y " + displacementY);
        
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, target.position.y - ball.position.y, 0);
        //Debug.Log("disp XZ " + displacementXZ);
        

        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));
        
        Vector3 velocityY = Vector3.forward * Mathf.Sqrt(-2 * gravity * h);
        //Debug.Log("vel Y " + velocityY);
        
        Vector3 velocityXZ = displacementXZ / time;

        Vector3 outVector = new Vector3(velocityXZ.x, velocityXZ.y , velocityY.z);
        //Debug.Log(outVector);

        return new LaunchData(outVector, time);
        
    }


    public int TargetSpeed = 3;
    public void MoveTarget(float zValue)
    {
        // in the current version we only move the target along the route of the receiver
        // which is along the z axis
        Vector3 movement = new Vector3(0, 0, zValue);
        if(target.position.z < 3 && zValue >= 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }

        if(target.position.z > -4 && zValue < 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }
    }

    public void Reset()
    {
        Destroy(ball.gameObject); 
        GameObject ballGo = InstantiateBall();
        UpdateBallAwarePlayers(ballGo);

        ball = ballGo.GetComponent<Rigidbody>();
        ball2D = GameObject.Find("Ball2d");
        ball2D.gameObject.tag = "Ball";

        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        launched = false;

        ball.gameObject.transform.position = ballStartPosition;
        ball.gameObject.transform.rotation = ballStartRotation;
        ball.isKinematic = false;
        ball.transform.parent = this.transform;
        //target.position = new Vector3(3, 0, 0);
    }

    public struct LaunchData{
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}

//for skript which need to access the current ball instanz
public interface IBallAwareness
{
    void UpdateBallInstance(GameObject ball);
}
