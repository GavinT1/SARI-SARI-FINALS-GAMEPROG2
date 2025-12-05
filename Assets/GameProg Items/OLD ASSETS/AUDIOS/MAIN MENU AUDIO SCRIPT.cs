using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
       
        float savedMusic = PlayerPrefs.GetFloat("MUSIC", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFX", 0.75f);

        
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);

        
        if (musicSlider != null)
            musicSlider.value = savedMusic;

        if (sfxSlider != null)
            sfxSlider.value = savedSFX;
    }

    void Start()
    {
        
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        
        float dB = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MUSIC", dB);
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SFX", dB);
        PlayerPrefs.SetFloat("SFX", volume);
    }
}
