using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        
        float music = PlayerPrefs.GetFloat("MUSIC", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFX", 0.75f);


        audioMixer.SetFloat("MUSIC", Mathf.Log10(music) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfx) * 20);

        
        int resIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);
        if (resIndex >= 0)
        {
            Resolution[] resolutions = Screen.resolutions;
            if (resIndex < resolutions.Length)
            {
                Resolution res = resolutions[resIndex];
                Screen.SetResolution(res.width, res.height, Screen.fullScreen);
            }
        }
    }
}