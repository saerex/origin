using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public float timeLeftForJump;
    public float timeLeftForSpeed;
    public float runImpulse;

    public float jumpForce = 25f;
    private bool jumpTwice;

    public LayerMask groundLayer;
    public Animator animator;
    public float runningSpeed = 2.5f;
    private float speedMultiplier = 1.05f;
    public float speedIncreaseMilestone = 50f;
    private float speedMilestoneCount = 0;

    public AudioSource jumpSource;
    public AudioSource deathSource;

    private Vector3 startPosition;
    private Vector3 positionOfPlayer;
    public bool bubbleDontStopPlayer;
    [HideInInspector]
    public Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        instance = this;
        startPosition = this.transform.position;
        bubbleDontStopPlayer = true;
    }

    
    public void StartGame () {
        
        GameManager.instance.collectedCoints = 0;
        speedIncreaseMilestone = 50f;
        speedMilestoneCount = 0;
        runningSpeed = 4.5f;
        LevelGenerator.instance.GenerateInitialPieces();
        animator.SetBool("isAlive", true);

        this.transform.position = new Vector3(0, -1, 0); 
        
	}
	
	// Update is called once per frame
	void Update () {
         
        //увелечение скорости движения игрока(усложнение игры),зависит от пройденного расстояния
        if(this.transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            runningSpeed *= speedMultiplier;
        }


        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown("w"))
                Jump();
            animator.SetBool("isGrounded", IsGrounded());
        }
	}

    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame && bubbleDontStopPlayer)
        {
            if (bubbleDontStopPlayer && rigidBody.velocity.x < runningSpeed)
                rigidBody.velocity = new Vector2(runningSpeed + runImpulse, rigidBody.velocity.y);
        }
    }

    public void Jump()
    {

        if (IsGrounded()) {

            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTwice = true;
        }
        if(!IsGrounded() && jumpTwice)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTwice = false;
        }
        animator.SetBool("isGrounded", IsGrounded());
        if(jumpTwice)
        jumpSource.Play();
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
            
        }
        else
            return false;
        
    }
    

    public void Kill()
    {
        deathSource.Play();
        animator.SetBool("isAlive", false);
        LevelGenerator.instance.RemoveAllPieces();
        LevelGenerator.instance.pieces.Clear();
        
        GameManager.instance.GameOver();
        
        //save new highscore
        if (PlayerPrefs.GetFloat("highscore", 0) < this.GetDistance())
        {
            PlayerPrefs.SetFloat("highscore", this.GetDistance());
        }

    }

  

    //метод вычисляет пройденное расстояние
    public float GetDistance()
    {
        float traveledDistance = Vector2.Distance(new Vector2(startPosition.x, 0),
                                                  new Vector2(this.transform.position.x, 0));
        return traveledDistance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bubble")
        {
            
                bubbleDontStopPlayer = false;
                
                this.transform.position = collision.transform.position;
                this.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bubble")
        {
           
            bubbleDontStopPlayer = true;
           
            this.rigidBody.constraints = RigidbodyConstraints2D.None;
            this.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
