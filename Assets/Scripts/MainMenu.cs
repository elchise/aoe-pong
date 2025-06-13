using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Data.Common;

public class MainMenu : MonoBehaviour
{
    protected string receivedString;
    public Button playBtn;
    private bool _acceptInput = false;

    private bool _prevBtnDown = false;
    private float delay = 1f;



    private void Start()
    {
        // flush any old serial data
        SerialPortManager.inst.FlushBuffer();

        // after delay, unlock
        StartCoroutine(EnableAfterDelay());
        
    }

    private IEnumerator EnableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        _acceptInput = true;
        playBtn.Select();

    }

    private void Play(){
        SceneManager.LoadScene(1);
    }
    
    private void FixedUpdate()
    {
        if (!_acceptInput)
            return;      // ignore everything until delay is done


        receivedString = SerialPortManager.inst.Read();
        string[] data = receivedString.Split(',');
        bool currDown = int.Parse(data[5]) == 0;

        if(currDown && !_prevBtnDown){

            this.Play();
        }

        _prevBtnDown = currDown;
    }
}
 