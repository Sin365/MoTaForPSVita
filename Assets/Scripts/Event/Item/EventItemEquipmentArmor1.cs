using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemEquipmentArmor1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 25;
        int giveDefence = 10;
        GameManager.Instance.PlayerManager.PlayerInfo.Defence += giveDefence;
        GameManager.Instance.UIManager.ShowInfo($"装备 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，增加 {giveDefence} 点防御力。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        GameManager.Instance.PlayerManager.PlayerInfo.ArmorID = itemId;
        return false;
    }
}
