using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZoneCoverage
{
public class Cornerback_GoToZone_State : Cornerback_Base_State
{
    public override void CleanUp(Cornerback_Controller_FSM cornerback)
    {
        
    }

    public override void EnterState(Cornerback_Controller_FSM cornerback)
    {
        cornerback.zone = Defender_Zones.currentZones[cornerback.zoneName];
    }

    public override void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
        
    }

    public override void OnCollisionExit(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
        
    }

    public override void OnTriggerEnter(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }

    public override void OnTriggerExit(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }

    public override void Update(Cornerback_Controller_FSM cornerback)
    {   
        if(cornerback.zone.receiverInZone != null)
        {
            cornerback.PlayerToCover = cornerback.zone.receiverInZone.transform;
            cornerback.TransitionToState(cornerback.cover_inZone_State);
        }

        Vector3 zonePos = cornerback.zone.centerPoint;

        //if didnt reach center of zone then move towards it
        if ((zonePos - cornerback.transform.position).magnitude > 0.5f)
        {
            Vector2 localRight = cornerback.transform.rotation * Vector2.right;
            cornerback.myRb.AddForce(localRight * cornerback.speed * Time.deltaTime);

            float angle = Vector2.SignedAngle(zonePos - cornerback.transform.position, new Vector2(1.0f, 0.0f));
            cornerback.myRb.MoveRotation(Mathf.LerpAngle(cornerback.myRb.rotation, -angle, cornerback.rotationSpeed * Time.deltaTime));
        }else
        {
            //look to the opposing side and wait
            float angle = Vector2.SignedAngle(Vector2.left, new Vector2(1.0f, 0.0f));
            cornerback.myRb.MoveRotation(Mathf.LerpAngle(cornerback.myRb.rotation, -angle, cornerback.rotationSpeed * Time.deltaTime));
        }

        
    }   
}
}
