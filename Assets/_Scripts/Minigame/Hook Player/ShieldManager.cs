using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    int shieldAmounts = 0;
    [SerializeField] GameObject[] shieldVisual;

    #region monobehaviour
    private void OnEnable()
    {
        Bus<HitBomb>.OnEvent += OnHitBomb;
        Bus<MinigameStart>.OnEvent += OnMinigameStart;
    }
    private void OnDisable()
    {
        Bus<HitBomb>.OnEvent -= OnHitBomb;
        Bus<MinigameStart>.OnEvent -= OnMinigameStart;
    }
    #endregion
    
    //OnMinigameStart works like Start() but called every minigame start
    private void OnMinigameStart(MinigameStart e)
    {
        shieldAmounts = UpgradeManager.Instance.GetUpgrade(Upgrade.Shield);
        for (int i = 0; i < shieldVisual.Length; i++)
        {
            if(i <  shieldAmounts)
                shieldVisual[i].SetActive(true);
            else
                shieldVisual[i].SetActive(false);
        }
    }

    private void OnHitBomb(HitBomb e)
    {
        if (shieldAmounts > 0)
        {
            shieldAmounts--;
            shieldVisual[shieldAmounts].gameObject.SetActive(false);
        }
        else
        {
            MinigameManager.Instance.Phase = MinigamePhase.Up;
            Bus<FishFell>.Raise(new FishFell());
        }
    }
}
