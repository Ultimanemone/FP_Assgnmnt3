using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    /* 3. Main functions */
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    public static ObjPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab, transform);
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}