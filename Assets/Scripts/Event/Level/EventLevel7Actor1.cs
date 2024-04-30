using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel7Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[3])
        {
            // 状态 1 交易
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我有五把黄钥匙，只需 50 金币你就能得到它。", "我买了", "不需要", () =>
                {
                    // 花费 50 金币
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 50)
                    {
                        GameManager.Instance.UIManager.ShowInfo("金币不够哦~再去历练一下叭~");
                        // 音频播放
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 50;
                    // 获得 黄钥匙 5 把
                    for (int i = 0; i < 5; i++)
                    {
                        GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).GetComponent<ItemController>());
                    }
                    // 变更剧情状态
                    GameManager.Instance.PlotManager.PlotDictionary[3] = 2;
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
                });
                break;
            case 2:
                string talk = "你最好先提升防御力，只有在攻击力低于敌人的防御力时才提升攻击力。";
                // 商人说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // 记笔记
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
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
