using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "Constant", menuName =  "ScriptableObjects/Constant", order =  1)]
public class Constant : ScriptableObject
{
    public float spawnStart = 0;
    public float spawnEnd = 7;
    
    public float playerJumpSpeed = 30;
    public float playerMaxSpeed = 5;
    
    public float enemyMoveSpeed = 5;
}
