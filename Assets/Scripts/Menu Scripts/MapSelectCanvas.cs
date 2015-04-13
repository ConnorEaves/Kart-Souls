using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapSelectCanvas : MonoBehaviour {
    public Canvas MMenuCanvas;
    public Canvas CharMenuCanvas;
    public Canvas MapMenuCanvas;

    public Button Map1But;
    public Button Map2But;
    public Button Map3But;

	// Use this for initialization
	void Start () {
        MMenuCanvas = MMenuCanvas.GetComponent<Canvas>();
        CharMenuCanvas = CharMenuCanvas.GetComponent<Canvas>();
        MapMenuCanvas = MapMenuCanvas.GetComponent<Canvas>();
        
        Map1But = Map1But.GetComponent<Button>();
        Map2But = Map2But.GetComponent<Button>();
        Map3But = Map3But.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMenuCanvas.enabled = true;
            CharMenuCanvas.enabled = false;
            MapMenuCanvas.enabled = false;
        }
	}

    public void Map1Pressed()
    {
        Application.LoadLevel(1);
    }

    public void Map2Pressed()
    {
        Application.LoadLevel(1);
    }

    public void Map3Pressed()
    {
        Application.LoadLevel(1);
    }
}
