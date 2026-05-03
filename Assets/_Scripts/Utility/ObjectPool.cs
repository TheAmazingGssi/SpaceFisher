using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected int initialSize = 10;

    protected readonly Queue<T> pool = new Queue<T>();

    private void Awake()
    {
        FillPool(initialSize);
    }

    protected virtual void FillPool(int count)
    {
        if(!prefab) return; 

        for (int i = 0; i < count; i++)
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public virtual T Get()
    {
        if (pool.Count == 0)
            FillPool(1);

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }
    public virtual void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
