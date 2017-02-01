using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float jumpForce = 6f;
    public LayerMask groundLayer;
    public Animator animator;
    public float runningSpeed = 2.5f;
    private Vector3 startPosition;
    private Vector3 positionOfPlayer;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        instance = this;
        startPosition = this.transform.position;
    }

    
    public void StartGame () {
        GameManager.instance.collectedCoints = 0;
        runningSpeed = 2.5f;
        LevelGenerator.instance.GenerateInitialPieces();
        animator.SetBool("isAlive", true);
        this.transform.position = startPosition; 
        
	}
	
	// Update is called once per frame
	void Update () {

        //увелечение скорости движения игрока(усложнение игры),зависит от пройденного расстояния
        if (GetDistance() >= 25 && GetDistance() <= 50)
            runningSpeed = 2.8f;
        else if (GetDistance() >= 51 && GetDistance() <= 100)
            runningSpeed = 3.0f;
        else if (GetDistance() >= 101 && GetDistance() <= 200)
            runningSpeed = 3.5f;
        else if (GetDistance() >= 201 && GetDistance() <= 400)
            runningSpeed = 4.0f;
        else if (GetDistance() >= 401 && GetDistance() <= 600)
            runningSpeed = 4.5f;
        else if (GetDistance() >= 601 && GetDistance() <= 1000)
            runningSpeed = 4.7f;
        else if (GetDistance() >= 1001)
            runningSpeed = 5.0f;



        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown("w"))
                Jump();
            animator.SetBool("isGrounded", IsGrounded());
        }
	}

    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        if(IsGrounded()) rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("isGrounded", IsGrounded());
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
            return true;
        else
            return false;
    }
    public void Kill()
    {
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
}
