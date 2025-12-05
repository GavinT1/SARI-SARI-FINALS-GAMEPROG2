using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SplashScreenManager : MonoBehaviour
{
    public TextMeshProUGUI splashText;
    public TextMeshProUGUI creatorText; 
    public string nextSceneName = "SampleScene";

    void Start()
    {
      
        splashText.transform.localScale = Vector3.one;
        creatorText.transform.localScale = Vector3.one;
        creatorText.gameObject.SetActive(false);

       
        LeanTween.scale(splashText.gameObject, new Vector3(7f, 7f, 7f), 3.5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(ShowCreatorText);

        
        Invoke("LoadNextScene", 10f); 
    }

    void ShowCreatorText()
    {
        
        splashText.gameObject.SetActive(false);

        
        creatorText.gameObject.SetActive(true);
        LeanTween.scale(creatorText.gameObject, new Vector3(7f, 7f, 7f), 3.5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(LoadNextScene);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
