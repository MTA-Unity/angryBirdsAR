using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public void SaveHighScore(int newScore)
    {
        // Fetch existing high scores
        int highScore1 = PlayerPrefs.GetInt("HighScore1", 0);
        int highScore2 = PlayerPrefs.GetInt("HighScore2", 0);
        int highScore3 = PlayerPrefs.GetInt("HighScore3", 0);

        // Check and update high scores
        if (newScore > highScore1)
        {
            PlayerPrefs.SetInt("HighScore3", highScore2);
            PlayerPrefs.SetInt("HighScore2", highScore1);
            PlayerPrefs.SetInt("HighScore1", newScore);
        }
        else if (newScore > highScore2)
        {
            PlayerPrefs.SetInt("HighScore3", highScore2); // Shift the third high score down
            PlayerPrefs.SetInt("HighScore2", newScore);   // Set the new second high score
        }
        else if (newScore > highScore3)
        {
            PlayerPrefs.SetInt("HighScore3", newScore);
        }

        PlayerPrefs.Save();
    }


    public int[] GetHighScores()
    {
        int[] highScores = new int[3];
        highScores[0] = PlayerPrefs.GetInt("HighScore1", 0);
        highScores[1] = PlayerPrefs.GetInt("HighScore2", 0);
        highScores[2] = PlayerPrefs.GetInt("HighScore3", 0);
        return highScores;
    }
}
