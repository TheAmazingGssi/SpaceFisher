using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    [SerializeField] GameObject fishPrefab;
    private List<GameObject> list = new List<GameObject>();


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
        //    fishObj = list[0];
        //    list.RemoveAt(0);
        //    fishObj.transform.position = position;
        //    fishObj.transform.rotation = rotation;
        //    fishObj.SetActive(true);
        //}
        fishObj.GetComponent<FishAI>().Setup(fishType, this);
        return fishObj;
    }

    public void Push(GameObject returningObject)
    {
        returningObject.SetActive(false);
        list.Add(returningObject);
    }
}
