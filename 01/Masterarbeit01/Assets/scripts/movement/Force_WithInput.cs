using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_WithInput : MonoBehaviour
{   
    //public Vector2 forceVector;
    //public Vector2 positionOffsetVector;
    public float rotationPower;
    public float speed;

    public ForceMode2D forceMode;
    //public ForceMode2D forceMode;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        

        float forwardMovement = Input.GetAxis("Vertical") * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;

        //get local forward vector
        //Vector2 localRight = transform.worldToLocalMatrix.MultiplyVector(Vector2.right);
        Vector2 localRight = transform.rotation * Vector2.right;

        rb.AddForce(localRight * forwardMovement * speed);
        rb.AddTorque(-rotation * rotationPower, forceMode);
        

    }
}