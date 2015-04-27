using UnityEngine;
using System.Collections;

public class CharSelSpawner : MonoBehaviour {
    public GameObject karto1;
    public GameObject karto2;
    public GameObject karto3;

    int savedPlayer = 0;

	// Use this for initialization
	void Awake () 
    {
        savedPlayer = PlayerPrefs.GetInt("charSelected");
		CameraFollow camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>();

		//spedometer = GameObject.Find ("Needle Pivot").GetComponent<HUDSpedometer> ();
// Sets the camera and cart to the correct character based on PlayerPref
        if (savedPlayer == 1)
        {
			camera.Target = karto1.transform;
			//spedometer.PlayerKart = karto1;
            Debug.Log( "Saved Player " + savedPlayer);

            karto1.GetComponent<AIController>().enabled = false;
            karto1.GetComponent<KartController>().playerController = true;


            karto2.GetComponent<AIController>().enabled = true;
            karto2.GetComponent<KartController>().playerController = false;

            karto3.GetComponent<AIController>().enabled = true;
            karto3.GetComponent<KartController>().playerController = false;
        }

        else if(savedPlayer == 2)
        {
			Debug.Log( "Saved Player " + savedPlayer);
			//spedometer.PlayerKart = karto2;
			camera.Target = karto2.transform;

            karto1.GetComponent<AIController>().enabled = true;
            karto1.GetComponent<KartController>().playerController = false;

			karto2.GetComponent<AIController>().enabled = false;
			karto2.GetComponent<KartController>().playerController = true;

            karto3.GetComponent<AIController>().enabled = true;
            karto3.GetComponent<KartController>().playerController = false;
        }

        else if(savedPlayer == 3)
        {
			Debug.Log( "Saved Player " + savedPlayer);
			//spedometer.PlayerKart = karto3;
			camera.Target = karto3.transform;

            karto1.GetComponent<AIController>().enabled = true;
            karto1.GetComponent<KartController>().playerController = false;

            karto2.GetComponent<AIController>().enabled = true;
            karto2.GetComponent<KartController>().playerController = false;

			karto3.GetComponent<AIController>().enabled = false;
			karto3.GetComponent<KartController>().playerController = true;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
