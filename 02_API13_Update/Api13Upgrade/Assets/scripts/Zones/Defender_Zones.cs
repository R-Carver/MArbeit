using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_Zones
{   
    public static Defender_Zones Instance;

    public Defender_Zones()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.LogError("There shouldnt ever be 2 Defender Zone Classes");
        }
    }

    //dict form name to zone.
    //tha players take their zones from this dict
    public static Dictionary<ZoneName, Zone> currentZones = new Dictionary<ZoneName, Zone>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

public class Zone
{
    public ZoneName name;

    public Vector2 centerPoint;
    public Vector3 extents;

    public GameObject receiverInZone;

    public Zone(ZoneName name, Vector2 centerPoint, Vector3 extents)
    {
        this.name = name;
        this.centerPoint = centerPoint;
        this.extents = extents;

        //initially there is no receiver in the zone
        this.receiverInZone = null;
    }

}

public enum ZoneName{
        Flat_Right,
        Flat_Left,
        Hook_Right,
        Hook_Left,
        Deep_Right,
        Deep_Left
    };