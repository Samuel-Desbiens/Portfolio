using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRewardManager : MonoBehaviour
{
    [SerializeField] GameObject reward;

    public void UnlockReward()
    {
        PermanentUpgradesManager.instance.AddUnlockedSoul(reward);
    }
}
