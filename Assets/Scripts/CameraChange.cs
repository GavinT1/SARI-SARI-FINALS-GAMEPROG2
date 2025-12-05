using UnityEngine;
using UnityEngine.UI; 

public class CameraChange : MonoBehaviour
{
    public RectTransform storeLayoutPanel;

    public Vector2 centerViewPosition = new Vector2(0f, 0f);
    public Vector2 rightViewPosition = new Vector2(-1000f, 0f);

    void Start()
    {
        if (storeLayoutPanel != null)
        {
            storeLayoutPanel.anchoredPosition = centerViewPosition;
        }
    }

    void Update()
    {
        if (storeLayoutPanel == null) 
        {
            Debug.LogError("Store Layout Panel is not assigned in the Inspector!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            storeLayoutPanel.anchoredPosition = centerViewPosition;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            storeLayoutPanel.anchoredPosition = rightViewPosition;
        }
    }
}