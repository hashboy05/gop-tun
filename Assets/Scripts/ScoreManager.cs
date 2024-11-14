using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highscoreText;
    public static int score = 0;
    public static int highscore = 0;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerPrefs.SetInt("highscore", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHEST SCORE: " + highscore.ToString();
    }

    void Update(){
        highscoreText.text = "HIGHEST SCORE: " + highscore.ToString();
    }

    public void AddPoint(int point)
    {
        score += point;
        scoreText.text = "SCORE: " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
    public void DeductPoint(int point)
    {
        if (score >= point)
        {
            score -= point;
            scoreText.text = "SCORE: " + score.ToString();
        }
        else
        {
            score = 0;
            scoreText.text = "SCORE: " + score.ToString();
        }
    }
    public void restartGame()
    {
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
    }
}
