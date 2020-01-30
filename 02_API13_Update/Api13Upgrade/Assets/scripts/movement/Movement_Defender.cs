using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Defender : MonoBehaviour
{   
    public Transform otherObject;

    public Transform QBtransform;

    Rigidbody2D myRb;
    Rigidbody2D otherRb;

    

    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        otherRb = otherObject.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    int frameCounter = 20;
    // Update is called once per frame
    void FixedUpdate()
    {
        //move first object
        float angle = Vector2.SignedAngle(otherObject.position - this.transform.position, new Vector2(1.0f, 0.0f));
        myRb.MoveRotation(-angle);

        if(frameCounter == 0)
        {   
            //for the cross product we need to compare the vectors from the QB to the Receiver
            //to the Vector from the QB to the Defender

            Vector2 defenderQbVector = myRb.transform.position - QBtransform.position;
            Vector2 receiverQbVector = otherObject.transform.position - QBtransform.position;

            if(IsLeft(defenderQbVector, receiverQbVector))
            {
                myRb.AddRelativeForce(Vector2.up * speed);
                Debug.Log("Is left");
            }else
            {
                myRb.AddRelativeForce(Vector2.up * -speed);
                Debug.Log("Is right");
            }
            frameCounter = 20;    
        }else
        {
            frameCounter--;
        }
        
    }

    bool IsLeft(Vector2 A, Vector2 B)
    {
        return (-A.x * B.y + A.y * B.x) < 0;
    }
}
