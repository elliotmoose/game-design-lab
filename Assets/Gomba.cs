using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gomba : MonoBehaviour
{
    bool isMovingLeft = true;
    public float moveDuration = 2;
    public float speed = 3;
    private float curMoveTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMoveDirection());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(isMovingLeft ? -1 : 1,0,0) * Time.deltaTime * speed;
    }

    IEnumerator ChangeMoveDirection() {
        yield return new WaitForSeconds(moveDuration);
        isMovingLeft = !isMovingLeft;
        StartCoroutine(ChangeMoveDirection());
    }
}
