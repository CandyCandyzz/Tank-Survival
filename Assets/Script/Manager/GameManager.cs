using System.Collections;
using UnityEngine;

public enum GameState
{
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState state;
    private CharacterStat playerStat;

    //UI
    private GameObject gameOverUI;
    private float timeGameOver = 3;

    //Score
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        ChangeState(GameState.Playing);
        playerStat = GameObject.FindWithTag("Player").GetComponent<CharacterStat>();

        gameOverUI = GameObject.Find("GameOverPanel");

        AddEvent();
    }

   

    public void PlayGame()
    {
        ChangeState(GameState.Playing);
    }


    #region GameOver
    private void AddEvent()
    {
        playerStat.onDie.AddListener(GameOver);
    }
    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(timeGameOver);
        gameOverUI.GetComponent<OnOffObj>().TurnOn();
        ChangeState(GameState.GameOver);
        scoreManager.SaveScore();
    }
    #endregion


    private void ChangeState(GameState newState)
    {
        state = newState;
        switch(state)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
}
