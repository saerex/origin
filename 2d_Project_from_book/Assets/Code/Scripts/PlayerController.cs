using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float jumpForce = 6f;
    public LayerMask groundLayer;
    public Animator animator;
    public float runningSpeed = 1.5f;
    private Vector3 startPosition;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        instance = this;
        startPosition = this.transform.position;
    }

    
    public void StartGame () {
        this.transform.position = startPosition;
        animator.SetBool("isAlive", true);
	}
	
	// Update is called once per frame
	void Update () {
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

    private void Jump()
    {
        if(IsGrounded()) rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        GameManager.instance.GameOver();
        animator.SetBool("isAlive", false);
    }
}
