using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public Transform mario;
    public Transform mainCamera;
    public Renderer[] layers;
    public float[] speedMultiplier;
    private float previousXPositionMario;
    private float previousXPositionCamera;
    private float[] currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = new float[layers.Length];
        for(int i=0; i<layers.Length; i++) {
            currentOffset[i] = 0;
        }

        previousXPositionMario = mario.position.x;
        previousXPositionCamera = mainCamera.position.x;        
    }

    void Update()
    {
        if(Mathf.Abs(previousXPositionCamera - mainCamera.transform.position.x) > 0.001f) {
            for(int i=0; i<layers.Length; i++) {
                if(currentOffset[i] > 1 || currentOffset[i] < -1) {
                    currentOffset[i] = 0;           
                }                

                float newOffset = mario.position.x - previousXPositionMario; 
                currentOffset[i] = currentOffset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(currentOffset[i], 0);
            }
        }

        previousXPositionCamera = mainCamera.position.x;
        previousXPositionMario = mario.position.x;
    }
}
