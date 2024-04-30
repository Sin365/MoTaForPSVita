using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel12Actor2 : ActorController
{
    public override bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "给我 1 金币，你猜我能送给你什么？", "大宝剑！", "鬼知道~", () =>
        {
            // 花费 1 金币
            if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 1)
            {
                GameManager.Instance.UIManager.ShowInfo("穷鬼，快走开!");
                // 音频播放
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                return;
            }
            GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 1;
            int randomInt = UnityEngine.Random.Range(1, 100);
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
            if (randomInt == 88)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Gold += 88;
                GameManager.Instance.UIManager.ShowInfo("运气不错嘛~获得 88 金币。");
                // 打开人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // NPC 回收
                GameManager.Instance.PoolManager.RecycleResource(gameObject);
            }
        });
        return false;
    }
}
