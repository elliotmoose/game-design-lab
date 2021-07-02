using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState {
    Idle,
    Triggered,
    Celebrate
}

public class Gomba : MonoBehaviour
{   
    public ParticleSystem explosionParticle;
    public Constant gameConstants;
    bool isMovingLeft = true;
    public float speed = 3;
    private float curMoveTimer = 0;
    private float triggerDistance = 2;
    EnemyState state = EnemyState.Idle;

    float explosionRadius = 2;
    float explodeTimer = 0.7f;
    float maxFlickerTimer = 0.2f;
    float curFlickerTimer = 0.2f;
    bool exploded = false;
    bool alive = true;

    bool grounded = true;

    public UnityEvent onEnemyDeath;

    void Start()
    {
        GameManager.OnPlayerKilled += Celebrate;
        speed = gameConstants.enemyMoveSpeed;
    }

    void OnDestroy() {
        GameManager.OnPlayerKilled -= Celebrate;
    }

    void Celebrate() {
        this.state = EnemyState.Celebrate;
    }

    public void Reset() {
        alive = true;
        exploded = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                curMoveTimer += Time.deltaTime;

                // if(curMoveTimer > moveDuration) {
                //     isMovingLeft = !isMovingLeft;
                //     curMoveTimer = 0;
                // }

                // this.transform.position += new Vector3(isMovingLeft ? -1 : 1,0,0) * Time.deltaTime * speed;                
                bool isInRangeOfPlayer = (Vector3.Distance(MarioPlayer.Instance.transform.position, this.transform.position) < triggerDistance);                
                if(isInRangeOfPlayer) {
                    curMoveTimer = 0;
                    // this.state = EnemyState.Triggered;
                    // this.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
                }

                break;

            case EnemyState.Triggered:
                explodeTimer -= Time.deltaTime;
                curFlickerTimer -= Time.deltaTime;
                
                if(curFlickerTimer < 0) {
                    curFlickerTimer = maxFlickerTimer;
                    if(this.GetComponent<SpriteRenderer>().color == new Color32(255,0,0,255)) {
                        this.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
                    }
                    else {
                       this.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
                    }
                }

                ParticleSystem.MainModule main = explosionParticle.main;
                if(explodeTimer < 0) {
                    if(!exploded) {
                        explosionParticle.gameObject.SetActive(true);
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        explosionParticle.Play();

                        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                        foreach(Collider2D hit in hits) {
                            if(hit.gameObject == MarioPlayer.Instance.gameObject) {
                                GameManager.Instance.GameOver();
                            }
                        }

                        exploded = true;
                    }

                    if(explodeTimer < (-1*main.duration/4)) {
                        // GameObject.Destroy(this.gameObject);
                        Debug.Log("explodeTimer");
                        this.gameObject.SetActive(false);
                        onEnemyDeath.Invoke();
                    }
                }
                break;
            case EnemyState.Celebrate:
                if(grounded) {
                    this.GetComponent<Rigidbody2D>().velocity = 4*Vector2.up;
                    grounded = false;
                }
                break;
        }        
    }

    void FixedUpdate() {
        if(GameManager.Instance.IsGameOver()) {return;}

        if(this.state == EnemyState.Idle) {
            this.GetComponent<Rigidbody2D>().MovePosition(this.transform.position + new Vector3(isMovingLeft ? -1 : 1,0,0) * Time.fixedDeltaTime * speed);
        }
    }

    void OnCollisionEnter2D (Collision2D hit)
    {
        if(hit.gameObject.name == "Tube")
        {
            isMovingLeft = !isMovingLeft;   
        }
        else if (hit.gameObject.CompareTag("Player")) {
            Vector3 playerPosition = hit.gameObject.transform.position;
            float horThreshold = 1f;
            float vertThreshold = 1f;
            if(Mathf.Abs(transform.position.x - playerPosition.x) < horThreshold && (playerPosition.y-transform.position.y) > vertThreshold) {
                if(alive) {
                    KillSelf();
                }
            }
            else {
                GameManager.Instance.GameOver();
            }
        }
        else if(hit.gameObject.name == "Floor") {
            grounded = true;
        }
    }

    public void KillSelf(){
		// enemy dies		
        alive = false;
		StartCoroutine(flatten());
        onEnemyDeath.Invoke();
	}

    IEnumerator flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper = 1.0f/(float) steps;

		for (int i = 0; i < steps; i ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);
			// make sure enemy is still above ground
            // gameConstants.groundSurface
			this.transform.position = new  Vector3(this.transform.position.x, -2 +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield  break;
	}
}
