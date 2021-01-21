using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSensor_Generic : MonoBehaviour
{

    public float Range;
    public bool ActiveDebugRay;

    private int layerMask;

    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        Range = 200;
        ActiveDebugRay = false;
    }

    void FixedUpdate()
    {
        Raycast();
        
    }

    void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Range, layerMask))
        {
            if (ActiveDebugRay)
            {
                Debug.DrawRay(transform.position, Vector3.forward * hit.distance, Color.red);
            }
        }
        else
        {
            if (ActiveDebugRay)
            {
                Debug.DrawRay(transform.position, Vector3.forward * Range, Color.yellow);
            }
        }
    }
}
