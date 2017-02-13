using UnityEngine;
using System.Collections;

public class CollectCoins : MonoBehaviour {

    private AudioSource coinSource;


    private void Awake()
    {
        coinSource = GameObject.Find("coinSound").GetComponent<AudioSource>();
    }

    //This will show the coin and activate its collider
    private void Show()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        
    }
    //This will hide the coin and deactivate its collider
    private void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }
    //This is called at the moment of collection of the coin
    private void Collect()
    {

        if (coinSource.isPlaying)
        {
            coinSource.Stop();
            coinSource.Play();
        }
        else
        coinSource.Play();
        Hide();
        GameManager.instance.CollectedCoin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }

   
}
