using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreakMenu : MonoBehaviour
{

    public static bool paused = false;

    public Canvas breakMenu;
    public GameManager manager;
    public Button cont, quit;

    private void Awake()
    {
        breakMenu = GetComponent<Canvas>();
        breakMenu.enabled = false;
    }

    public void NextRound(){
        paused = false;
        breakMenu.enabled = false;
        manager.StartRound();
    }
    
    public void Pause(){
        paused = true;
        breakMenu.enabled = true;
        cont.Select();
    }

    public void Quit(){
        SceneManager.LoadScene(0);
    }
}
