using UnityEngine;

public class TouchDebugger : MonoBehaviour
{
    public GameObject textToHide;
    public GameObject textToShow;
    private MeshRenderer mesh;

    void Start() {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. VISUAL FEEDBACK: Change to Yellow for ANY collision
        // If it doesn't turn yellow, your Colliders or Rigidbodies are broken.
        mesh.material.color = Color.yellow;

        // 2. LOG THE NAME: This helps if you're using the World-Space Console we made
        Debug.Log("Hit by: " + other.name + " | Tag: " + other.tag);

        // 3. CHECK THE TAG: Change to Green only for your hand
        if (other.CompareTag("PlayerHand"))
        {
            mesh.material.color = Color.green;
            PerformSwap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset to original color (White) when hand leaves
        mesh.material.color = Color.white;
    }

    void PerformSwap()
    {
        if (textToHide != null) textToHide.SetActive(false);
        if (textToShow != null) textToShow.SetActive(true);
    }
}
