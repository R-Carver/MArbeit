using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_instance : MonoBehaviour
{   
    public ZoneName myName;

    Zone myZone;

    // Start is called before the first frame update
    private void Awake() 
    {
        //create your zone based on your collider and add yourself to the Defender_Zones script
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        Vector3 myExtents = myCollider.bounds.extents;

        myZone = new Zone(myName, this.transform.position, myExtents);
        Defender_Zones.currentZones.Add(myZone.name, myZone);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Receiver" && myZone.receiverInZone == null)
        {
            myZone.receiverInZone = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Receiver")
        {   
            //only unset the recevier if its the same whihc entered
            if(other.gameObject == myZone.receiverInZone)
            {
                myZone.receiverInZone = null;
            }
        }
    }
}
