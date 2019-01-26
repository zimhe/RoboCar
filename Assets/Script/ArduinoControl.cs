using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Linq;
using System.Text;

public class ArduinoControl : MonoBehaviour
{

    [SerializeField] private int _comIndex = 3;

    private string _comName = "COM";

    //the input port
    private SerialPort _sp;

    char[] buffer = new char[48];


    // Use this for initialization
    void Start()
    {
        _sp = new SerialPort(_comName + _comIndex, 9600);
        _sp.Open();
        _sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (_sp.IsOpen && _sp.BytesToRead > 0)
    //    {

    //        try
    //        {
    //            string bufferString = "";
    //            _sp.Read(buffer, 0, 2);

    //            buffer.ToList().ForEach(chr => { bufferString += chr.ToString(); });

    //            print(bufferString);
    //            if (bufferString.Contains("#A"))
    //            {
    //                print("Astoped");
    //            }
    //            if (bufferString.Contains("#B"))
    //            {
    //                print("Bstoped");
    //            }
    //        }
    //        catch (Exception e)
    //        {
                
    //        }
    //    }

    //}

    private void OnDisable()
    {
        _sp.Close();
    }
    /// <summary>
    /// set the the index of the motor should be moving, and the state of it 
    /// implement this method in a button component to set a specific motor state
    /// </summary>
    /// <param name="index"></param>
    public void Transmit(string index)
    {
        if (_sp.IsOpen == false) _sp.Open();
        _sp.Write(index);
    }
}
