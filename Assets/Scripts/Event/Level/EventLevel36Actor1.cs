using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel36Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "如果你能用好 4 种移动宝物，你不用与强敌作战就能上楼。";
        // 老头说话
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
        return false;
    }
}
