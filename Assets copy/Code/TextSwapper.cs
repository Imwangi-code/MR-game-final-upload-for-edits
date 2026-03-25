using UnityEngine;
using TMPro;

public class TextSwapper : MonoBehaviour
{
    public GameObject textToHide;
    public GameObject textToShow;

    // This detects the physical touch of your hand's sphere collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the button is your hand
        if (other.CompareTag("PlayerHand"))
        {
            SwapText();
        }
    }

    public void SwapText()
    {
        if (textToHide != null) textToHide.SetActive(false);
        if (textToShow != null) textToShow.SetActive(true);
        
        // Optional: Disable the button so it can't be pressed twice
        // gameObject.SetActive(false); 
    }
}

