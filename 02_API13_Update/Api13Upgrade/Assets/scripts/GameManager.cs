using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public List<IResettable> allPlayers = new List<IResettable>();

    [HideInInspector]
    public PassAgent passAgent;

    public GameObject ThrowTrigger;

    //use this to determine whether some player is to the right or left of the qb
    public Transform GameOrigin;

    public float catchableHeight = 1.1f;

    //flags for resetting, the actual resetting is executed on the Agent
    public bool ballCaught = false;
    public bool ballIntercepted = false;
    public bool qbSacked = false;
    public bool ballLaunched = false;
    public bool canThrow = false;

    public TrainingInfo trainingText;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("ResetTest", 10.0f, 10.0f);
    }

    void ResetTest()
    {
        foreach(IResettable player in allPlayers)
        {
            player.Reset();
        }
    }

    public void ScheduleReset()
    {
        Invoke("ResetEnv", 1.0f);
    }

    void ResetEnv()
    {   
        Cornerback_Controller.Instance.ActivateAll();

        foreach(IResettable player in allPlayers)
        {
            player.Reset();
        }

        Cornerback_Controller.Instance.DeactivateOne();

        ManageScoreInformation();

        ballCaught = false;
        qbSacked = false;
        ballIntercepted = false;
        ballLaunched = false;
        canThrow = false;

        ThrowTrigger.SetActive(true);

        //make sure the Agents starts its decision process only after the env is reset
        passAgent.episodeDone = false;
    }

    int count_total = 0;
    int count_qbSacked = 0;
    int count_intercepted = 0;
    int count_ballsCaught = 0;

    void ManageScoreInformation()
    {
        count_total ++;

        if(ballCaught)
        {
            count_ballsCaught ++;
        }else if(ballIntercepted)
        {
            count_intercepted ++;
        }else if(qbSacked)
        {
            count_qbSacked ++;
        }

        float ratio = ((float)count_ballsCaught/(float)count_total);


        Debug.Log("Ratio: " + ratio);
        string info = "total throws:" + count_total + 
            " <color=red> sacks: <b>" + count_qbSacked + "</b>" +
            " interceptions: <b>" + count_intercepted + "</b></color>" +
            " <color=green> balls caught: <b>" + count_ballsCaught + "</b></color>" + 
            " <color=black> success %: <b>" + System.Math.Round(ratio, 2) + "</b></color>";
        Debug.Log(info);

        trainingText.UpdateInfoText(info);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface IResettable
{
    void Reset();
}
