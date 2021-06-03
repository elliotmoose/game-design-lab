using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public SpriteRenderer spriteRenderer;

    public Sprite usedQuestionBox; 
    public GameObject consummablePrefab; 

    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && !hit) {
            hit = true;
            rigidBody.AddForce(Vector3.up * rigidBody.mass * 10, ForceMode2D.Impulse); //ensure move
            GameObject.Instantiate(consummablePrefab, this.transform.position + Vector3.up, Quaternion.identity);
            StartCoroutine(DisableHittable());
            // rigidBody.isKinematic = true;
            // springJoint.enabled = false;
        }
    }

    bool ObjectStopped() {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01f;
    }
    IEnumerator DisableHittable() {
        if(!ObjectStopped()) {
            yield return new WaitUntil(()=>ObjectStopped());
        }

        spriteRenderer.sprite = usedQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Static;

        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false;
    }
}
