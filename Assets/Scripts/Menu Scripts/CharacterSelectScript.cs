using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterSelectScript : MonoBehaviour {
    //Set the variable for the character to be selected
    int charSelected = 0;
    
    public Canvas MMenuCanvas;
    public Canvas CharMenuCanvas;
    public Canvas MapMenuCanvas;

    public Button Char1But;
    public Button Char2But;
    public Button Char3But;
    public Button Char4But;
   
	// Use this for initialization
	void Start () {
        MMenuCanvas = MMenuCanvas.GetComponent<Canvas>();
        CharMenuCanvas = CharMenuCanvas.GetComponent<Canvas>();
        MapMenuCanvas = MapMenuCanvas.GetComponent<Canvas>();

        Char1But= Char1But.GetComponent<Button>();
        Char2But = Char2But.GetComponent<Button>();
        Char3But = Char3But.GetComponent<Button>();
	}
	
	// Update is called once per frame
    void Update()
    {
        // Go to menu when Escape is hit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMenuCanvas.enabled = true;
            CharMenuCanvas.enabled = false;
            MapMenuCanvas.enabled = false;
        }
    }
   
    //  PlayerPref to hold which character is pressed
    // Depending on which one is picked, set the playerpref to that and disable the main and char menu and enable the map menu
    public void Player1Pressed()
    {
        charSelected = 1;
        PlayerPrefs.SetInt("charSelected", (charSelected));
        print(PlayerPrefs.GetInt("charSelected"));
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }

    public void Player2Pressed()
    {
        charSelected = 2;
        PlayerPrefs.SetInt("charSelected", (charSelected));
        print(PlayerPrefs.GetInt("charSelected"));
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }

    public void Player3Pressed()
    {
        charSelected = 3;
        PlayerPrefs.SetInt("charSelected", (charSelected));
        print(PlayerPrefs.GetInt("charSelected"));
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }
}
