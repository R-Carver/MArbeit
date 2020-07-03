using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver_StartPositions : MonoBehaviour, IResettable
{   
    public static Receiver_StartPositions Instance;
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

    public static Dictionary<Vector2, bool> availableStartPositions = new Dictionary<Vector2, bool>();
    public static List<Vector2> keyList;
    void Start()
    {
        GameManager.Instance.allPlayers.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {   
        if(keyList == null)
        {
            keyList = new List<Vector2>(availableStartPositions.Keys);
        }
        //make all start positions available again
        foreach(Vector2 key in keyList)
        {
            availableStartPositions[key] = true;
        }
    }

    public Vector2 getRandomStartPos()
    {   
        if(keyList == null)
        {
            keyList = new List<Vector2>(availableStartPositions.Keys);
        }
        int rand = Random.Range(0, keyList.Count);
        Vector2 key = keyList[rand];

        while(availableStartPositions[key] != true)
        {   
            //try new random values until you find an available key
            rand = Random.Range(0, keyList.Count);
            key = keyList[rand];
        }

        //now we have an available key
        availableStartPositions[key] = false;
        return key;
    }
}
