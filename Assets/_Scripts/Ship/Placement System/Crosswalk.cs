using UnityEngine;

[ExecuteAlways]
public class Crosswalk : MonoBehaviour
{
    [SerializeField] int crosswalkWidth;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] BoxCollider2D col;
    const float gridSizeX = 4;
    const float gridSizeY = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        sprite.size = new Vector2(crosswalkWidth * gridSizeX, gridSizeY);
        col.size = sprite.size;
        if(crosswalkWidth%2  != 0 )
        {
            sprite.transform.localPosition = new Vector3(gridSizeX/2, gridSizeY/2, 0);
        }
        else
            sprite.transform.localPosition = new Vector3(0, gridSizeY/2, 0);
#endif
    }
}
