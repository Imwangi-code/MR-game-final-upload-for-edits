using UnityEngine;
using TMPro;

public class MRButtonTrigger : MonoBehaviour
{
    [Header("Assign your TMP objects here")]
    public GameObject textToHide;
    public GameObject textToShow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            PerformSwap();
        }
    }

    void PerformSwap()
    {
        if (textToHide != null) textToHide.SetActive(false);
        if (textToShow != null) textToShow.SetActive(true);
    }
}
