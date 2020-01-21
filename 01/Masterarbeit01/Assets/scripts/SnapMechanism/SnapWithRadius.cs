using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapWithRadius : MonoBehaviour
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
    void Update()
    {
        colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, colliderRadius);

        if(colliders.Length == 2)
        {   
            if(joints.Length > 0)
            {   
            
                if(Mathf.Abs(myRb.rotation - 180.0f - otherRb.rotation) < 2.0f )
                {   
                    //Debug.Log(myRb.rotation - 180.0f - otherRb.rotation);

                    //if the angles are close enough then snap
                    joints[0].enabled = true;
                }else
                {   
                    
                    //move first object
                    float angle = Vector2.SignedAngle(otherObject.position - this.transform.position, new Vector2(1.0f, 0.0f));
                    myRb.MoveRotation(-angle);

                    //move second object
                    float angle2 = Vector2.SignedAngle(this.transform.position - otherObject.position, new Vector2(1.0f, 0.0f));
                    otherRb.MoveRotation(-angle2);
                    
                }
            }
            
        }
    }
}
