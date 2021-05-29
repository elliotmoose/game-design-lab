using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPlayer : MonoBehaviour
{
    public static MarioPlayer Instance;
    public float jumpSpeed = 20;
    bool faceRight = true;
    int jumpCount = 1;

    KeyCode MOVE_LEFT = KeyCode.LeftArrow;
    KeyCode MOVE_RIGHT = KeyCode.RightArrow;
    KeyCode JUMP = KeyCode.Space;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameOver) {return;}
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        if(Input.GetKeyDown(JUMP)) {
            Debug.Log(jumpCount);

            if(jumpCount == 1) {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
                jumpCount = 0;
                animator.SetBool("IsJumping", true);
            }
        }

        if(Input.GetKey(MOVE_LEFT)) {
            rigidBody2D.velocity = new Vector2(-5, rigidBody2D.velocity.y);
            faceRight = false;
            animator.SetBool("IsRunning", true);
        }
        if(Input.GetKey(MOVE_RIGHT)) {
            rigidBody2D.velocity = new Vector2(5, rigidBody2D.velocity.y);
            faceRight = true;
            animator.SetBool("IsRunning", true);
        }
        if(!Input.GetKey(MOVE_LEFT) && !Input.GetKey(MOVE_RIGHT)) {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            animator.SetBool("IsRunning", false);
        }       

        GetComponent<SpriteRenderer>().flipX = !faceRight;
    }

    void OnCollisionEnter2D (Collision2D hit)
    {
        if(hit.gameObject.name == "Floor")
        {
            jumpCount = 1;
            animator.SetBool("IsJumping", false);
        }

        if(hit.gameObject.CompareTag("Enemy")) {
            Vector3 enemyPosition = hit.gameObject.transform.position;
            float horThreshold = 1;
            float vertThreshold = 0.5f;
            if(enemyPosition.x - transform.position.x < horThreshold && (transform.position.y-enemyPosition.y) > vertThreshold) {
                GameObject.Destroy(hit.gameObject);
                GameManager.Instance.score += 1;
            }
            else {
                GameManager.Instance.GameOver();
            }
        }

        if(hit.gameObject.CompareTag("End")) {
            GameManager.Instance.Win();
        }
    }
}
