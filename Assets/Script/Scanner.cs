using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float GetSensorDistance(CarBody body)
    {
        float dis;
        RaycastHit hit;
        bool hitSomething;

        Color col = Color.green;

        hitSomething = Physics.Raycast(transform.position,transform.forward,out hit,body.maxLookDistance);

     

        if (hitSomething)
        {
            dis = hit.distance;
            col = Color.yellow;
            if (hit.distance < body.safeDistance)
            {
                col = Color.red;
            }
        }
        else
        {
            dis = body.maxLookDistance;
        }

        Debug.DrawRay(transform.position, transform.forward * dis, col);

        return dis;
    }

}
