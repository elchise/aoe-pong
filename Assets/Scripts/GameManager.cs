using UnityEngine;
using System;
// using System.IO.Ports;
using System.Threading;
// using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Concurrent;
// using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    // protected SerialPort data_stream = new SerialPort("/dev/cu.usbmodem101", 115200);
    protected string receivedString;

    private int _p1Score, _p2Score;

    public Ball ball;
    public PlayerPaddle p2;
    public Paddle p1;
    public TMP_Text p1ScoreTxt, p2ScoreTxt;
    public BreakMenu menu;

    private readonly float delay = 0.024f;
    private float lastReadTime = 0f;
    private Thread serialThread;
    ConcurrentQueue<string> _incoming = new ConcurrentQueue<string>();
    private bool _running = true;





    private void Start(){
        menu.NextRound();
        serialThread = new Thread(SerialThread);
        serialThread.Start();

    }


    private void Update(){
        while (_incoming.TryDequeue(out var receivedString))
        {
            ProcessData(receivedString);
        }

        //to test with keyboard
        if(BreakMenu.paused){
                if(Input.GetKey(KeyCode.W)){
                    menu.cont.Select();
                } else if (Input.GetKey(KeyCode.S)){
                    menu.quit.Select();
                    Debug.Log("quit selected");
                }

                if(Input.GetKey(KeyCode.A)){
                    if (EventSystem.current.currentSelectedGameObject == menu.quit.gameObject){
                        menu.Quit();
                    } else {
                        menu.NextRound();
                    }
                }
        }    
    }
    private void SerialThread()
    {
        
        while (_running)
        {
            var line = SerialPortManager.inst.Read();
            if (!string.IsNullOrEmpty(line))
                _incoming.Enqueue(line);
            Thread.Sleep(10);  // throttle to 100 Hz
        }

    }

    private void OnDestroy()
    {
        // serialThread.Abort();
        _running = false;
        serialThread.Join();

    }

    private void ProcessData(string receivedString){
        string[] data = receivedString.Split(',');

        if(int.Parse(data[2]) == 0){
            Debug.Log("p1 move up");
            p1.MoveUp();
        } else if (int.Parse(data[3]) == 0){
            Debug.Log("p2 move down");
            p1.MoveDown();
        } else {
            p1.StopMoving();
        }

        if(int.Parse(data[0]) == 0){
            Debug.Log("p2 move up");
            p2.MoveUp();
        } else if (int.Parse(data[1]) == 0){
            Debug.Log("p2 move down");

            p2.MoveDown ();
        } else {
            p2.StopMoving();
        }

        if(BreakMenu.paused){
            if(int.Parse(data[4]) < 200){
                Debug.Log("continue selected");
                menu.cont.Select();
            } else if (int.Parse(data[4]) > 800){
                menu.quit.Select();
                Debug.Log("quit selected");
            }

            if(int.Parse(data[5]) == 0){
                Debug.Log("button pressed");
                if (EventSystem.current.currentSelectedGameObject == menu.quit.gameObject){
                    menu.Quit();
                } else {
                    menu.NextRound();
                }
            }
        }
    }

    public void Player1Scores(){
        _p1Score++;
        this.p1ScoreTxt.text = _p1Score.ToString();

        ResetRound();
    }

    public void Player2Scores(){
        _p2Score++;
        this.p2ScoreTxt.text = _p2Score.ToString();

        ResetRound();
    }

    private void ResetRound()
    {
        this.ball.ResetPosition();
        this.p1.ResetPosition();
        this.p2.ResetPosition();
        // this.ball.AddStartingForce();
        menu.Pause();

    }

    public void StartRound(){
        this.ball.AddStartingForce();
    }



}
