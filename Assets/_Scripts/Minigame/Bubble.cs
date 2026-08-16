using UnityEngine;

public class Bubble : MonoBehaviour
{
    Vector2 mainPosition;
    [SerializeField] float wiggleModifier;
    [SerializeField] float wiggleSpeed;
    [SerializeField] Camera cam;
    [SerializeField] Vector2 scaleMaxMin;
    const float buffer = 1;

    private void Start()
    {
        mainPosition = transform.position;
        if(!cam)
            cam = Camera.main;
    }
    private void Update()
    {
        transform.position = new Vector2(mainPosition.x + (wiggleModifier * Mathf.Sin(Time.time * wiggleSpeed)), mainPosition.y);
        //OnBecameInvisible();
    }

    private void OnBecameInvisible()
    {
        Vector2 checkVect;
        float addSign;
        if (MinigameManager.Instance.Phase == MinigamePhase.Down)
        {
            checkVect = Vector2.zero;
            addSign = -buffer;
        }
        else
        {
            checkVect = Vector2.one;
            addSign = buffer;
        }
        
        Vector2 camEdge = cam.ViewportToWorldPoint(checkVect);
        float newY = camEdge.y + addSign;
        if (newY >= -10)
            return;

        camEdge.x *= Mathf.Sign(camEdge.x);
        float newX = Random.Range(-camEdge.x, camEdge.x);
        mainPosition = new Vector2(newX, newY);

        float scale = Random.Range(scaleMaxMin.x, scaleMaxMin.y);
        transform.localScale = new Vector3(scale, scale, 1);

    }
}
