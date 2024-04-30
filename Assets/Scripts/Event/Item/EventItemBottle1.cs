using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemBottle1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 5;
        int giveHealth = 200;
        GameManager.Instance.PlayerManager.PlayerInfo.Health += giveHealth;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 个，恢复 {giveHealth} 点生命值。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "GiveHealth");
        return false;
    }
}
