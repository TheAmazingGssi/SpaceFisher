using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected int initialSize = 10;

    protected readonly Queue<T> pool = new Queue<T>();

    private Camera cam;

    private void Awake()
    {
        FillPool(initialSize);
        cam = Camera.main;
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

    public virtual void SetPosition(GameObject obj)
    {
        Vector2 spawnPos = cam.transform.position + cam.transform.forward;
        obj.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
    }

    public virtual void SetPosition(GameObject obj, Transform trans)
    {
        Vector2 spawnPos = trans.position + trans.forward;
        obj.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
    }
}
