using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel12Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[5])
        {
            // 状态 1 交易
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "我有一把红钥匙，只需 800 金币你就能得到它。", "我买了", "有点贵", () =>
                {
                    // 花费 800 金币
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 800)
                    {
                        GameManager.Instance.UIManager.ShowInfo("金币不够哦~再去历练一下叭~");
                        // 音频播放
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 800;
                    // 获得 红钥匙 5 把
                    GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 3).GetComponent<ItemController>());
                    // 变更剧情状态
                    GameManager.Instance.PlotManager.PlotDictionary[5] = 2;
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                });
                break;
            case 2:
                string talk = "你是否注意到 5、9、14、16、18 层墙的与众不同？";
                // 商人说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // 记笔记
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 音频播放
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
                    // NPC 回收
                    GameManager.Instance.PoolManager.RecycleResource(gameObject);
                });
                break;
        }
        return false;
    }
}
