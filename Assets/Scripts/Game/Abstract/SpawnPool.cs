using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPool<T, U> : MonoBehaviour 
    where U : class
    where T : class
{
    [Tooltip("Objects ammount in the pool")]
    [SerializeField] protected int poolCount;

    [Tooltip("BonusBase prefab")]
    [SerializeField] protected GameObject Prefab;

    [Tooltip("Bonuses settings List")]
    [SerializeField] protected T elementSettings;

    public static Dictionary<GameObject, U> ObjElementsDict;
    protected Queue<GameObject> queueGameObjects;

    // THIS SHOULD
    protected virtual void Awake() 
    {
        Debug.Log("lol");
        ObjElementsDict = new Dictionary<GameObject, U>();
        queueGameObjects = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            InstantiateObjects();
        }
    }

    protected void InstantiateObjects()
    {
        GameObject prefab = Instantiate(Prefab);
        prefab.transform.SetParent(transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        U script = prefab.GetComponent<U>();
        prefab.SetActive(false);
        ObjElementsDict.Add(prefab, script);
        queueGameObjects.Enqueue(prefab);
    }

    protected bool TryGetNextElementInTheQueue(out U element) 
    {
        if (queueGameObjects.Count > 0)
        {
            GameObject enemy = queueGameObjects.Dequeue();
            element = ObjElementsDict[enemy];
            enemy.SetActive(true);
            return true;
        }
        else 
        {
            element = null;
            return false;
        }
    }

    protected void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        queueGameObjects.Enqueue(obj);
    }

    protected void ReturnAll()
    {
        foreach (KeyValuePair<GameObject, U> keyValuePair in ObjElementsDict)
        {
            ReturnObj(keyValuePair.Key);
        }
    }
}
