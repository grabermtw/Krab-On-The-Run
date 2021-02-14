using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int initialCoinAmount = 50;
    public float coinXBound = 50;
    public float coinZBound = 50;
    public float coinSpawnHeight = 30;
    public float coinSpawnPeriod = 5;
    public int laterCoinSpawnAmount = 3;
    public GameObject coinPrefab;
    public TextMeshProUGUI scoreText;
    public ScoreRecorder scoreRecorder;
    private int score;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RainMoney());
        Cursor.visible = false;
    }

    private IEnumerator RainMoney()
    {
        for (int i = 0; i < initialCoinAmount; i++)
        {
            Instantiate(coinPrefab, new Vector3(Random.Range(-coinXBound, coinXBound), coinSpawnHeight, Random.Range(-coinZBound, coinZBound)), Quaternion.identity);
        }

        while(true)
        {
            yield return new WaitForSeconds(coinSpawnPeriod);
            for (int i = 0; i < laterCoinSpawnAmount; i++)
            {
                Instantiate(coinPrefab, new Vector3(Random.Range(-coinXBound, coinXBound), coinSpawnHeight, Random.Range(-coinZBound, coinZBound)), Quaternion.identity);
            }
            yield return null;
        }
    }

    public void EndGame()
    {
        scoreRecorder.SetScore(score);
        Cursor.visible = true;
        GetComponent<SceneChanger>().ChangeScene(2);
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "" + score + " Coins Collected";
    }
}
