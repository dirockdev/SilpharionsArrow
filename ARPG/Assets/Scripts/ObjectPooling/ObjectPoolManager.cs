using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static Dictionary<GameObject, List<GameObject>> ObjectPools = new Dictionary<GameObject, List<GameObject>>();

    public static GameObject SpawnObject(GameObject obj, Vector3 spawnPos, Quaternion spawnRotation)
    {
        if (!ObjectPools.ContainsKey(obj))
        {
            ObjectPools[obj] = new List<GameObject>();
        }

        GameObject spawnGameObject = ObjectPools[obj].FirstOrDefault(go => !go.activeSelf);

        if (spawnGameObject == null)
        {
            spawnGameObject = Instantiate(obj, spawnPos, spawnRotation);
            ObjectPools[obj].Add(spawnGameObject);
        }
        else
        {
            spawnGameObject.transform.position = spawnPos;
            spawnGameObject.transform.rotation = spawnRotation;
            spawnGameObject.SetActive(true);
        }

        return spawnGameObject;
    }
    public static void ReturnObjectToPool(GameObject obj)
    {
        foreach (var pool in ObjectPools.Values)
        {
            if (pool.Contains(obj))
            {
                obj.SetActive(false);
                return;
            }
        }
    }
    

}

public class PooledObjectInfo
{
    public string lookUpString;
    public List<GameObject>InactiveObjects=new List<GameObject>();
}