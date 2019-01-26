using System.Collections;
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
    [SerializeField] bool _transmit;
    [SerializeField] ArduinoControl controller;
    public float lookAngle = 15f;
    public float maxLookDistance = 100f;
    public float safeDistance = 10f;

    
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
        if (_transmit)
        {
            controller.Transmit("s");
        }
        foreach (var w in leftWheels )
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
        if (_transmit)
        {
            controller.Transmit("f");
        }
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
        if (_transmit)
        {
            controller.Transmit("b");
        }
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
        if (_transmit)
        {
            controller.Transmit("l");
        }
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
        if (_transmit)
        {
            controller.Transmit("r");
        }
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
            head.Scan(this);

            if (head.BothOpen(safeDistance))
            {
                Forward();
            }
            else
            {
                Stop();

                if (head.LeftOpen())
                {
                    Left();
                }
                else
                {
                    Right();
                }
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
