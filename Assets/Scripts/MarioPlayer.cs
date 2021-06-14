using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPlayer : MonoBehaviour
{
    public static MarioPlayer Instance;
    public float jumpSpeed = 30;
    bool faceRight = true;
    int jumpCount = 1;

    KeyCode MOVE_LEFT = KeyCode.LeftArrow;
    KeyCode MOVE_RIGHT = KeyCode.RightArrow;
    KeyCode JUMP = KeyCode.Space;

    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameOver) {return;}
        if(Input.GetKeyDown(JUMP)) {
            Debug.Log(jumpCount);

            if(jumpCount == 1) {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                jumpCount = 0;
                animator.SetBool("IsJumping", true);
            }
        }        
    
        if(Input.GetKey(MOVE_LEFT)) {
            if(faceRight && Mathf.Abs(rigidBody.velocity.x) > 0.5 && jumpCount == 1) {
                animator.SetTrigger("OnSkid");
            }

            // rigidBody.velocity = new Vector2(-5, rigidBody.velocity.y);
            faceRight = false;
        }

        if(Input.GetKey(MOVE_RIGHT)) {
            if(!faceRight && Mathf.Abs(rigidBody.velocity.x) > 0.5 && jumpCount == 1) {
                animator.SetTrigger("OnSkid");
            }
            // rigidBody.velocity = new Vector2(5, rigidBody.velocity.y);
            faceRight = true;
        }
        if(!Input.GetKey(MOVE_LEFT) && !Input.GetKey(MOVE_RIGHT)) {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }       

        animator.SetFloat("xSpeed", Mathf.Abs(rigidBody.velocity.x));
        GetComponent<SpriteRenderer>().flipX = !faceRight;
    }

    void FixedUpdate() {
        if(GameManager.Instance.gameOver) {return;}
        float speed = 200;
        float maxSpeed = 5;
        float moveHor = Input.GetAxis("Horizontal");
        
        if(Mathf.Abs(moveHor) > 0) {
            bool isSameDirection = (Mathf.Sign(rigidBody.velocity.x) == Mathf.Sign(moveHor));
            if(!isSameDirection || rigidBody.velocity.magnitude < maxSpeed) {
                rigidBody.AddForce(new Vector2(moveHor, 0) * speed);
            }
        }
    }

    void OnCollisionEnter2D (Collision2D hit)
    {
        if((hit.gameObject.CompareTag("Ground") || hit.gameObject.CompareTag("Tube")) && Mathf.Abs(rigidBody.velocity.y) < 0.01f  && jumpCount != 1)
        {
            jumpCount = 1;
            animator.SetBool("IsJumping", false);
            transform.Find("dustCloud").GetComponent<ParticleSystem>().Play();
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

    public void PlayJumpSound() {        
        audioSource.PlayOneShot(audioSource.clip);
    }
}
