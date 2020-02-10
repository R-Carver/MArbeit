using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_KnockedOver_State : DefenderBaseState
{
    
    GameObject oldSpriteObj;
    GameObject newSpriteObj;

    public override void EnterState(DefenderController_FSM player)
    {   
        Color col = player.GetComponent<SpriteRenderer>().color;
        col.a = 0.3f;
        player.GetComponent<SpriteRenderer>().color = col;

        oldSpriteObj = player.transform.Find("ImgDir").gameObject;
        newSpriteObj = player.transform.Find("ImgDown").gameObject;

        //clean up the old object

        player.myRb.isKinematic = true;
        player.myRb.velocity = Vector2.zero;
        player.myRb.angularVelocity = 0;

        player.joint.enabled = false;
        player.GetComponent<Collider2D>().enabled = false;

        oldSpriteObj.SetActive(false);
        newSpriteObj.SetActive(true);
    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    public override void Update(DefenderController_FSM player)
    {
        
    }

    public override void CleanUp(DefenderController_FSM player)
    {
        Color col = player.GetComponent<SpriteRenderer>().color;
        col.a = 1f;
        player.GetComponent<SpriteRenderer>().color = col;

        player.myRb.isKinematic = false;

        //player.joint.enabled = true;
        player.GetComponent<Collider2D>().enabled = true;

        oldSpriteObj.SetActive(true);
        newSpriteObj.SetActive(false);
    }
    
}
