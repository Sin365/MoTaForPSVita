using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor4 : ActorController
{
    public override bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "感谢你救了我，我能提升你 3% 的攻击力和防御力，你需要吗？", "感激不尽", "下次再说", () =>
        {
            // 提升属性
            GameManager.Instance.PlayerManager.PlayerInfo.Attack = (int)(GameManager.Instance.PlayerManager.PlayerInfo.Attack * 1.03f);
            GameManager.Instance.PlayerManager.PlayerInfo.Defence = (int)(GameManager.Instance.PlayerManager.PlayerInfo.Defence * 1.03f);
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
