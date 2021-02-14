using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreEntry {

    private int score;
    private string entryName;

    public HighScoreEntry(int score, string entryName)
    {
        this.score = score;
        this.entryName = entryName;
    }

    public int GetScore()
    {
        return score;
    }

    public string GetName()
    {
        return entryName;
    }

}
