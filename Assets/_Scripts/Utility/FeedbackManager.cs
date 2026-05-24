using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private Sprite coinSprite;

    private void Awake()
    {
        Bus<ShowCoin>.OnEvent += OnShowCoin;
    }
    private void OnShowCoin(ShowCoin e)
    {
        //TODO: coin feedback logic
    }

    private void OnDestroy()
    {
        Bus<ShowCoin>.OnEvent -= OnShowCoin;
    }
}
