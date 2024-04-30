using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel27Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "如果你来到这一层还拥有 1500 生命值、80 攻击力、98 防御力、1 把蓝钥匙、5 把黄钥匙的话，你就是成功人士。";
        // 老头说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
        {
            // 记笔记
            GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
