using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    bool isRight = true; 
    bool stop = false;
    float speed = 5;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up*18, ForceMode2D.Impulse);        
    }

    
    void FixedUpdate()
    {
        if(stop) {
            rigidBody.velocity = Vector2.zero;
        }
        else {
            rigidBody.velocity = new Vector2((isRight ? 1 : -1) * speed, rigidBody.velocity.y);
        }
        // rigidBody.AddForce(Vector2.down * 10 * 9.81f, ForceMode2D.Force);
        // Vector2 nextPos = rigidBody.position + speed * (isRight ? Vector2.right : Vector2.left) * Time.fixedDeltaTime;
        // rigidBody.MovePosition(nextPos);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Tube")) {
            Debug.Log("Change direction!");
            this.isRight = !this.isRight;
        }

        if(col.gameObject.CompareTag("Player")) {
            this.stop = true;
        }
    }
    

    void OnBecameInvisible() {
        GameObject.Destroy(gameObject);
    }
}
