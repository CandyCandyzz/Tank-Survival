using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //HUD
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float timeDur; 
    private int score = 0;

    //GameOverUI
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textBest;

    public void AddScore(int scoreIncrease)
    {
        int startScore = score;
        int newScore = score + scoreIncrease;
        StartCoroutine(IncreaseAnimate(startScore,newScore));
    }

    private IEnumerator IncreaseAnimate(int startScore, int newScore)
    {
        float time = 0;
        while (time < timeDur)
        {
            time += Time.deltaTime;
            float t = time / timeDur;

            score = (int)Mathf.Lerp(startScore, newScore, t);
            scoreText.text = score.ToString();
            yield return null;
        }
    }

    public void SaveScore()
    {
        float oldScore = PlayerPrefs.GetFloat("Score");
        if (oldScore < score)
        {
            PlayerPrefs.SetFloat("Score", score);
        }

        textScore.text = score.ToString();
        textBest.text = PlayerPrefs.GetFloat("Score").ToString();
    }
}
