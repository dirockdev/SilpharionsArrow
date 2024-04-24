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

        GameObject spawnGameObject = ObjectPools[obj].FirstOrDefault(go => go != null && !go.activeSelf);


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
                if (obj != null)
                {
                    obj.SetActive(false);
                    return;
                }
                else
                {
                    Debug.LogWarning("Trying to return a null object to the pool.");
                    return;
                }
            }
        }
    }
    public static void ReturnToPool(float timeAlive, GameObject gameObject)
    {
        if(_instance!=null)_instance.StartCoroutine(ReturnToPoolRoutine(timeAlive, gameObject));
    }

    private static ObjectPoolManager _instance;
    
    private void Awake()
    {
        // Verifica si ya existe una instancia
        if (_instance == null)
        {
            // Si no existe, establece esta instancia como la instancia única y no la destruyas al cargar una nueva escena
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye este objeto para asegurarse de que solo haya una instancia en todo momento
            Destroy(gameObject);
        }
        ClearObjectPools();
    }
    private void OnDisable()
    {
        ClearObjectPools();
    }
    private static IEnumerator ReturnToPoolRoutine(float timeAlive, GameObject gameObject)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeAlive)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ReturnObjectToPool(gameObject);
    }
    public static void ClearObjectPools()
    {
        Destroy(_instance);
    }
}

public class PooledObjectInfo
{
    public string lookUpString;
    public List<GameObject>InactiveObjects=new List<GameObject>();
}