using UnityEngine;
using System.Collections;

public enum GameState
{
    menu, inGame, gameOver
}

public class GameManager : MonoBehaviour {


    public static GameManager instance; // use singleton for GameManager
    public GameState currentGameState = GameState.menu;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetButtonDown("s"))
            StartGame();
    }

    // вызывается для начала игры
    public void StartGame () {
        SetGameState(GameState.inGame);
        PlayerController.instance.StartGame();
	}
	
	// вызывается при смерти персонажа
	public void GameOver () {
        SetGameState(GameState.gameOver);
	}

    // вызывается для возврата в меню
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {

        }
        else if(newGameState == GameState.inGame)
        {

        }
        else if(newGameState == GameState.gameOver)
        {

        }

        currentGameState = newGameState;
    }
}
