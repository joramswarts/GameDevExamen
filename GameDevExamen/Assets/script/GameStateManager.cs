using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hier geven we de mogelijke spelstatussen aan
public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    // Singleton om overal makkelijk bij deze class te kunnen
    public static GameStateManager instance;

    // UI-elementen voor de verschillende menu’s
    public GameObject MainMenuUi;
    public GameObject inGameMenuUi;
    public GameObject PauseMenuUi;
    public GameObject GameOverMenuUi;

    // Kleine vertraging bij het wisselen van toestand
    public int delay = 1;

    // De huidige status van het spel, alleen uitleesbaar
    public GameState CurrentState { get; private set; }

    // Verwijzing naar de obstacle generator, zodat we die kunnen resetten
    public ObstacleGenerator obstacleGenerator;

    private void Awake()
    {
        // Zorgt ervoor dat er maar één GameStateManager is
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Blijft bestaan tussen scenes
        }
        else
        {
            Destroy(gameObject); // Verwijder als er al één is
        }
    }

    private void Start()
    {
        // Begin altijd in het hoofdmenu
        ChangeState(GameState.MainMenu);
    }

    // Verandert de huidige gamestatus (asynchroon met vertraging)
    public void ChangeState(GameState newState)
    {
        StartCoroutine(TransitionToState(newState)); 
    }

    // Snelle methods om naar een bepaalde status te gaan
    public void changeToMainMenu() => ChangeState(GameState.MainMenu);
    public void changeToPlaying() => ChangeState(GameState.Playing);
    public void changeToPause() => ChangeState(GameState.Pause);
    public void changeToGameOver() => ChangeState(GameState.GameOver);

    // Zet het hele spel terug in de beginstaat
    public void ResetGame()
    {
        // Score resetten
        scoreManager.instance.ResetScore();

        // Coins resetten
        coinManager.instance.ResetCoins();

        // Speler resetten (bijv. positie/herstarten animatie)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().ResetPlayer();
        }

        // Obstakels weghalen uit de scene
        foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obj);
        }

        // Coins weghalen uit de scene
        foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }

        // Obstacle generator opnieuw instellen
        if(obstacleGenerator != null)
        {
            obstacleGenerator.ResetGenerator();
        }

        // Zet spel weer op "Playing"
        changeToPlaying();
    }

    // Zorgt voor de overgang naar de nieuwe gamestatus
    private IEnumerator TransitionToState(GameState newState)
    {
        // Als we niet naar het hoofdmenu gaan, wachten we even
        if (newState != GameState.MainMenu)
            yield return new WaitForSecondsRealtime(delay);

        CurrentState = newState;
        HandleStateChange(); // Voert aanpassingen uit voor nieuwe status
    }

    // Regelt welke UI en instellingen bij de huidige status horen
    private void HandleStateChange()
    {
        HideAllMenu(); // Eerst alles verbergen

        switch (CurrentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0; // Tijd stilstaan
                MainMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.Playing:
                Time.timeScale = 1; // Tijd weer laten lopen
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

    // Zet alle UI schermen uit
    private void HideAllMenu()
    {
        MainMenuUi.SetActive(false);
        inGameMenuUi.SetActive(false);
        PauseMenuUi.SetActive(false);
        GameOverMenuUi.SetActive(false);
    }
}
