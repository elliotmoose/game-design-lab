using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Constant gameConstants;
    float groundDistance = -2.0f;
    public void SpawnNewEnemy() {
        SpawnFromPooler(Mathf.RoundToInt(Random.Range(0, 2)) == 0 ? ObjectPoolType.gombaEnemy : ObjectPoolType.greenEnemy);
    }

    public void SpawnFromPooler(ObjectPoolType type) {
        GameObject item = ObjectPooler.Instance.GetPooledObject(type);

        if(item) {
            item.transform.localScale = Vector3.one;
            item.transform.position = new Vector3(Random.Range(gameConstants.spawnStart, gameConstants.spawnEnd), groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
            item.GetComponent<Gomba>().Reset();
        }
        else {
            Debug.Log("Pool does not have enough items");
        }
    }

    public void StartSpawn() {
        for(int i=0; i<2; i++)
            SpawnFromPooler(ObjectPoolType.gombaEnemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
        GameManager.OnIncreaseScore += SpawnNewEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        GameManager.OnIncreaseScore -= SpawnNewEnemy;
    }
}


