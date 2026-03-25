using UnityEngine;

public class DrumHitDetection : MonoBehaviour
{
    [Header("References")]
    public Conductor conductor;
    private RhythmDrum glowScript; // Reference to the glow script

    [Header("Rhythm Settings")]
    public int beatInterval = 2;
    public float timingThreshold = 0.15f;

    private float lastHitBeat = -1f;

    [Header("Score Management")]
    public ScoreManager scoreManager;

    void Start()
    {
        // Automatically find the RhythmDrum script on this same object
        glowScript = GetComponent<RhythmDrum>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Changed tag check to "Player" to match your code
        if (other.CompareTag("Player")) 
        {
            float currentBeat = conductor.songPositionInBeats;
            float closestValidBeat = Mathf.Round(currentBeat / beatInterval) * beatInterval;
            float distanceFromBeat = Mathf.Abs(currentBeat - closestValidBeat);

            if (distanceFromBeat < timingThreshold && closestValidBeat != lastHitBeat)
            {
                lastHitBeat = closestValidBeat;
                HandleHit(true);
            }
            else if (distanceFromBeat < 0.5f && closestValidBeat != lastHitBeat) 
            {
                HandleHit(false);
            }
        }
    }

    private void HandleHit(bool isPerfect)
    {
        if (isPerfect)
    {
        Debug.Log("<color=green>PERFECT HIT!</color>");
        if (glowScript != null) glowScript.TriggerFlash();
        
        // Add 100 points and increase combo
        if (scoreManager != null) scoreManager.AddHit(100);
    }
    else
    {
        Debug.Log("<color=red>MISSED TIMING</color>");
        
        // Break the combo!
        if (scoreManager != null) scoreManager.ResetCombo();
    }
    }
}