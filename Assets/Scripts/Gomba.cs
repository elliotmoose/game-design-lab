using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Idle,
    Triggered
}

public class Gomba : MonoBehaviour
{   
    public ParticleSystem explosionParticle;
    bool isMovingLeft = true;
    public float moveDuration = 2;
    public float speed = 3;
    private float curMoveTimer = 0;
    private float triggerDistance = 2;
    EnemyState state = EnemyState.Idle;

    float explosionRadius = 2;
    float explodeTimer = 0.7f;
    float maxFlickerTimer = 0.2f;
    float curFlickerTimer = 0.2f;
    bool exploded = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.Instance.gameOver);
        if(GameManager.Instance.gameOver) {return;}
        switch (state)
        {
            case EnemyState.Idle:
                curMoveTimer += Time.deltaTime;

                if(curMoveTimer > moveDuration) {
                    isMovingLeft = !isMovingLeft;
                    curMoveTimer = 0;
                }

                this.transform.position += new Vector3(isMovingLeft ? -1 : 1,0,0) * Time.deltaTime * speed;

                bool isInRangeOfPlayer = (Vector3.Distance(MarioPlayer.Instance.transform.position, this.transform.position) < triggerDistance);                
                if(isInRangeOfPlayer) {
                    curMoveTimer = 0;
                    this.state = EnemyState.Triggered;
                    this.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
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
                        GameObject.Destroy(this.gameObject);
                    }
                }
                break;
        }        
    }

    void OnCollisionEnter2D (Collision2D hit)
    {
        if(hit.gameObject.name == "Floor")
        {
            isMovingLeft = !isMovingLeft;   
        }
    }
}
