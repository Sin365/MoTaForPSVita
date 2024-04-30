using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel45Actor2 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[12])
        {
            // 状态 1 交易
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我能提升你 2000 点生命值，只需 1000 金币你就能得到它。", "我买了", "不需要", () =>
                {
                    // 花费 1000 金币
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 1000)
                    {
                        GameManager.Instance.UIManager.ShowInfo("金币不够哦~再去历练一下叭~");
                        // 音频播放
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 1000;
                    // 提升生命
                    GameManager.Instance.PlayerManager.PlayerInfo.Health += 2000;
                    // 变更剧情状态
                    GameManager.Instance.PlotManager.PlotDictionary[12] = 2;
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                });
                break;
            case 2:
                string talk = "神圣盾能免疫魔法攻击，但它被藏在异界内。";
                // 商人说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // 记笔记
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "41-48");
                    // NPC 回收
                    GameManager.Instance.PoolManager.RecycleResource(gameObject);
                });
                break;
            default:
                break;
        }
        return false;
    }
}
