﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Canvas MMenuCanvas;
    public Canvas CharMenuCanvas;
    public Canvas MapMenuCanvas;
    public Canvas ControlMenuCanvas;

    public Button StartBut;
    public Button ControlsBut;
    public Button OptionsBut;
    public Button AboutBut;
    public Button ExitBut;

    // Use this for initialization
    void Start()
    {
// Disable and enables the correct menus
        MMenuCanvas = MMenuCanvas.GetComponent<Canvas>();
        CharMenuCanvas = CharMenuCanvas.GetComponent<Canvas>();
        MapMenuCanvas = MapMenuCanvas.GetComponent<Canvas>();

        StartBut = StartBut.GetComponent<Button>();
        ControlsBut = ControlsBut.GetComponent<Button>();
        OptionsBut = OptionsBut.GetComponent<Button>();
        ExitBut = ExitBut.GetComponent<Button>();
        AboutBut = AboutBut.GetComponent<Button>();

        MMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = false;
        ControlMenuCanvas.enabled = false;

    }

 
 // If exit button is hit then quits
    public void ExitButtonPressed()
    {
        Application.Quit();
    }

  //Controls is pressed and brings up controls canvas
    public void ControlsPressed()
    {
        MMenuCanvas.enabled = false;
        CharMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = false;
        ControlMenuCanvas.enabled = true;
    }

// Goes to the character select  menu and disables all the other menus
    public void StartGamePressed()
    {
        MMenuCanvas.enabled = false;
        CharMenuCanvas.enabled = true;
        MapMenuCanvas.enabled = false;
        ControlMenuCanvas.enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMenuCanvas.enabled = true;
            CharMenuCanvas.enabled = false;
            MapMenuCanvas.enabled = false;
            ControlMenuCanvas.enabled = false;
        }

    }

}
