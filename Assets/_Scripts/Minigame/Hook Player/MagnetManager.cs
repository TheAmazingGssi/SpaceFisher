using System;
using UnityEngine;

public class MagnetManager : MonoBehaviour
{
    [SerializeField] CircleCollider2D col;

    MinigamePhase lastFramePhase;
    private void Start()
    {
        lastFramePhase = MinigameManager.Instance.Phase;
        
    }
    private void Update()
    {
        if(lastFramePhase != MinigameManager.Instance.Phase)
        {
            if(MinigameManager.Instance.Phase == MinigamePhase.Up)
            {
                //make radius bigger
                int level = UpgradeManager.Instance.GetUpgrade(Upgrade.Magnet);
                col.radius = MinigameManager.Instance.UpgradeData.MagnetRadius[level];
            }
            else if(MinigameManager.Instance.Phase == MinigamePhase.Down)
            {
                col.radius = 0;
            }
        }
        lastFramePhase = MinigameManager.Instance.Phase;
    }
}
