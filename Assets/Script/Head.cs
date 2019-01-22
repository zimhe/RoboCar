using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    [SerializeField] Scanner scanner;
    [SerializeField] int turnDivide = 100;

    [HideInInspector] public bool looking = false;

   public  float GetScanDistanceAtAngle(float angle,float maxDis)
    {

        StartCoroutine(TurnHeadTo(angle));
        var d= scanner.GetSensorDistance(maxDis);
        ResetRotation();

        return d;
    }
    public void LookAt(float angle)
    {
        var q = Quaternion.Euler(0, angle, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, q,Time.fixedDeltaTime);
    }

    IEnumerator TurnHeadTo(float angle)
    {
        //float ta = transform.localEulerAngles.y;
        float a = (angle) / turnDivide;

        for(int i = 0; i < turnDivide; i++)
        {
            transform.RotateAround(transform.position, transform.up, a);
           
            //print(ta);
            yield return new WaitForFixedUpdate();
        }
    }

    void ResetRotation()
    {
        StartCoroutine(TurnHeadTo(0f));
    }

    public float GetScanDistance(float maxDis)
    {
        return scanner.GetSensorDistance(maxDis);
    }



}
