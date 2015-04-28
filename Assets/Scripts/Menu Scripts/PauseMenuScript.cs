using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public Canvas PMenu;

    public Button ResumeBut;
    public Button RestartBut;
    public Button QuitBut2;

	private float savedSFXVol;

    // Use this for initialization
    void Start()
    {
        PMenu = PMenu.GetComponent<Canvas>();

        ResumeBut = ResumeBut.GetComponent<Button>();
        RestartBut = RestartBut.GetComponent<Button>();
        QuitBut2 = QuitBut2.GetComponent<Button>();

        PMenu.enabled = false;
		savedSFXVol = PlayerPrefs.GetFloat("sfxvolume");
    }

    // Update is called once per frame
    void Update()
    {
// When escape is hit pull up the pause menu and pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PMenu.enabled = true;
            Time.timeScale = 0;
			PlayerPrefs.SetFloat("sfxvolume", 0.0f);
        }
    }

// Disable the pause menu and sets the timescale to 1 
    public void ResumePressed()
    {
        PMenu.enabled = false;
        Time.timeScale = 1;
		PlayerPrefs.SetFloat("sfxvolume", savedSFXVol);
    }

// Depending on what scene the correct scene is reloaded
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

// Loads the main menu and sets the timescale to 1  
    public void QuitPressed2()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }
}
