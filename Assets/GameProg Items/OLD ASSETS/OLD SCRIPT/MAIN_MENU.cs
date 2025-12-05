using UnityEngine;
using UnityEngine.SceneManagement;
public class MAIN_MENU : MonoBehaviour
{   
        public void Play()
    {
        SceneManager.LoadScene("GameScene(InGame)");
    }
}


