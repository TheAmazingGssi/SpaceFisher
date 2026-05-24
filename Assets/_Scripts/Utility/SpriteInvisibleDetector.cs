using UnityEngine;
using UnityEngine.Events;

public class SpriteInvisibleDetector : MonoBehaviour
{
    public UnityEvent becameInvisible;

    private void OnBecameInvisible()
    {
        becameInvisible.Invoke();
    }
}
