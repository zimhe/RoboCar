using UnityEngine;
using UnityEditor;
using System.IO.Ports;

public abstract class SerialListener : ScriptableObject
{
    [HideInInspector]public string[] dataList;
    public int dataNumber = 1;
    

    public void MakeList()
    {
        if (dataList.Length==0)
        {
            dataList = new string[dataNumber];
        }
    }

    public abstract void Listen(SerialPort port);


}