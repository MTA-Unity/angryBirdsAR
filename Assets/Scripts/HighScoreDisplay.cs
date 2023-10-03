using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScore1Text;
    public TextMeshProUGUI highScore2Text;
    public TextMeshProUGUI highScore3Text;

    private HighScoreManager highScoreManager;

    private void Start()
    {
        highScoreManager = GetComponent<HighScoreManager>();
        UpdateHighScores();
    }

    public void UpdateHighScores()
    {
        int[] highScores = highScoreManager.GetHighScores();
        highScore1Text.text = "1. " + highScores[0].ToString();
        highScore2Text.text = "2. " + highScores[1].ToString();
        highScore3Text.text = "3. " + highScores[2].ToString();
    }
}

