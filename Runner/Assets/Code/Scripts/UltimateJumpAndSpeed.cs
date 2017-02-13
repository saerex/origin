using UnityEngine;
using System.Collections;

public class UltimateJumpAndSpeed : MonoBehaviour {


    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        

        if (PlayerController.instance.timeLeftForJump > 0)
            PlayerController.instance.timeLeftForJump -= Time.deltaTime;

        if (PlayerController.instance.timeLeftForSpeed > 0)
            PlayerController.instance.timeLeftForSpeed -= Time.deltaTime;

        if (PlayerController.instance.timeLeftForJump < 0)
        {
            PlayerController.instance.jumpForce = 25f;
        }
        if (PlayerController.instance.timeLeftForSpeed < 0)
        {
            PlayerController.instance.runImpulse = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(this.gameObject.name == "SpeedSphere")
            {
                
                PlayerController.instance.runImpulse = 3f;
                Destroy(this.gameObject);
                PlayerController.instance.timeLeftForSpeed = 10.0f;
            }
            else if(this.gameObject.name == "JumpSphere")
            {
                PlayerController.instance.jumpForce = 35f;
                Destroy(this.gameObject);
                PlayerController.instance.timeLeftForJump = 10.0f;
            }
        }
    }
}
