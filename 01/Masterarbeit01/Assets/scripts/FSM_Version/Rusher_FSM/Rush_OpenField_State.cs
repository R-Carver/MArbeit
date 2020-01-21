using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush_OpenField_State : Rush_Base_State
{   
    Rigidbody2D myRb;

    public override void CleanUp(Rusher_Controller_FSM rusher)
    {
        
    }

    public override void EnterState(Rusher_Controller_FSM rusher)
    {
        myRb = rusher.myRb;

        rusher.Target = rusher.Qb;
    }

    public override void OnCollisionEnter(Rusher_Controller_FSM rusher, Collider2D col)
    {
      
        //if(other.gameObject.tag == "Defender" && !opponents.Contains(other.gameObject))
        if(col.gameObject.tag == "Defender")
        {
            rusher.Target = col.transform;
            rusher.defender = col.GetComponent<DefenderController_FSM>();

            rusher.TransitionToState(rusher.oppVisible_State);
        }
    
    }

    public override void Update(Rusher_Controller_FSM rusher)
    {
        Vector2 localRight = rusher.transform.rotation * Vector2.right;
        myRb.AddForce(localRight * rusher.speed * Time.deltaTime);

        float angle = Vector2.SignedAngle(rusher.Target.position - rusher.transform.position, new Vector2(1.0f, 0.0f));
        myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rusher.rotationSpeed * Time.deltaTime));
    }
}
