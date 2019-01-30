using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicalOnlyControl : MonoBehaviour
{
    [SerializeField] KeyCode brake = KeyCode.Space;
    [SerializeField] KeyCode forward = KeyCode.W;
    [SerializeField] KeyCode backward = KeyCode.S;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] bool _transmit;
    [SerializeField] ArduinoControl controller;
    public float lookAngle = 15f;
    public float maxLookDistance = 400f;
    public float safeDistance = 10f;
    public float scanRate = 1f;

    [HideInInspector] public float scanRateUpdate = 1f;

    float currentDistance = 0f;
    float currentAngle = 0f;

    bool leftClose = false;
    bool rightClose = false;


    List<KeyCode> keys;

    [SerializeField] RoboCarControlMode playMode = RoboCarControlMode.Manual;

    int actionMode = 0;

    // Use this for initialization
    void Start()
    {
        controller.Initialize();
        keys = new List<KeyCode> { brake, forward, backward, left, right };
        scanRateUpdate = scanRate;

        Stop();
    }

    public void Stop()
    {
        if (_transmit)
        {
            controller.Transmit("s");
        }
     
    }
    public void Forward()
    {
        if (_transmit)
        {
            controller.Transmit("f");
        }
      
    }
    public void Backward()
    {
        if (_transmit)
        {
            controller.Transmit("b");
        }
     
    }
    public void Left()
    {
        if (_transmit)
        {
            controller.Transmit("l");
        }
     
    }

    public void Right()
    {
        if (_transmit)
        {
            controller.Transmit("r");
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

    public void DoAction(int action = -1)
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

            default:
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

            JoystickInput();
        }
    }


    void ExcecuteAuto()
    {
        if (playMode == RoboCarControlMode.Automatic)
        {
            float.TryParse(controller.listener.dataList[0],out currentDistance);
            float.TryParse(controller.listener.dataList[1], out currentAngle);

            print("angle: " + currentAngle);
            print("distance: " + currentDistance);

            if (currentAngle >=90f + lookAngle )
            {
                if (currentDistance < safeDistance)
                {
                    rightClose = true;
                }
                else
                {
                    rightClose = false;
                }
            }
            else if (currentAngle <= 90f - lookAngle)
            {
                if (currentDistance < safeDistance)
                {
                    leftClose = true;
                }
                else
                {
                    leftClose = false;
                }
            }

            if (leftClose && rightClose)
            {
                Backward();
            }
            else
            {
                if (currentDistance < safeDistance && currentAngle > 90f)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        Left();
                    }
                 
                }
                else if (currentDistance < safeDistance && currentAngle < 90f)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Right();
                    }
                }
                else
                {
                    Forward();
                }
            }
        }
    }

    void JoystickInput()
    {
        var hor = Input.GetAxis("Horizontal");

        var fwd = Input.GetAxis("Forward");

        var bak = Input.GetAxis("Backward");

        if (hor == 0f && fwd == 0f && bak == 0f)
        {
            Stop();
        }

        if (hor >= 0.6f)
        {
            Right();
        }
        if (hor <= -0.6f)
        {
            Left();
        }
        if (fwd >= 0.6f)
        {
            Forward();
        }
        if (bak >= 0.6f)
        {
            Backward();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        controller.Listen();
        ExcecuteManual();
        ExcecuteAuto();
    }

    public enum RoboCarControlMode
    {
        Manual, Automatic
    }
}
