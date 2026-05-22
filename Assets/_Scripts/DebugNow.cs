using UnityEngine;

public class DebugNow : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [ContextMenu("spawnShop")]
    private void Debug()
    { 
        Instantiate(obj);
        obj.GetComponent<MoveableObject>().TryStartMoving();
    }
}
