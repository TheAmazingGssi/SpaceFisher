using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.Player)
        {
            MinigameManager.Instance.Phase = MinigamePhase.Up;
            Bus<FishFell>.Raise(new FishFell());
        }
    }
}
