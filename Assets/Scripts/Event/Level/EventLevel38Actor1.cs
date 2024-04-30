using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel38Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[10])
        {
            // 状态 1 交易
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我有三把黄钥匙，只需 200 金币你就能得到它。", "我买了", "不需要", () =>
                {
                    // 花费 200 金币
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 200)
                    {
                        GameManager.Instance.UIManager.ShowInfo("金币不够哦~再去历练一下叭~");
                        // 音频播放
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 200;
                    // 获得 黄钥匙 3 把
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).GetComponent<ItemController>());
                    }
                    // 变更剧情状态
                    GameManager.Instance.PlotManager.PlotDictionary[10] = 2;
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                });
                break;
            case 2:
                string talk = "存放翡翠剑的房间门坏了，你必须用镐破墙而入。";
                // 商人说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // 记笔记
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "31-39");
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
