using UnityEngine;
using System.Collections;

public class KillPlayerTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayerController.instance.Kill();
    }
}
