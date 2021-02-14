using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    ArrayList highScores = new ArrayList();

    public class GenericScoreComparer : IComparer<HighScoreEntry>
    {
        //Compares the two HighScoreEntries based on score
        public int Compare(HighScoreEntry x, HighScoreEntry y)
        {
            if (x.GetScore() > y.GetScore())
            {
                return 1;
            }
            else if (x.GetScore() < y.GetScore())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class ScoreComparer : IComparer
    {
        // Calls GenericScoreComparer to compare the two HighScoreEntries by score
        public int Compare(object x, object y)
        {
            return (new GenericScoreComparer()).Compare((HighScoreEntry)x, (HighScoreEntry)y);
        }
    }

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            highScores.Add(new HighScoreEntry(PlayerPrefs.GetInt("KrabInt" + i, 0),
                PlayerPrefs.GetString("KrabString" + i)));
        }

    }

    public void AddScore(int score, string name)
    {
        int i = 0;

        //New custom comparer created to allow for sorting by score
        ScoreComparer comparer = new ScoreComparer();
        highScores.Sort(comparer);
        Debug.Log("Adding score: " + score);

        if (score > ((HighScoreEntry)highScores[0]).GetScore())
        {
            Debug.Log(score + ">" + ((HighScoreEntry)highScores[0]).GetScore());


            //Accounts for instances of many 0's!
            while (score < ((HighScoreEntry)highScores[i]).GetScore() &&
                ((HighScoreEntry)highScores[i]).GetScore() == ((HighScoreEntry)highScores[i + 1]).GetScore())
            {
                i++;
            }
            highScores.Insert(i, new HighScoreEntry(score, name));
            Debug.Log("highScores at " + i + ": " + ((HighScoreEntry)highScores[i]).GetScore());

            //highScores.Reverse();
            // if (highScores.Count > 11)
            //{
            //  highScores.RemoveAt(0);
            //}
        }
        highScores.Reverse();
        Save();
    }

    public int GetHighScore()
    {
        return ((HighScoreEntry)highScores[0]).GetScore();
    }

    public override string ToString()
    {
        string highScoreString = "";
        for (int i = 0; i < 10; i++)
        {
            highScoreString += (i + 1) + ") $" + ((HighScoreEntry)highScores[i]).GetScore() + " "
                + ((HighScoreEntry)highScores[i]).GetName() + "\n";
        }
        return highScoreString;
    }

    public void Save()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("KrabInt" + i, ((HighScoreEntry)highScores[i]).GetScore());
            PlayerPrefs.SetString("KrabString" + i, ((HighScoreEntry)highScores[i]).GetName());
        }
    }
}
