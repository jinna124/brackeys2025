using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject poolItem;
    [SerializeField] Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] int poolSize = 20;
    
    private GameObject go;
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            go = Instantiate(poolItem);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetObject()
    {
        go = pool.Dequeue();
        go.SetActive(enabled);
        return go;
    }

    public void ReturnObject(GameObject gameObject)
    {
        pool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
