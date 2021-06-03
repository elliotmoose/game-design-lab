using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform leftBound;
    public Transform rightBound;
    public Transform bottomBound;
    private float viewportHalfWidth;
    private float viewportHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 bottomLeft =  Camera.main.ViewportToWorldPoint(new  Vector3(0, 0, 0));
        viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x);
        viewportHalfHeight  =  Mathf.Abs(bottomLeft.y  -  this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {   
        this.transform.position = new Vector3(Mathf.Clamp(player.position.x, leftBound.position.x + viewportHalfWidth, rightBound.position.x - viewportHalfWidth), Mathf.Clamp(player.position.y, bottomBound.position.y + viewportHalfHeight, Mathf.Infinity), this.transform.position.z);
    }
}
