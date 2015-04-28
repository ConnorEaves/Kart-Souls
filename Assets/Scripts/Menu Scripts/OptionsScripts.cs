using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsScripts : MonoBehaviour {
    float savedMusicVol;
    float savedSFXVol;

    public Slider MusicSlider;
    public Slider SFXSlider;

    public Text MusicText;
    public Text SFXText;

    public void Update()
    {
        // Make the text equal the value of the slider (0 - 1)
        MusicText.text = MusicSlider.value.ToString();
        SFXText.text = SFXSlider.value.ToString();
        
        // Set the value of the slider to a variable
        savedMusicVol = MusicSlider.value;
        savedSFXVol = SFXSlider.value;
       
        // Set the player pref equal to the value of the variable
        PlayerPrefs.SetFloat("musicvolume", (savedMusicVol));
        PlayerPrefs.SetFloat("sfxvolume", (savedSFXVol));

    }
}
