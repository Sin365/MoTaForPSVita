using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel16Actor2 : ActorController
{
    public override bool Interaction()
    {
        // 小偷说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "很好，你居然找到了我。", "给你这瓶圣水作为奖励。", "喝了它你可以根据你的攻击力和防御力增加生命点数，不要早早的用掉了。" }, () =>
        {
            // 给予人物圣水
            GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 17).GetComponent<ItemController>());
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
