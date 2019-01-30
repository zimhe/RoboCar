using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    [SerializeField] Scanner scanner;
    [SerializeField] int turnDivide = 100;
    

    float angle = 1f;
    [HideInInspector] public bool looking = false;

    float leftDistance;
    float rightDistance;
    float scanDistance;

  
    public void LookAt(float angle)
    {
        //var q = Quaternion.Euler(0, angle, 0);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, q,Time.fixedDeltaTime);
    }

    public float HeadAngle(CarBody body)
    {
        var left = -body.transform.right;
        var head = transform.forward;

        return Vector3.Angle(left, head);
    }

    public float Scan(CarBody body)
    {
        var qe = transform.localEulerAngles;

        qe.y += body.scanRateUpdate;
        transform.localEulerAngles = qe;
        scanDistance= GetScanDistance(body);

        return scanDistance;
    }
      
    public bool LeftOpen()
    {
        return leftDistance > rightDistance;
    }
    public bool BothOpen(float safeDistance)
    {
        return leftDistance > safeDistance && rightDistance > safeDistance && scanDistance > safeDistance;
    }



    public float GetScanDistance(CarBody body)
    {
        return scanner.GetSensorDistance(body);
    }
}
