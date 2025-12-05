using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public TMP_Dropdown ResolutionDrop;

    private Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDrop.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDrop.AddOptions(options);

        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        Debug.Log(" Loaded saved Resolution" + savedResolutionIndex);

        ResolutionDrop.value = savedResolutionIndex;
        ResolutionDrop.RefreshShownValue();

        SetResolution(savedResolutionIndex);

        ResolutionDrop.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Debug.Log(" Resolution Changed to: " + selectedResolution.width + " x " + selectedResolution.height);
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
        Debug.Log("saved Resolution Index: " + resolutionIndex);
    }
}
