using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    [SerializeField] Scanner scanner;
    [SerializeField] int turnDivide = 100;
    [SerializeField]  float rotAmount = 1f;

    float angle = 1f;
    [HideInInspector] public bool looking = false;

    float leftDistance;
    float rightDistance;
    float scanDistance;

   public  float GetScanDistanceAtAngle(float angle,float maxDis)
    {

        StartCoroutine(TurnHeadTo(angle));
        var d= scanner.GetSensorDistance(maxDis);
        ResetRotation();

        return d;
    }
    public void LookAt(float angle)
    {
        //var q = Quaternion.Euler(0, angle, 0);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, q,Time.fixedDeltaTime);
    }

    public void Scan(CarBody body)
    {
        var qe = transform.localEulerAngles;

        scanDistance = GetScanDistance(body.maxLookDistance);

        print(qe.y);

        if (qe.y<180f&&qe.y > body.lookAngle)
        {
            angle=-rotAmount;
            rightDistance = GetScanDistance(body.maxLookDistance);
        }
        if (qe.y>180f&&qe.y-360f < -body.lookAngle )
        {
            angle= rotAmount;
            leftDistance = GetScanDistance(body.maxLookDistance);
        }
        qe.y += angle;
        transform.localEulerAngles = qe;
    }

    public bool LeftOpen()
    {
        return leftDistance > rightDistance;
    }
    public bool BothOpen(float safeDistance)
    {
        return leftDistance > safeDistance && rightDistance > safeDistance && scanDistance > safeDistance;
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
