using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPassThrough : Image
{
    // This function tells Unity to let clicks pass through this element
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // We still return true if we want hover events, but we must ignore clicks.
        // For your setup, we want the hover detection to work (true) but allow clicks to pass.
        // If the element is visible, IsRaycastLocationValid is usually enough.
        
        // Since you only need hover and not clicks, we will keep the Raycast Target ON
        // and add a script to handle the click pass-through.
        
        // For your simple case, try just overriding the validity.
        // WARNING: This may pass ALL events through, breaking the hover.
        
        // Let's stick to the simplest approach first: Unchecking the Raycast Target. 
        // If hover breaks, let me know, and we'll implement this script to ONLY ignore clicks.
        
        return true; // We keep it true for now.
    }
}