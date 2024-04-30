using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel3Actor1 : ActorController
{
    public override bool Interaction()
    {
        // 小偷说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "这本怪物手册交给你。", "它能查看本层怪物的能力。", "祝你好运。" }, () =>
        {
            // 给予人物怪物手册
            GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 10).GetComponent<ItemController>());
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
