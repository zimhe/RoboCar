using UnityEngine;
using UnityEditor;
using System.IO.Ports;
using System;

[CreateAssetMenu(menuName ="ArduinoControl/Listener/AngleDistance")]
public class AngleDistanceListener : SerialListener
{
    public override void Listen(SerialPort port)
    {
        
        MakeList();
        //Debug.Log("dataList"+dataList.Length);

        if (port.IsOpen && port.BytesToRead > 0)
        {
            try
            {
                string bufferString = "";

                bufferString = port.ReadLine();

                if (bufferString.StartsWith("#"))
                {
                    dataList[0] = bufferString.Remove(0,1);
                    Debug.Log("Distance: "+dataList[0]);
                }
                if (bufferString.StartsWith("/"))
                {
                    dataList[1] = bufferString.Remove(0,1);
                    Debug.Log("Angle: "+dataList[1]);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}