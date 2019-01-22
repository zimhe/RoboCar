using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float GetSensorDistance(float maxDis)
    {
        float dis;
        RaycastHit hit;
        bool hitSomething;

        Color col = Color.green;

        hitSomething = Physics.Raycast(transform.position,transform.forward,out hit,maxDis);

     

        if (hitSomething)
        {
            dis = hit.distance;
            col = Color.red;
        }
        else
        {
            dis = maxDis;
        }

        Debug.DrawRay(transform.position, transform.forward * dis, col);

        return dis;
    }

}
