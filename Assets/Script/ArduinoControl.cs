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

    public SerialListener listener;


    // Use this for initialization
    public void Initialize()
    {
        _sp = new SerialPort(_comName + _comIndex, 9600);
        _sp.Open();
        _sp.ReadTimeout = 10;
        listener.Listen(_sp);
    }

    //Update is called once per frame
    void Update()
    {
        //listener.Listen(_sp);
    }

    public void Listen()
    {
        //Debug.Log("listen");
        listener.Listen(_sp);
    }

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

        //_sp.DiscardInBuffer();
        _sp.Write(index);
    }
}
