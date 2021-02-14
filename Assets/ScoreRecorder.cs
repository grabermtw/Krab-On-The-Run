using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    private int score;

    public void SetScore(int newscore)
    {
        DontDestroyOnLoad(this);
        score = newscore;
    }

    public int GetScore()
    {
        return score;
    }
}
