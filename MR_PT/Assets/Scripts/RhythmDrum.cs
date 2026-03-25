using UnityEngine;

public class RhythmDrum : MonoBehaviour
{
    public Conductor conductor;
    public int beatInterval = 2; 
    public float glowAnticipation = 0.5f; 

    [Header("Colors")]
    public Color normalGlowColor = Color.cyan;
    public Color hitFlashColor = Color.yellow;
    public float flashDuration = 0.2f; // How long the yellow stays

    private Material mat;
    private float flashTimer; 
    private static readonly int EmissionColorProp = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
    }

    // This is the function the HitDetection script will call
    public void TriggerFlash()
    {
        flashTimer = flashDuration;
    }

    void Update()
    {
        if (conductor == null) return;

        // If we are currently flashing yellow, prioritize that
        if (flashTimer > 0)
        {
            mat.SetColor(EmissionColorProp, hitFlashColor * 10f); // High intensity
            flashTimer -= Time.deltaTime;
            return; // Skip the regular blue glow logic while flashing
        }

        // Regular anticipation logic (Blue Glow)
        float nextTargetBeat = Mathf.Ceil(conductor.songPositionInBeats / beatInterval) * beatInterval;
        float distanceToBeat = nextTargetBeat - conductor.songPositionInBeats;

        if (distanceToBeat <= glowAnticipation && distanceToBeat > 0)
        {
            float intensity = 1f - (distanceToBeat / glowAnticipation);
            mat.SetColor(EmissionColorProp, normalGlowColor * intensity * 5f);
        }
        else
        {
            // Smoothly fade to black
            Color currentColor = mat.GetColor(EmissionColorProp);
            mat.SetColor(EmissionColorProp, Color.Lerp(currentColor, Color.black, Time.deltaTime * 5f));
        }
    }
}