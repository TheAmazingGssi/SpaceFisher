using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] Camera cam;
    PlanetFishTable stageData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageData = MinigameManager.Instance.PlanetData;
    }

    // Update is called once per frame
    void Update()
    {
        cam.backgroundColor = stageData.BackgroundGradient.Evaluate(-cam.transform.position.y / stageData.MaxDepth);
    }
}
