using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel21Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "大法师在25楼，他是魔塔的主人。你需要更高级的道具才能打败他，否则就是在自杀！";
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
