using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Animator anim;
    const string BOOM_TRIGGER = "Boom";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.Player)
        {
            MinigameManager.Instance.Phase = MinigamePhase.Up;
            anim.SetTrigger(BOOM_TRIGGER);
            Bus<FishFell>.Raise(new FishFell());
        }
    }   
    public void DeleteSelf()
    {
        Destroy(gameObject);
    }
}
