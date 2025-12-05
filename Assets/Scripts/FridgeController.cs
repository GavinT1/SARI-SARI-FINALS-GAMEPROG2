using UnityEngine;
using UnityEngine.EventSystems; 

public class FridgeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator doorAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (doorAnimator == null)
        {
            Debug.LogError("Door Animator is not assigned");
            return;
        }
        
        doorAnimator.SetBool("IsHovering", true); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (doorAnimator == null)
        {
            return;
        }

        doorAnimator.SetBool("IsHovering", false); 
    }
}