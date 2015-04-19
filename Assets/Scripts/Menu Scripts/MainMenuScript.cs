using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Canvas MMenuCanvas;
    public Canvas CharMenuCanvas;
    public Canvas MapMenuCanvas;

    public Button StartBut;
    public Button ControlsBut;
    public Button OptionsBut;
    public Button ExitBut;

	// Use this for initialization
	void Start () 
    {
        MMenuCanvas = MMenuCanvas.GetComponent<Canvas>();
        CharMenuCanvas = CharMenuCanvas.GetComponent<Canvas>();
        MapMenuCanvas = MapMenuCanvas.GetComponent<Canvas>();
        
        StartBut = StartBut.GetComponent<Button> ();
        ControlsBut = ControlsBut.GetComponent<Button>();
        OptionsBut = OptionsBut.GetComponent<Button>();
        ExitBut = ExitBut.GetComponent<Button>();

        MMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = false;
    

    }
	
    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void ControlsPressed()
    {
        Application.LoadLevel(4);
    }
	
    public void StartGamePressed()
    {
        MMenuCanvas.enabled = false;
        CharMenuCanvas.enabled = true;
        MapMenuCanvas.enabled = false;
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMenuCanvas.enabled = true;
            CharMenuCanvas.enabled = false;
            MapMenuCanvas.enabled = false;
        }
	
	}

}
