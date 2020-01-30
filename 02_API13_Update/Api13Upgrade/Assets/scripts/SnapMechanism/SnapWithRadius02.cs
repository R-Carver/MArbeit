using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapWithRadius02 : MonoBehaviour
{   
    public Collider2D[] colliders;
    public Transform otherObject;

    FixedJoint2D[] joints;
    Rigidbody2D myRb;
    Rigidbody2D otherRb;

    float colliderRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        joints = GetComponents<FixedJoint2D>();
        myRb = GetComponent<Rigidbody2D>();

        otherRb = otherObject.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, colliderRadius);

        if(colliders.Length > 0)
        {   
            if(joints.Length > 0)
            {   
                if(joints[0].enabled = false)
                {
                    myRb.angularVelocity = 0.0f;
                    otherRb.angularVelocity = 0.0f;
                }

                joints[0].enabled = true;

            }
            
        }else
        {
            //move first object
            //float angle = Vector2.SignedAngle(otherObject.position - this.transform.position, new Vector2(1.0f, 0.0f));
            //myRb.MoveRotation(-angle);
        }
    }
}
