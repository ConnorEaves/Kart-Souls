using UnityEngine;
using System.Collections;

public class MusicVolumeForTrack : MonoBehaviour {

    public AudioSource trackmusic;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
    {
        // Get the playerpref value set for the music from the slider and assign it as the volume
        trackmusic.volume = PlayerPrefs.GetFloat("musicvolume");
	}
}
