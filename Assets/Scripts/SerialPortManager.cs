using UnityEngine;
using System.IO.Ports;
using System;

public class SerialPortManager : MonoBehaviour
{
    private readonly string port = "/dev/cu.usbmodem1101";
    private readonly int baud = 115200, timeout = 1000;

    protected SerialPort data_stream;
    public static SerialPortManager inst; // static instance for access across scenes

    private void Awake()
        {
            if (inst != null && inst != this)
            {
                Destroy(gameObject);
                Debug.Log("Instance destroyed");
            }
            else
            {
                inst = this;
                DontDestroyOnLoad(gameObject); // Make this GameObject persist across scene changes
                this.InitializePort();
            }
        }

    private void InitializePort(){
        try{
            data_stream = new SerialPort(port, baud);
            data_stream.Open();
            data_stream.ReadTimeout = timeout;
            Debug.Log("Port opened");
        } catch (Exception e){
            Debug.Log(e);
        }
    }

    public void FlushBuffer()
    {
        if (data_stream != null && data_stream.IsOpen)
            data_stream.DiscardInBuffer();
    }

    
    public string Read(){
        try
        {
            return data_stream.ReadLine();

        } catch (Exception e){
            Debug.Log(e);
            return null;
        }

    }

    private void OnDestroy()
    {
        if (data_stream != null && data_stream.IsOpen)
        {
            data_stream.Close();
            Debug.Log("Serial port closed");
        }
    }

    private void OnApplicationQuit()
    {
        if (data_stream != null && data_stream.IsOpen)
        {
            data_stream.Close();
        }
    }




}
