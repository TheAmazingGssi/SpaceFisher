using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject FishObject; //TODO: add depth scriptable object to decide random spawn based on depth
    private void OnValidate()
    {
        cam = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
