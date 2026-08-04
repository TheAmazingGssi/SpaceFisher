using UnityEngine;

public class StarterBuildings : MonoBehaviour
{
    private void Awake()
    {
        if(!RunManager.Instance.FirstRun) Destroy(gameObject);
    }
}
