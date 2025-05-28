using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameObject MainMenuUi;
    public GameObject inGameMenuUi;
    public GameObject PauseMenuUi;
    public GameObject GameOverMenuUi;

    public int delay = 1;

    public GameState CurrentState { get; private set; }

    // Voeg deze regel toe:
    public ObstacleGenerator obstacleGenerator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        StartCoroutine(TransitionToState(newState)); 
    }

    public void changeToMainMenu() => ChangeState(GameState.MainMenu);
    public void changeToPlaying() => ChangeState(GameState.Playing);
    public void changeToPause() => ChangeState(GameState.Pause);
    public void changeToGameOver() => ChangeState(GameState.GameOver);

    public void ResetGame()
    {
        // Reset score
        scoreManager.instance.ResetScore();

        // Reset coins
        coinManager.instance.ResetCoins();

        // Reset speler
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().ResetPlayer();
        }

        // Verwijder obstakels
        foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obj);
        }

        // Verwijder coins
        foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }

        // **Reset de obstacle generator spawn positie**
        if(obstacleGenerator != null)
        {
            obstacleGenerator.ResetGenerator();
        }

        // Start opnieuw met de Playing-state
        changeToPlaying();
    }

    private IEnumerator TransitionToState(GameState newState)
    {
        if (newState != GameState.MainMenu)
            yield return new WaitForSecondsRealtime(delay);

        CurrentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        HideAllMenu();

        switch (CurrentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                MainMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                inGameMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                PauseMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                GameOverMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
        }
    }

    private void HideAllMenu()
    {
        MainMenuUi.SetActive(false);
        inGameMenuUi.SetActive(false);
        PauseMenuUi.SetActive(false);
        GameOverMenuUi.SetActive(false);
    }
}
