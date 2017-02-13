using UnityEngine;
using System.Collections;

public enum GameState
{
    menu, inGame, gameOver, pauseMenu
}

public class GameManager : MonoBehaviour {

    //разные типы канвасов для разных состояний игры,присваиваем в Unity
    public Canvas menuCanvas;      
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public Canvas pauseMenuCanvas;

    public int collectedCoints = 0; // собранные монетки
    public static GameManager instance; // use singleton for GameManager
    public GameState currentGameState = GameState.menu;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
    }

    private void Start()
    {
        SetGameState(GameState.menu);
    }
    // Метод увеличивает кол-во монеток на +1
    public void CollectedCoin()
    {
        collectedCoints++;
    }

    // вызывается для начала игры
    public void StartGame () {
        PlayerController.instance.Kill();
        PlayerController.instance.StartGame();
        SetGameState(GameState.inGame);
        Time.timeScale = 1f;

    }
	
	// вызывается при смерти персонажа
	public void GameOver () {
        
        Time.timeScale = 1f;
        SetGameState(GameState.gameOver);
	}

    // вызывается для возврата в меню
    public void BackToMenu()
    {
        
        SetGameState(GameState.menu);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.pauseMenu);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        SetGameState(GameState.inGame);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //setup unity scene for menu state
            menuCanvas.enabled      = true;
            inGameCanvas.enabled    = false;
            gameOverCanvas.enabled  = false;
            pauseMenuCanvas.enabled = false;
        }
        else if(newGameState == GameState.inGame)
        {
            //setup unity scene for InGame state
            menuCanvas.enabled      = false;
            inGameCanvas.enabled    = true;
            gameOverCanvas.enabled  = false;
            pauseMenuCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {
            //setup unity scene for GameOver state
            menuCanvas.enabled      = false;
            inGameCanvas.enabled    = false;
            gameOverCanvas.enabled  = true;
            pauseMenuCanvas.enabled = false;
        }
        else if (newGameState == GameState.pauseMenu)
        {
            //setup unity scene for Pause state
            menuCanvas.enabled      = false;
            inGameCanvas.enabled    = false;
            gameOverCanvas.enabled  = false;
            pauseMenuCanvas.enabled = true;
        }

        currentGameState = newGameState;
    }
}
