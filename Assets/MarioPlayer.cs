using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPlayer : MonoBehaviour
{
    public float jumpSpeed = 20;
    int jumpCount = 1;

    KeyCode MOVE_LEFT = KeyCode.LeftArrow;
    KeyCode MOVE_RIGHT = KeyCode.RightArrow;
    KeyCode JUMP = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        if(Input.GetKeyDown(JUMP)) {
            Debug.Log(jumpCount);

            if(jumpCount == 1) {
                Debug.Log("Jump");
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
                jumpCount = 0;
            }
        }

        if(Input.GetKey(MOVE_LEFT)) {
            rigidBody2D.velocity = new Vector2(-5, rigidBody2D.velocity.y);
        }
        if(Input.GetKey(MOVE_RIGHT)) {
            rigidBody2D.velocity = new Vector2(5, rigidBody2D.velocity.y);
        }
        if(!Input.GetKey(MOVE_LEFT) && !Input.GetKey(MOVE_RIGHT)) {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }

       
    }

    void OnCollisionEnter2D (Collision2D hit)
    {
        if(hit.gameObject.name == "Floor")
        {
            jumpCount = 1;
        }
    }
}
