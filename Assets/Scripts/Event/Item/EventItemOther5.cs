using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemOther5 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        // 楼层跳跃
        GameManager.Instance.LevelManager.Level += 1;
        // 提示信息并从背包清除
        int itemId = 21;
        GameManager.Instance.UIManager.ShowInfo($"使用 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ，向上一层传送。");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
