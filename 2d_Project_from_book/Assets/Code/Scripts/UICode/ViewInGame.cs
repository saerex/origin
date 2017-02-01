using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour {

    public Text coinsLabel;
    public Text scoreLabel;
    public Text highScore;


    // Update is called once per frame
    void Update () {
	     if(GameManager.instance.currentGameState == GameState.inGame)
        {
            coinsLabel.text = GameManager.instance.collectedCoints.ToString();
            scoreLabel.text = PlayerController.instance.GetDistance().ToString("f0");
            highScore.text  = PlayerPrefs.GetFloat("highscore", 0).ToString("f0");
        }
	}
}
