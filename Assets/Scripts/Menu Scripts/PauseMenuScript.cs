using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public Canvas PMenu;

    public Button ResumeBut;
    public Button RestartBut;
    public Button QuitRaceBut;

    // Use this for initialization
    void Start()
    {
        PMenu = PMenu.GetComponent<Canvas>();

        ResumeBut = ResumeBut.GetComponent<Button>();
        RestartBut = RestartBut.GetComponent<Button>();
        QuitRaceBut = QuitRaceBut.GetComponent<Button>();

        PMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PMenu.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void ResumePressed()
    {
        PMenu.enabled = false;
        Time.timeScale = 1;
    }

    public void RestartPressed1()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void RestartPressed2()
    {
        Application.LoadLevel(2);
        Time.timeScale = 1;
    }

    public void RestartPressed3()
    {
        Application.LoadLevel(3);
        Time.timeScale = 1;
    }

    public void QuitRacePressed()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }
}
