using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    [SerializeField] GameObject fishPrefab;
    private Queue<GameObject> list = new Queue<GameObject>();


    public GameObject Pull(FishStats fishType, Vector3 position, Quaternion rotation)
    {
        GameObject fishObj;
        //if (list.Count == 0)
        //{
              fishObj = Instantiate(fishPrefab, position, rotation);
        //    //fishObj.SetActive(false);
        //}
        //else
        //{
        //    fishObj = list.Dequeue();
        //    fishObj.transform.position = position;
        //    fishObj.transform.rotation = rotation;
        //    fishObj.SetActive(true);
        //}
        fishObj.GetComponent<FishAI>().Setup(fishType, this);
        return fishObj;
    }

    public void Push(GameObject returningObject)
    {
        if(list.Contains(returningObject))
        {
            Debug.LogError("Tried to push a fish already in the pool");
            return;
        }
        returningObject.SetActive(false);
        list.Enqueue(returningObject);
    }
}
