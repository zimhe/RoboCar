﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBody : MonoBehaviour {

    [SerializeField] Wheel[] leftWheels;
    [SerializeField] Wheel[] rightWheels;
    [SerializeField] Head head;

    [SerializeField] KeyCode brake = KeyCode.Space;
    [SerializeField]  KeyCode forward=KeyCode.W;
    [SerializeField]  KeyCode backward = KeyCode.S;
    [SerializeField]  KeyCode left = KeyCode.A;
    [SerializeField]  KeyCode right = KeyCode.D;
    [SerializeField] float lookAngle = 30f;
    [SerializeField] float maxLookDistance = 100f;
    [SerializeField] float safeDistance = 10f;

    List<KeyCode> keys;

    [SerializeField]RoboCarControlMode playMode = RoboCarControlMode.Manual;

    int actionMode = 0;

    // Use this for initialization
    void Start ()
    {
        keys = new List<KeyCode>{ brake, forward, backward, left, right };
        Stop();
	}

    public void Stop()
    {
        foreach(var w in leftWheels )
        {
            w.Stop();
        }
        foreach (var w in rightWheels)
        {
            w.Stop();
        }
    }
    public void Forward()
    {
        foreach (var w in leftWheels)
        {
            w.Forward();
        }
        foreach (var w in rightWheels)
        {
            w.Forward();
        }
    }
    public void Backward()
    {
        foreach (var w in leftWheels)
        {
            w.Backward();
        }
        foreach (var w in rightWheels)
        {
            w.Backward();
        }
    }
    public void Left()
    {
        foreach (var w in leftWheels)
        {
            w.Backward();
        }
        foreach (var w in rightWheels)
        {
            w.Forward();
        }
    }

    public void Right()
    {
        foreach (var w in leftWheels)
        {
            w.Forward();
        }
        foreach (var w in rightWheels)
        {
            w.Backward();
        }
    }

    public void CheckInputKey(KeyCode key)
    {
        if (!keys.Contains(key)) return;

        if (Input.GetKey(key))
        {
            actionMode = keys.IndexOf(key);
        }
        if (Input.GetKeyUp(key))
        {
            actionMode = 0;
        }
    }

    public void DoAction(int action=-1)
    {
        if (action != -1)
        {
            actionMode = action;
        }

        switch (actionMode)
        {
            case 0:
                Stop();
                break;
            case 1:
                Forward();
                break;
            case 2:
                Backward();
                break;
            case 3:
                Left();
                break;
            case 4:
                Right();
                break;

            default  :
                Stop();
                break;
        }
    }

    void ExcecuteManual()
    {
        if (playMode == RoboCarControlMode.Manual)
        {
            foreach (var k in keys)
            {
                CheckInputKey(k);
            }
            DoAction();
        }
    }
    void ExcecuteAuto()
    {
        if (playMode == RoboCarControlMode.Automatic)
        {
            if (head.GetScanDistance(maxLookDistance) > safeDistance)
            {
                if (!head.looking)
                    DoAction(1);
            }
            else
            {
                head.looking = true;
                Stop();

                head.LookAt(lookAngle);

                //head.LookAt(-lookAngle);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        ExcecuteManual();
        ExcecuteAuto();

	}

    public enum RoboCarControlMode
    {
        Manual, Automatic
    }
}
