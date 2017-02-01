using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour
{

    public Text coinsLabel;
    public Text highScore;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.gameOver)
        {
            coinsLabel.text = GameManager.instance.collectedCoints.ToString();
            
            highScore.text = PlayerPrefs.GetFloat("highscore", 0).ToString("f0");
        }
    }
}
