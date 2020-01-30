using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_Sacked_State : QB_Base_State
{   
    GameObject normal_img;
    GameObject sacked_img;

    public override void CleanUp(QB_Controller_FSM qb)
    {
        normal_img.SetActive(true);
        sacked_img.SetActive(false);
    }

    public override void EnterState(QB_Controller_FSM qb)
    {
        normal_img = qb.transform.Find("Img_normal").gameObject;
        sacked_img = qb.transform.Find("Img_sacked").gameObject;

        normal_img.SetActive(false);
        sacked_img.SetActive(true);

        GameManager.Instance.qbSacked = true;
    }

    public override void OnCollisionEnter(QB_Controller_FSM qb)
    {
        
    }

    public override void Update(QB_Controller_FSM qb)
    {
        
    }
}
