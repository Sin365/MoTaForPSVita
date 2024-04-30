using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemEquipmentWeapon4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 30;
        int giveAttack = 50;
        GameManager.Instance.PlayerManager.PlayerInfo.Attack += giveAttack;
        GameManager.Instance.UIManager.ShowInfo($"装备 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，增加 {giveAttack} 点攻击力。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        GameManager.Instance.PlayerManager.PlayerInfo.WeaponID = itemId;
        return false;
    }
}
