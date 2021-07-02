using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mushroom : MonoBehaviour, Consumable
{
    bool isRight = true; 
    bool collided = false;
    float speed = 5;
    Rigidbody2D rigidBody;

    public Powerup stats;
    public CustomPowerupEvent onCollected;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up*18, ForceMode2D.Impulse);        
    }

    
    void FixedUpdate()
    {
        if(collided) {
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
            this.collided = true;            
            StartCoroutine(CollectedAnimation());
            this.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.OnCollected();
            onCollected.Invoke(stats);
        }
    }
    
    public abstract void ConsumedBy(GameObject player);
    public abstract void OnCollected();

    IEnumerator CollectedAnimation(){

        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        int steps = 10;
        for(int i=0; i<steps; i++) {
            this.transform.localScale += Vector3.one * 4/(float)steps;
            renderer.color = new Color(color.r, color.g, color.b, 255*(1-i/(float)steps));
            yield return new WaitForEndOfFrame();
        }

        renderer.enabled = false;
    }

    void OnBecameInvisible() {
        // GameObject.Destroy(gameObject);
    }
}
