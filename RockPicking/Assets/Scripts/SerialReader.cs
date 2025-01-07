using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.Events;

public class SerialReader : MonoBehaviour
{
    private SerialPort serialPort = new SerialPort();
    private string message;
    private string lastMessage = "";

    public string portName;
    public int baudRate;

    public UnityEvent newMessage;
    
    void Start()
    {
        serialPort.PortName = portName;
        serialPort.BaudRate = baudRate;
        serialPort.ReadTimeout = 5;
        serialPort.Open();
    }
    
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                message = serialPort.ReadLine();
            }
            catch (System.TimeoutException)
            {
                // Ignore TimeOutException
            }
            if (message != lastMessage)
            {
                Debug.Log("Serial Received:" + message);
                newMessage.Invoke();
				lastMessage = message;
                HandleMessage(lastMessage);
            }
        }
    }

    public string GetLastMessage()
    {
        return message;
    }

    public virtual void HandleMessage(string message)
    {
        return;
    }
}
