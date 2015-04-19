using UnityEngine;
using System.Collections;

public class CharSelSpawner : MonoBehaviour {
    public GameObject karto1;
    public GameObject karto2;
    public GameObject karto3;

    int savedPlayer = 0;

	// Use this for initialization
	void Start () 
    {
        savedPlayer = PlayerPrefs.GetInt("charSelected");
		CameraFollow camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow>();

        if (savedPlayer == 1)
        {
			camera.Target = karto1.transform;
            Debug.Log(savedPlayer);
            karto1.GetComponent<AIController>().enabled = false;
            karto1.GetComponent<KartController>().playerController = true;


            karto2.GetComponent<AIController>().enabled = true;
            karto2.GetComponent<KartController>().playerController = false;

            karto3.GetComponent<AIController>().enabled = true;
            karto3.GetComponent<KartController>().playerController = false;
        }

        else if(savedPlayer == 2)
        {
            Debug.Log(savedPlayer);
			camera.Target = karto2.transform;
            karto2.GetComponent<AIController>().enabled = false;
            karto2.GetComponent<KartController>().playerController = true;

            karto1.GetComponent<AIController>().enabled = true;
            karto1.GetComponent<KartController>().playerController = false;

            karto3.GetComponent<AIController>().enabled = true;
            karto3.GetComponent<KartController>().playerController = false;
        }

        else if(savedPlayer == 3)
        {
            Debug.Log(savedPlayer);
			camera.Target = karto3.transform;
            karto3.GetComponent<AIController>().enabled = false;
            karto3.GetComponent<KartController>().playerController = true;

            karto1.GetComponent<AIController>().enabled = true;
            karto1.GetComponent<KartController>().playerController = false;

            karto2.GetComponent<AIController>().enabled = true;
            karto2.GetComponent<KartController>().playerController = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
