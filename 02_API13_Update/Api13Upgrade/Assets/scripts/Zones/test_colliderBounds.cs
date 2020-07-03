using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_colliderBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        Debug.Log(myCollider.bounds);
        Debug.Log(myCollider.bounds.center);
        Debug.Log(myCollider.bounds.extents);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
