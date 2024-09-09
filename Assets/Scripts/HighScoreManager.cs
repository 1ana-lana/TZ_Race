using UnityEngine;

public class HighScoreManager
{
    public int Load()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        return bestScore;
    }

    public void Save (int score)
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (bestScore >= score)
        {
            return;
        }
        PlayerPrefs.SetInt("BestScore", score);
        PlayerPrefs.Save();
    }
}