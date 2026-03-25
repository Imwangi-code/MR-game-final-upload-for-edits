using UnityEngine;
using TMPro; // Make sure you have TextMeshPro installed!

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    private int currentScore = 0;
    private int currentCombo = 0;
    private int maxCombo = 0;

    void Start()
    {
        UpdateUI();
    }

    public void AddHit(int points)
    {
        currentCombo++;
        // Bonus: Multiply points by combo (e.g., 100 points * combo level)
        currentScore += points * Mathf.Clamp(currentCombo / 5, 1, 4); 
        
        if (currentCombo > maxCombo) maxCombo = currentCombo;
        
        UpdateUI();
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore.ToString("N0");
        comboText.text = "Combo: " + currentCombo + "x";
        
        // Optional: Make the combo text "pop" when it changes
        comboText.transform.localScale = Vector3.one * 1.2f;
    }

    void Update()
    {
        // Smoothly scale the combo text back down for a nice effect
        comboText.transform.localScale = Vector3.Lerp(comboText.transform.localScale, Vector3.one, Time.deltaTime * 10f);
    }
}