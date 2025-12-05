using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickSound : MonoBehaviour
{
    public static ClickSound Instance;

    public AudioSource clickAudio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddClickListenersToAllButtons();
    }

    public void AddClickListenersToAllButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button btn in buttons)
        {
            btn.onClick.RemoveListener(PlayClickSound);
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound()
    {
        if (clickAudio != null && clickAudio.clip != null)
        {
            clickAudio.Play();
        }
    }
}