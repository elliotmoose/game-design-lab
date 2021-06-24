using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolType {
    gombaEnemy = 0,
    greenEnemy = 1,
}

[System.Serializable]
public class ObjectPoolItem
{
	public GameObject prefab;
	public int amount;
	public bool expandPool;
	public ObjectPoolType type;
}

public class ExistingPoolItem
{
	public GameObject gameObject;
	public ObjectPoolType type;

	public  ExistingPoolItem(GameObject gameObject, ObjectPoolType type){
		this.gameObject = gameObject;
		this.type = type;
	}
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<ObjectPoolItem> itemsToPool;
    public List<ExistingPoolItem> pooledObjects;

    void  Awake()
    {
        Instance = this ;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject poolObjectInstance = Instantiate(item.prefab);
                poolObjectInstance.SetActive(false);
                poolObjectInstance.transform.parent = this.transform;
                pooledObjects.Add(new ExistingPoolItem(poolObjectInstance, item.type));
            }
        }
    }

    public GameObject GetPooledObject(ObjectPoolType type) {
        foreach(ExistingPoolItem poolItem in pooledObjects) {
            if(!poolItem.gameObject.activeInHierarchy && poolItem.type == type) return poolItem.gameObject;
        }

        foreach(ObjectPoolItem item in itemsToPool) {
            if(item.type == type && item.expandPool) {
                GameObject newInstance = Instantiate(item.prefab);
                newInstance.SetActive(false);
                newInstance.transform.SetParent(this.transform);
                pooledObjects.Add(new ExistingPoolItem(newInstance, item.type));
                return newInstance;
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
