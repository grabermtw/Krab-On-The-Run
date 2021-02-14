using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI yourScoreText;
    public InputField nameInput;
    private int score;
    private HighScores highScoreList;

    // Start is called before the first frame update
    void Start()
    {
        highScoreList = GetComponent<HighScores>();
        GameObject scoreRecorder = GameObject.Find("ScoreRecorder");
        score = scoreRecorder.GetComponent<ScoreRecorder>().GetScore();
        Destroy(scoreRecorder);
        
        yourScoreText.text = "Your profit: $" + score;
        highScoreText.text = GetComponent<HighScores>().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NameInputFinish()
    {
        string name = nameInput.GetComponent<InputField>().text;
        Debug.Log(name);
        highScoreList.AddScore(score, name);
        highScoreList.AddScore(0, "");
        highScoreList.AddScore(0, "");
        GetComponent<SceneChanger>().ChangeScene(0);
    }
}
