using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel48Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "骰子上 5 点的形状是一种封印魔法，你最好记住它，在与魔王战斗时有大用处。";
        // 老头说话
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
        return false;
    }
}
