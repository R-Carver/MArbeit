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

    public float catchableHeight = 1.3f;

    //flags for resetting, the actual resetting is executed on the Agent
    public bool ballCaught = false;
    public bool ballIntercepted = false;
    public bool qbSacked = false;
    public bool ballLaunched = false;
    public bool canThrow = false;

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
        foreach(IResettable player in allPlayers)
        {
            player.Reset();
        }
        ballCaught = false;
        qbSacked = false;
        ballIntercepted = false;
        ballLaunched = false;
        canThrow = false;

        ThrowTrigger.SetActive(true);

        //make sure the Agents starts its decision process only after the env is reset
        passAgent.episodeDone = false;
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
