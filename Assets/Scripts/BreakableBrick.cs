using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    public GameObject debrisPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")){
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            }

            GameObject.Destroy(this.transform.parent.gameObject);
    	}
    }
}