using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

   
    public GameObject[] pooledObject;
    public int pooledAmount = 5;
    public bool WillGrow = true;
    List<GameObject>PooledObjects;
    int rand;
   
	void Start () {

        PooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            for (int j = 0; j < pooledObject.Length; j++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject[j]);
                obj.SetActive(false);
                PooledObjects.Add(obj);
            }
          
        }
	}

    public GameObject GetPooledObject()
    {
        rand = Random.Range(0, pooledObject.Length);
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[rand*i];
            }
            if (WillGrow)
            {
                for (int j = 0; j < pooledObject.Length; j++)
                {
                    GameObject obj = (GameObject)Instantiate(pooledObject[j]);
                    PooledObjects.Add(obj);
                    return obj;
                }
            }
         
        }
        return null;
    }

}
