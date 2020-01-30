/* Rusher Movement with addForce and moveRotation*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Rusher_01 : MonoBehaviour
{   

    public Transform Target;
    private Rigidbody2D myRb;

    public float speed;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 localRight = transform.rotation * Vector2.right;

        myRb.AddForce(localRight * speed * Time.deltaTime);

        float angle = Vector2.SignedAngle(Target.position - this.transform.position, new Vector2(1.0f, 0.0f));
        //myRb.MoveRotation(-angle);
        myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rotationSpeed * Time.deltaTime));
    }
}
