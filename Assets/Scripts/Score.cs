using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int numKilled;
    public Text numKilledText;

    public static float timeAlive;
    public Text timeAliveText;

    public int score;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        numKilledText.text = numKilled.ToString();
        timeAliveText.text = timeAlive.ToString();
        score = Mathf.RoundToInt(timeAlive * numKilled);
        scoreText.text = score.ToString();
    }
}
