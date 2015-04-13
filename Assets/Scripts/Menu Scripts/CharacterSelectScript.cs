using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour {
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
        Char4But = Char4But.GetComponent<Button>();
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
    public void Player1Pressed()
    {
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }

    public void Player2Pressed()
    {
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }

    public void Player3Pressed()
    {
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }

   public void Player4Pressed()
    {
        MMenuCanvas.enabled = false;
        MapMenuCanvas.enabled = true;
        CharMenuCanvas.enabled = false;
    }
}
