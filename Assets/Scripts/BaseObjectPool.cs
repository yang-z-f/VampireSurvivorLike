using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolManager
{
    private static ObjectPoolManager objectPoolManager;
    public static ObjectPoolManager GetObjectPoolManager()
    {
        if (objectPoolManager == null)
        {
            objectPoolManager = new ObjectPoolManager();
        }

        return objectPoolManager;
    }
    private Dictionary<string, Stack<GameObject>> objectPoolDic = new Dictionary<string, Stack<GameObject>>();
    public GameObject CreateObject(string name)
    {

        GameObject obj;
        if (objectPoolDic.ContainsKey(name) && objectPoolDic[name].Count > 0)
        {
            obj = objectPoolDic[name].Pop();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }

        return obj;
    }

    public void AddObjectPool(GameObject gameObject)
    {
        if (!objectPoolDic.ContainsKey(gameObject.name))
            objectPoolDic.Add(gameObject.name, new Stack<GameObject>());
            objectPoolDic[gameObject.name].Push(gameObject);
    }
    public void ClearDic()
    {
        objectPoolDic.Clear();
    }
}