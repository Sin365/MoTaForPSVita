using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品信息
/// </summary>
[Serializable]
public class ItemInfo
{
    public int ID;
    public string Name;
    public string Info;
    public string IconPath;
    public int UseCount;
}

public class BackpackManager : Singleton<BackpackManager>
{
    [SerializeField]
    public Dictionary<int, ItemInfo> BackpackDictionary = new Dictionary<int, ItemInfo>();

    /// <summary>
    /// 捡起物品
    /// </summary>
    /// <param name="item">物品控制器</param>
    public void PickUp(ItemController item)
    {
        ItemInfo itemInfo = TransferItemControllerToItemInfo(item);
        // 如果背包没有则创建
        if (!BackpackDictionary.ContainsKey(itemInfo.ID))
        {
            // 复制物体防止引用错误
            BackpackDictionary.Add(itemInfo.ID, itemInfo);
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemInfo.ID, itemInfo);
        }
        // 如果有则增加数量
        else
        {
            BackpackDictionary[itemInfo.ID].UseCount += itemInfo.UseCount;
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemInfo.ID, BackpackDictionary[itemInfo.ID]);
        }
        // 捡起后物体消失
        GameManager.Instance.PoolManager.RecycleResource(item.gameObject);
        // UI 提示
        GameManager.Instance.UIManager.ShowInfo($"获得 {item.Name} {(item.UseCount < 0 ? 1 : item.UseCount)} 个");
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
    }

    /// <summary>
    /// 消耗物品
    /// </summary>
    /// <param name="itemID">物品 ID</param>
    /// <returns>是否消耗成功</returns>
    public bool ConsumeItem(int itemID)
    {
        // 如果背包有则消耗数量
        if (BackpackDictionary.ContainsKey(itemID))
        {
            BackpackDictionary[itemID].UseCount -= 1;
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemID, BackpackDictionary[itemID]);
            // 如果数量为 0 则删除 小于 0 为无限使用的物品
            if (BackpackDictionary[itemID].UseCount == 0) BackpackDictionary.Remove(itemID);
            return true;
        }
        // 如果有则返回 false
        else return false;
    }

    /// <summary>
    /// 转换物品控制器为物品信息
    /// </summary>
    /// <param name="item">物品控制器</param>
    /// <returns>物品信息</returns>
    public ItemInfo TransferItemControllerToItemInfo(ItemController item)
    {
        return new ItemInfo
        {
            ID = item.ID,
            Name = item.Name,
            Info = item.Info,
            IconPath = item.IconPath,
            UseCount = item.UseCount,
        };
    }
}
