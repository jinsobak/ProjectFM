using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public Dictionary<string, Queue<GameObject>> pool;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AddObject(string poolName, int count, GameObject objectPF)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(objectPF);
            EnqueueObject(poolName, obj);   
        }
    }

    public void EnqueueObject(string poolName, GameObject obj)
    {
        Debug.Log($"Enqueue object, name: {obj.name}");
        obj.transform.parent = null;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(false);
        pool[poolName].Enqueue(obj);
    }

    public GameObject GetObject(string poolName, Transform transform)
    {
        if (pool[poolName].Count == 0)
        {
            
        }

        return null;
    }
}
