using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    JointMotor _motor;

    const float defVelocity = 150;

    float _velocity = 30f;
     
    public HingeJoint GetHingeJoint()
    {
        return GetComponent<HingeJoint>();
    }

    public void SetSpeed(float vel)
    {
        _velocity = vel;
        _motor = GetHingeJoint().motor;
        _motor.targetVelocity = vel;
        GetHingeJoint().motor = _motor;
    }

    public void Forward()
    {
        if (_velocity == 0)
        {
            _velocity = -defVelocity;

            SetSpeed(_velocity);
        }
        else
        {
            if (_velocity < 0)
            {
                return;
            }
            else
            {
                _velocity = -_velocity;

                SetSpeed(_velocity);
            }
        }
    }

    public void Backward()
    {
        if (_velocity == 0)
        {
            _velocity = defVelocity;

            SetSpeed(_velocity);
        }
        else
        {
            if (_velocity > 0)
            {
                return;
            }
            else
            {
                _velocity = -_velocity;

                SetSpeed(_velocity);
            }
        }
    }

    public void Stop()
    {
        _velocity = 0f;
        SetSpeed(0f);
    }

    public bool IsStop()
    {
        return _velocity == 0;
    }
}
