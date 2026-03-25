using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeSceneManager : MonoBehaviour
{
    [Header("Scene Transition")]
    public string nextSceneName; // scene to load when cube is touched by user

    [Header("Hover Settings")]
    public float hoverAmplitude = 0.1f; // how high the cube will hover
    public float hoverFrequency = 0.5f; // how fast the cube will hover

    [Header("Wobble Settings")]
    public float wobbleAmplitude = 0.1f; // how much the cube will wobble
    public float wobbleFrequency = 5f; // how fast the cube will wobble (increased default for a jiggle effect)

    private Vector3 initialPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position; // initial position of cube for hover
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Calculate Hover (Up and Down on the Y axis)
        // We multiply Time by frequency for speed, and the whole Sine wave by amplitude for height.
        float hoverY = initialPosition.y + (Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude);

        // 2. Calculate Wobble (Side to side on X and Z axes)
        // We use Sine for X and Cosine for Z so they don't move in a perfectly straight diagonal line.
        float wobbleX = Mathf.Sin(Time.time * wobbleFrequency) * wobbleAmplitude;
        float wobbleZ = Mathf.Cos(Time.time * wobbleFrequency * 1.1f) * wobbleAmplitude;

        // 3. Apply the new calculated position to the cube
        transform.position = new Vector3(initialPosition.x + wobbleX, hoverY, initialPosition.z + wobbleZ);
    }

    // Triggered when another collider enters this object's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the cube is the player
        if (other.CompareTag("Player"))
        {
            // Ensure the scene name isn't left blank in the Inspector
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Next scene name is missing! Please enter it in the Inspector.");
            }
        }
    }
}